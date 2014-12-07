﻿
using System;
using System.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestFixture]
    public class UniverseTests
    {
        private Universe _universe;
        private int _rowsCount;
        private int _columnsCount;
        private int _cycleInterval;

        [SetUp]
        public void Setup()
        {
            _rowsCount    = 10;
            _columnsCount = 15;
            _cycleInterval = 200;
            _universe     = new Universe(_rowsCount, _columnsCount,_cycleInterval);
        }

        [Test]
        public void Cells_Property_Should_Expose_Collection_Of_Cells()
        {
            var expected = typeof(Cell);
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Cells, expected);
        }

        [Test]
        public void Cells_Count_Should_Equal_RowsCount_By_ColumnsCount()
        {
            var expected = _universe.RowsCount * _universe.ColumnsCount;
            Assert.AreEqual(_universe.Cells.Count(),expected);
        }

        [Test]
        public void Indexer_Should_Return_Cell_With_Specified_Row_And_Column()
        {
            var expectedRow = 3;
            var expectedColumn = 5;
            var cell = _universe[expectedRow, expectedColumn];
            Assert.AreEqual(cell.Row, expectedRow);
            Assert.AreEqual(cell.Column, expectedColumn);
        }

        
    }
}
