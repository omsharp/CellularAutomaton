using System;
using System.Collections.Generic;
using System.Linq;
using CellularAutomaton;
using Moq;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private const int ROWS_COUNT    = 10;
        private const int COLUMNS_COUNT = 13;
        
        private Universe            _universe;
        private Mock<CellularGrid> _gridMock;
 
        [SetUp]
        public void Setup()
        {
            //_gridMock = new Mock<CellularGrid>(MockBehavior.Strict);

            //_gridMock.SetupGet(g => g.RowsCount).Returns(ROWS_COUNT);
            //_gridMock.SetupGet(g => g.ColumnsCount).Returns(COLUMNS_COUNT);

            ////setup cells
            //var cells = new List<Cell>();

            //for (var row = 0; row < ROWS_COUNT; row++)
            //{
            //    for (var column = 0; column < COLUMNS_COUNT; column++)
            //    {
            //        cells.Add(Cell.MakeCell(row, column));
            //    }
            //}

            //_gridMock.SetupGet(g => g.Cells).Returns(cells);
            
            _universe = Universe.MakeUniverse(CellularGrid.MakeCellularGrid(10,13));
        }

        [Test]
        public void MakeUniverse_ArgumentLessThanOne_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Universe.MakeUniverse(null));
        }

      
        [Test]
        public void Rules_Get_ReturnsCollectionOfIRules()
        {
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Rules, typeof(IRule));
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
            var reviveRule = new Mock<IRule>(MockBehavior.Strict);
            var killRule   = new Mock<IRule>(MockBehavior.Strict);
            var evolveRule = new Mock<IRule>(MockBehavior.Strict);

            //Setup the return of the first Rule.
            reviveRule.Setup(r => r.GetPredicate()).Returns((c, grid) => (c.Row % 3 == 0));
            reviveRule.Setup(r => r.GetAction()).Returns(c => c.Revive());

            //Setup the return of the second Rule.
            killRule.Setup(r => r.GetPredicate()).Returns((c, grid) => c.Row == 8);
            killRule.Setup(r => r.GetAction()).Returns((c) =>
                                                            {
                                                                c.Revive();
                                                                c.Evolve();
                                                            });

            //Setup the return of the third Rule.
            evolveRule.Setup(r => r.GetPredicate()).Returns((c, grid) => c.Column == 4 && 
                                                                         c.Row % 3 != 0 && 
                                                                         c.Row != 8);
            evolveRule.Setup(r => r.GetAction()).Returns(c =>
                                                            {
                                                                c.Revive();
                                                                c.Evolve(2);
                                                            }); 

            _universe.Rules.Add(reviveRule.Object);
            _universe.Rules.Add(killRule.Object);
            _universe.Rules.Add(evolveRule.Object);

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

     
    }
}