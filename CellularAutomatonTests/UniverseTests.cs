using System;
using System.Linq;
using CellularAutomaton;
using Moq;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private const int ROWS_COUNT = 10;
        private const int COLUMNS_COUNT = 15;
        
        private Universe _universe;

        [SetUp]
        public void Setup()
        {
            _universe = Universe.MakeUniverse(ROWS_COUNT, COLUMNS_COUNT);
        }

        [Test]
        public void MakeUniverse_ArgumentsGreaterThanZero_ReturnsUniverseInstance()
        {
            var universe1 = Universe.MakeUniverse(1, 1);
            var universe2 = Universe.MakeUniverse(44, 24);

            Assert.IsInstanceOf(typeof(Universe), universe1);
            Assert.IsInstanceOf(typeof(Universe), universe2);
        }

        [Test]
        public void MakeUniverse_ArgumentLessThanOne_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(0, 0));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(0, 1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(1, 0));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(-1, -1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(1, -1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(-1, 1));
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
        public void NextCycle_RulesListNotEmpty_ApplyRules()
        {
            var reviveAllCells = new Mock<IRule>();
            var moveToNextGen = new Mock<IRule>();
            var diagonalKill = new Mock<IRule>();

            reviveAllCells.Setup(r => r.Transform(It.IsAny<CellularGrid>()))
                          .Callback<CellularGrid>(u =>
                          {
                              foreach (var cell in u.Cells)
                                  cell.Revive();
                          });

            moveToNextGen.Setup(r => r.Transform(It.IsAny<CellularGrid>()))
                         .Callback<CellularGrid>(u =>
                         {
                             foreach (var cell in u.Cells)
                                 cell.Evolve();
                         });

            diagonalKill.Setup(r => r.Transform(It.IsAny<CellularGrid>()))
                        .Callback<CellularGrid>(u =>
                        {
                            var cells = u.Cells.Where(c => c.Row == c.Column);

                            foreach (var cell in cells)
                                cell.Kill();
                        });

            _universe.Rules.Add(reviveAllCells.Object);
            _universe.Rules.Add(moveToNextGen.Object);
            _universe.Rules.Add(diagonalKill.Object);

            _universe.NextCycle();

            var firstApplied = _universe.Grid.Cells.Where(c => c.Row != c.Column)
                                               .All(c => c.Status == CellStatus.Alive);

            var secondApplied = _universe.Grid.Cells.Where(c => c.Row != c.Column)
                                               .All(c => c.Generation == 2);

            var thirdApplied = _universe.Grid.Cells.Where(c => c.Row == c.Column)
                                               .All(c => c.Status == CellStatus.Dead);


            Assert.IsTrue(secondApplied);
            Assert.IsTrue(firstApplied);
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