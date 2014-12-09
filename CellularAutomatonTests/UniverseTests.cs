using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Schema;
using CellularAutomaton;
using Moq;
using NUnit.Framework;

namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private int _universeRowsCount;
        private int _universeColumnsCount;
        private int _specificRow;
        private int _specificColumn;
        
        private Universe _universe;

        [SetUp]
        public void Setup()
        { 
            _universeRowsCount    = 10;
            _universeColumnsCount = 15;
            _specificRow          = 5;
            _specificColumn       = 9;
            _universe             = Universe.MakeUniverse(_universeRowsCount, 
                                                          _universeColumnsCount);
        }

        [Test]
        public void MakeUniverse_ArgumentsGreaterThanZero_ReturnsUniverseInstance()
        {
            var universe1 = Universe.MakeUniverse(1, 1);
            var universe2 = Universe.MakeUniverse(44, 24);

            Assert.IsInstanceOf(typeof(Universe),universe1);
            Assert.IsInstanceOf(typeof(Universe),universe2);
        }

        [Test]
        public void MakeUniverse_ArgumentLessThanOne_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse( 0,  0));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse( 0,  1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse( 1,  0));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(-1, -1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse( 1, -1));
            Assert.Throws<ArgumentException>(() => Universe.MakeUniverse(-1,  1));
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
        public void Indexer_UseValidIndices_ReturnsCellWithSpecifiedRowAndColumn()
        {
            var cell      = _universe[_specificRow, _specificColumn];
            var excpected = (cell.Row == _specificRow) && (cell.Column == _specificColumn);

            Assert.IsTrue(excpected);
        }

        [Test]
        public void Indexer_UseInvalidIndices_ThrowsOutOfBoundriesException()
        {
            // using the count of rows and columns as indices should be out of range.
            Assert.Throws<IndexOutOfRangeException>(() => _universe[_universeRowsCount, 
                                                                    _universeColumnsCount].Revive());
            // using negative numbers
            Assert.Throws<IndexOutOfRangeException>(() => _universe[-1, -1].Revive());
        }

        [Test]
        public void NextCycle_NormalCall_FiresCycleFinishedEvent()
        {
            var fired = false;
            _universe.CycleFinished += (sender,e) => fired = true;
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
        public void NextCycle_RulesListNotEmpty_AllRulesApplied()
        {
            var reviveAllCells = new Mock<IRule>();
            var moveToNextGen  = new Mock<IRule>();
            var diagonalKill   = new Mock<IRule>();

            reviveAllCells.Setup(r => r.Apply(It.IsAny<Cell>()))
                          .Callback<Cell>(c => c.Revive());
            
            moveToNextGen.Setup(r => r.Apply(It.IsAny<Cell>()))
                         .Callback<Cell>(c => c.MoveToNextGeneration());
            
            diagonalKill.Setup(r => r.Apply(It.IsAny<Cell>()))
                        .Callback<Cell>(c => { if (c.Row == c.Column) c.Kill(); });

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

        [Test]
        public void ToString_NormalCall_SomeString()
        {
            var actual = _universe.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        [Test]
        public void GetNeighboringCells_NegativeArguments_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells( 1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(-1,  1));
        }

        [Test]
        public void GetNeighboringCells_ArgumentOutOfBoundaries_ThrowException()
        {
            //rows and columns counts are out of boundaries, since the universe is zero based.
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(_universeRowsCount,
                                                                                           _universeColumnsCount));

            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(_universeRowsCount + 1,
                                                                                           _universeColumnsCount + 1));
        }

        [Test]
        public void GetNeighboringCells_UniverseIsOnlyOneCell_ReturnsEmptyCollection()
        {
            var oneCellUniverse = Universe.MakeUniverse(1, 1);
            var actualCollection = oneCellUniverse.GetNeighboringCells(0, 0);
            CollectionAssert.IsEmpty(actualCollection);
        }

        [Test]
        public void GetNeighboringCells_TargetCellSurrounded_ReturnNeighbors()
        {
            var targetRow = 3;
            var targetCol = 6;
            
            var neighbors = _universe.GetNeighboringCells(targetRow,targetCol).ToArray();

            var rowIndices = new[]{ targetRow - 1, targetRow, targetRow + 1 };
            var colIndices = new[]{ targetCol - 1, targetCol, targetCol + 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 8);
            Assert.AreEqual(rowInCount, 8);
            Assert.AreEqual(colInCount, 8);
        }
        
    }
}