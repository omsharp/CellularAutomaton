using System;
using System.Linq;
using CellularAutomaton;
using CellularAutomaton.Core;
using Moq;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private const int ROWS_COUNT    = 10;
        private const int COLUMNS_COUNT = 13;
        
        private Universe<SquareCell, SquareCellularGrid> _universe;
 
        [SetUp]
        public void Setup()
        {
            var grid = new SquareCellularGrid(ROWS_COUNT, COLUMNS_COUNT);

            _universe = new Universe<SquareCell, SquareCellularGrid>(grid);
        }

        [Test]
        public void MakeUniverse_ArgumentLessThanOne_ThrowsException()
        {
            SquareCellularGrid n = null;
            Assert.Throws<ArgumentNullException>(() => new Universe<SquareCell, SquareCellularGrid>(n));
        }

        [Test]
        public void NextCycle_NormalCall_FiresCycleFinishedEvent()
        {
            var fired = false;

            _universe.CycleFinished += (sender, e) => fired = true;
            _universe.NextCycle();
            
            Assert.That(fired, Is.True.After(200));
        }

        [Test]
        public void NextCycle_NormalCall_AgeIncreasedByOne()
        {
            var oldAge = _universe.Age;

            _universe.NextCycle();
            
            Assert.AreEqual(_universe.Age, oldAge + 1);
        }

        [Test]
        public void NextCycle_RulesNotOverlapping_ApplyAllRules()
        {

            
            //Setup the return of the first Rule.
            var reviveRule = _universe.MakeNewRule("Rule-1")
                            .WhenTrue((c, grid) => (c.Row % 3 == 0))
                            .Do(c => c.Revive());

            //Setup the return of the second Rule.
            var killRule = _universe.MakeNewRule("Rule-2")
                            .WhenTrue((c, grid) => c.Row == 8)
                            .Do(c =>
                            {
                                c.Revive();
                                c.Evolve();
                            });

            //Setup the return of the third Rule.
            var evolveRule = _universe.MakeNewRule("Rule-3")
                .WhenTrue((c, grid) => c.Column == 4 && c.Row % 3 != 0 && c.Row != 8)
                .Do(c =>
                {
                    c.Revive();
                    c.EvolveFor(2);
                }); 
          
            _universe.Rules.Add(reviveRule);
            _universe.Rules.Add(killRule);
            _universe.Rules.Add(evolveRule);

            _universe.NextCycle();

            //All cells in rows 0, 3, 6, 9 are Alive and Generation 1
            var firstApplied = _universe.Grid.Cells.Where(c => c.Row % 3 == 0).All(c => c.Alive);

            //All cells in row 8 are Alive and Generation 2
            var secondApplied = _universe.Grid.Cells.Where(c => c.Row == 8).All(c => c.Alive && 
                                                                                c.Generation == 2);
            //All cells in column 4 and rows 1, 2, 4, 5, 7 are Alive and Generation 3
            var thirdApplied = _universe.Grid.Cells.Where(c => c.Column == 4 &&
                                                               c.Row % 3 != 0 &&
                                                               c.Row != 8).All(c => c.Alive && 
                                                                                    c.Generation == 3);
            Assert.IsTrue(firstApplied);
            Assert.IsTrue(secondApplied);
            Assert.IsTrue(thirdApplied);
        }

        [Test]
        public void ToString_NormalCall_SomeString()
        {
            var actual = _universe.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        [Test]
        public void InitialRule_NotNull_ShouldWorkAsExpected()
        {
            var init = Rule<SquareCell, SquareCellularGrid>.MakeRule("Init")
                .WhenTrue((c, g) => c.Row == g.RowsCount - 1 || c.Column == 3)
                .Do(c => c.Revive());
            
            var universe = new Universe<SquareCell, SquareCellularGrid>(new SquareCellularGrid(5, 5), init);

            var alive = universe.Grid.Cells.Count(c => c.Alive); //9

            Assert.AreEqual(9, alive);
        }
     
    }
}