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
        private Universe _universe;
        private int _rowsCount;
        private int _columnsCount;

        [SetUp]
        public void Setup()
        { 
            _rowsCount = 10;
            _columnsCount = 15;
            _universe = new Universe(_rowsCount, _columnsCount);
        }

        [Test]
        public void Cells_Get_ReturnsCollectionOfCells()
        {
            var expected = typeof(Cell);
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Cells, expected);
        }

        [Test]
        public void Cells_Count_EqualsRowsTimesColumns()
        {
            var expected = _universe.RowsCount * _universe.ColumnsCount;
            Assert.AreEqual(_universe.Cells.Count(), expected);
        }

        [Test]
        public void Rules_Get_ReturnsCollectionOfIRules()
        {
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Rules, typeof(IRule));
        }

        [Test]
        public void Indexer_UseValidIndices_CellWithSpecifiedRowAndColumn()
        {
            const int expectedRow = 3;
            const int expectedColumn = 5;
            var cell = _universe[expectedRow, expectedColumn];
            Assert.AreEqual(cell.Row, expectedRow);
            Assert.AreEqual(cell.Column, expectedColumn);
        }

        [Test]
        public void Indexer_UseInvalidIndices_ThrowOutOfBoundriesException()
        {
            // using the count of rows and columns as indices should be out of range.
            Assert.Throws<IndexOutOfRangeException>(() => _universe[_rowsCount, _columnsCount].Revive());

            // using something greater thatn the cound of rows and columns as indices should be out of range.
            Assert.Throws<IndexOutOfRangeException>(() => _universe[_rowsCount + 1, _columnsCount + 1].Revive());
        }

        [Test]
        public void NextCycle_NormalCall_FiresCycleFinishedEvent()
        {
            var fired = false;
            _universe.CycleFinished += (sender,e) => fired = true;
            _universe.NextCycle();
            Assert.That(fired, Is.True.After(500));
        }

        [Test]
        public void NextCycle_NormalCall_AgeIncreasedByOne()
        {
            var oldAge = _universe.Age;
            _universe.NextCycle();
            Assert.AreEqual(_universe.Age, oldAge + 1);
        }

        [Test]
        public void NextCycle_NormalCall_AllRulesAppliedToAllCells()
        {
            var reviveAllCells = new Mock<IRule>();
            var moveToNextGen  = new Mock<IRule>();
            var diagonalKill   = new Mock<IRule>();

            reviveAllCells.Setup(r => r.Apply(It.IsAny<Cell>()))
                          .Callback<Cell>(c => c.Revive());
            
            moveToNextGen.Setup(r => r.Apply(It.IsAny<Cell>()))
                         .Callback<Cell>(c => c.MoveToNextGeneration());
            
            diagonalKill.Setup(r => r.Apply(It.IsAny<Cell>()))
                        .Callback<Cell>(c =>
                                            {
                                                if (c.Row == c.Column)
                                                    c.Kill();   
                                            });

            _universe.Rules.Add(reviveAllCells.Object);
            _universe.Rules.Add(moveToNextGen.Object);
            _universe.Rules.Add(diagonalKill.Object);

            _universe.NextCycle();

            var firstApplied  = _universe.Cells.Where(c => c.Row != c.Column)
                                               .All(c => c.Status == CellStatus.Alive);

            var secondApplied = _universe.Cells.Where(c => c.Row != c.Column)
                                               .All(c => c.Generation == 2);

            var thirdApplied  = _universe.Cells.Where(c => c.Row == c.Column)
                                               .All(c => c.Status == CellStatus.Dead);

            
            Assert.IsTrue(secondApplied);
            Assert.IsTrue(firstApplied);
            Assert.IsTrue(thirdApplied);
        }
    }
}
