﻿using System;
using System.Linq;
using System.Xml.Schema;
using CellularAutomaton;
using Moq;
using NUnit.Framework;

namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private Universe _universe;
        private int      _rowsCount;
        private int      _columnsCount;
        private int      _rowIndex;
        private int      _columnIndex;

        [SetUp]
        public void      Setup()
        { 
            _rowsCount    = 10;
            _columnsCount = 15;
            _rowIndex     = 5;
            _columnIndex  = 9;
            _universe     = Universe.MakeUniverse(_rowsCount, _columnsCount);
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
            var cell      = _universe[_rowIndex, _columnIndex];
            var excpected = (cell.Row == _rowIndex) && (cell.Column == _columnIndex);

            Assert.IsTrue(excpected);
        }

        [Test]
        public void Indexer_UseInvalidIndices_ThrowsOutOfBoundriesException()
        {
            // using the count of rows and columns as indices should be out of range.
            Assert.Throws<IndexOutOfRangeException>(() => _universe[_rowsCount, _columnsCount].Revive());
            // using negative numbers
            Assert.Throws<IndexOutOfRangeException>(() => _universe[-1, -1].Revive());
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
        public void GetNeighborhood_NegativeArguments_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighborhood(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighborhood( 1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighborhood(-1,  1));
        }

        [Test]
        public void GetNeghborhood_ArgumentOutOfBoundaries_ThrowException()
        {
            //rows and columns counts are out of boundaries, since the universe is zero based.
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighborhood(_rowsCount,
                                                                                       _columnsCount));

            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighborhood(_rowsCount + 1,
                                                                                       _columnsCount + 1));
        
        }

        
    }
}
