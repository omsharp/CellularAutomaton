
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

        [Test]
        public void When_First_Created_Not_Raising_OnCycle_Event()
        {
            var actual = false;
            var waitFor = _cycleInterval + 500;

           _universe.OnCycle += (sender, args) => actual = true;

            Assert.That(actual, Is.False.After(waitFor));
        }

        [Test]
        public void When_Start_Called_Begin_Raising_OnCycle_Event()
        {
            var actual = false;
            var waitFor = _cycleInterval + 500;

            _universe.OnCycle += (sender, args) => actual = true;

            _universe.Start();

            Assert.That(actual, Is.True.After(waitFor));
        }

        //[Test]
        //public void When_Pause_Called_Stop_Raising_OnCycle()
        //{

        //    var done = false;
        //    var actual = false;

        //    _controlTimer.Elapsed += (sender, args) => done = true;
        //    _universe.OnCycle += (sender, args) => actual = true;

        //    _controlTimer.Start();
        //    _universe.Start();

        //    while (!done) { } // Give the universe some extra time to raise the event.

        //    _controlTimer.Dispose();

        //    Assert.IsFalse(actual);
        //}
       
    }
}
