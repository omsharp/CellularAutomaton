
using System.Linq;
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

        [SetUp]
        public void Setup()
        {
            _rowsCount = 10;
            _columnsCount = 15;
            _universe = new Universe(_rowsCount, _columnsCount);
        }

        [Test]
        public void RowsCount_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_universe.RowsCount, _rowsCount);
        }

        [Test]
        public void ColumnsCount_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_universe.ColumnsCount, _columnsCount);
        }

        [Test]
        public void Cells_Property_Should_Expose_Collection_Of_Cells()
        {
            var actual = typeof(Cell);
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Cells, actual);
        }

        [Test]
        public void Cells_Count_Should_Equal_RowsCount_By_ColumnsCount()
        {
            var expected = _universe.RowsCount * _universe.ColumnsCount;
            Assert.AreEqual(_universe.Cells.Count(),expected);
        }

        [Test]
        public void Indexer_Should_Return_Cell_With_Row_And_Column()
        {
            var expectedRow = 3;
            var expectedColumn = 5;
            var cell = _universe[expectedRow, expectedColumn];
            Assert.AreEqual(cell.Row, expectedRow);
            Assert.AreEqual(cell.Column, expectedColumn);
        }


    }
}
