using System;
using System.Diagnostics;
using System.Linq;
using CellularAutomaton;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseGetNeighboringCellsTests
    {
        private const int ROWS_COUNT    = 10;
        private const int COLUMNS_COUNT = 15;
        
        private const int ROWS_PLUS_ONE    = ROWS_COUNT    + 1;
        private const int COLUMNS_PLUS_ONE = COLUMNS_COUNT + 1;

        private Universe _universe;

        [SetUp]
        public void Setup()
        {
            _universe = Universe.MakeUniverse(ROWS_COUNT, COLUMNS_COUNT);
        }

        [Test]
        public void GetNeighboringCells_NegativeArguments_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells( 1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _universe.GetNeighboringCells(-1,  1));
        }

        [Test]
        public void GetNeighboringCells_ArgumentOutOfBoundaries_ThrowsException()
        {
            //rows and columns counts are out of boundaries, since the universe is zero based.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _universe.GetNeighboringCells(ROWS_COUNT, COLUMNS_COUNT));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _universe.GetNeighboringCells(ROWS_PLUS_ONE, COLUMNS_PLUS_ONE));
        }

        [Test]
        public void GetNeighboringCells_UniverseIsOnlyOneCell_ReturnsEmptyCollection()
        {
            var oneCellUniverse = Universe.MakeUniverse(1, 1);
            var actualCollection = oneCellUniverse.GetNeighboringCells(0, 0);
            CollectionAssert.IsEmpty(actualCollection);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleRowTargetIsFirstCell_ReturnsSingRightNeighbor()
        {
            var universe = Universe.MakeUniverse(1, 5);

            var neighbors = universe.GetNeighboringCells(0, 0).ToArray();

            Assert.AreEqual(neighbors.Single().Column, 1);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleRowTargetIsLastCell_ReturnsSingLeftNeighbor()
        {
            var columnCount = 5;
            var universe    = Universe.MakeUniverse(1, columnCount);
            var lastColumn  = columnCount - 1;

            var neighbors = universe.GetNeighboringCells(0, lastColumn).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 0);
            Assert.AreEqual(neighbors.Single().Column, lastColumn - 1);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleColumnTargetIsFirstCell_ReturnsSingBelowNeighbor()
        {
            var universe = Universe.MakeUniverse(5, 1);

            var neighbors = universe.GetNeighboringCells(0, 0).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleColumnTargetIsLastCell_ReturnsSingAboveNeighbor()
        {
            var rowCount = 5;
            var universe = Universe.MakeUniverse(rowCount, 1);
            var lastRow = rowCount - 1;

            var neighbors = universe.GetNeighboringCells(lastRow, 0).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, lastRow - 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleColumn_ReturnsSingleNeighbor()
        {
            var universe = Universe.MakeUniverse(5, 1);

            var neighbors = universe.GetNeighboringCells(0, 0).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleRowCellInMiddle_ReturnsSingleNeighbor()
        {
            var universe = Universe.MakeUniverse(1, 10);

            var targetCol = 5;

            var neighbors = universe.GetNeighboringCells(0, targetCol).ToArray();

            var colIndices = new[] {targetCol - 1, targetCol + 1};

            var colInCount = neighbors.Count(neighbor => colIndices.Contains(neighbor.Column));

            Assert.AreEqual(neighbors.Count(), 2);
            Assert.AreEqual(colInCount, 2);
        }

        [Test]
        public void GetNeighboringCells_UniverseIsSingleColumnCellInMiddle_ReturnsSingleNeighbor()
        {
            var universe = Universe.MakeUniverse(10, 1);

            var targetRow = 5;

            var neighbors = universe.GetNeighboringCells(targetRow, 0).ToArray();

            var rowIndices = new[] { targetRow - 1, targetRow + 1 };

            var rowInCount = neighbors.Count(neighbor => rowIndices.Contains(neighbor.Row));

            Assert.AreEqual(neighbors.Count(), 2);
            Assert.AreEqual(rowInCount, 2);
        }

        [Test]
        public void GetNeighboringCells_TargetCellSurrounded_ReturnsNeighbors()
        {
            var targetRow = 3;
            var targetCol = 6;

            var neighbors = _universe.GetNeighboringCells(targetRow, targetCol).ToArray();

            var rowIndices = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var colIndices = new[] { targetCol - 1, targetCol, targetCol + 1 };

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

        [Test]
        public void GetNeighboringCells_TargetIsTopLeftCorner_ReturnsNeighbors()
        {
            var neighbors = _universe.GetNeighboringCells(0, 0).ToArray();

            var rowIndices = new[] { 0, 1 };
            var colIndices = new[] { 0, 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 3);
            Assert.AreEqual(rowInCount, 3);
            Assert.AreEqual(colInCount, 3);
        }

        [Test]
        public void GetNeighboringCells_TargetIsTopRightCorner_ReturnsNeighbors()
        {
            var lastColumn = _universe.ColumnsCount - 1;

            var neighbors = _universe.GetNeighboringCells(0, lastColumn).ToArray();

            var rowIndices = new[] { 0, 1 };
            var colIndices = new[] { lastColumn - 1, lastColumn };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 3);
            Assert.AreEqual(rowInCount, 3);
            Assert.AreEqual(colInCount, 3);
        }

        [Test]
        public void GetNeighboringCells_TargetIsBottomRightCorner_ReturnsNeighbors()
        {
            var lastRow = _universe.RowsCount - 1;
            var lastColumn = _universe.ColumnsCount - 1;

            var neighbors = _universe.GetNeighboringCells(lastRow, lastColumn).ToArray();

            var rowIndices = new[] { lastRow - 1, lastRow };
            var colIndices = new[] { lastColumn - 1, lastColumn };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 3);
            Assert.AreEqual(rowInCount, 3);
            Assert.AreEqual(colInCount, 3);
        }

        [Test]
        public void GetNeighboringCells_TargetIsBottomLeftCorner_ReturnsNeighbors()
        {
            var lastRow = _universe.RowsCount - 1;

            var neighbors = _universe.GetNeighboringCells(lastRow, 0).ToArray();

            var rowIndices = new[] { lastRow - 1, lastRow };
            var colIndices = new[] { 0, 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 3);
            Assert.AreEqual(rowInCount, 3);
            Assert.AreEqual(colInCount, 3);
        }

        [Test]
        public void GetNeighboringCells_TargetIsMidOfFirstRow_ReturnsNeighbors()
        {
            var targetRow = 0;
            var targetCol = 4;

            var neighbors = _universe.GetNeighboringCells(targetRow, targetCol).ToArray();

            var rowIndices = new[] { targetRow, targetRow + 1 };
            var colIndices = new[] { targetCol - 1, targetCol, targetCol + 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 5);
            Assert.AreEqual(rowInCount, 5);
            Assert.AreEqual(colInCount, 5);
        }

        [Test]
        public void GetNeighboringCells_TargetIsMidOfLastRow_ReturnsNeighbors()
        {
            var targetRow = _universe.RowsCount - 1;
            var targetCol = 6;

            var neighbors = _universe.GetNeighboringCells(targetRow, targetCol).ToArray();

            var rowIndices = new[] { targetRow, targetRow - 1 };
            var colIndices = new[] { targetCol - 1, targetCol, targetCol + 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 5);
            Assert.AreEqual(rowInCount, 5);
            Assert.AreEqual(colInCount, 5);
        }


        [Test]
        public void GetNeighboringCells_TargetIsMidOfFirstColumn_ReturnsNeighbors()
        {
            var targetRow = 6;
            var targetCol = 0;

            var neighbors = _universe.GetNeighboringCells(targetRow, targetCol).ToArray();

            var rowIndices = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var colIndices = new[] { targetCol, targetCol + 1 };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 5);
            Assert.AreEqual(rowInCount, 5);
            Assert.AreEqual(colInCount, 5);
        }

        [Test]
        public void GetNeighboringCells_TargetIsMidOfLastColumn_ReturnsNeighbors()
        {
            var targetRow = 6;
            var targetCol = _universe.ColumnsCount - 1;

            var neighbors = _universe.GetNeighboringCells(targetRow, targetCol).ToArray();

            var rowIndices = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var colIndices = new[] { targetCol - 1, targetCol };

            var rowInCount = 0;
            var colInCount = 0;

            foreach (var neighbor in neighbors)
            {
                if (rowIndices.Contains(neighbor.Row))
                    rowInCount++;

                if (colIndices.Contains(neighbor.Column))
                    colInCount++;
            }

            Assert.AreEqual(neighbors.Count(), 5);
            Assert.AreEqual(rowInCount, 5);
            Assert.AreEqual(colInCount, 5);
        }

        [Test]
        public void GetNeighboringCells_CellIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _universe.GetNeighboringCells(null));
        }

        [Test]
        public void GetNeighboringCells_CellNotInTheUniverse_ThrowsException()
        {
            var cell = Cell.MakeCell(4,3);
            Assert.Throws<ArgumentException>(() => _universe.GetNeighboringCells(cell));
        }

        [Test]
        public void GetNeighboringCells_CellInTheUniverse_ReturnsNeighbors()
        {
            var targetRow = 3;
            var targetCol = 6;

            var cell = _universe[targetRow, targetCol];

            var neighbors = _universe.GetNeighboringCells(cell).ToArray();

            var rowIndices = new[] { targetRow - 1, targetRow, targetRow + 1 };
            var colIndices = new[] { targetCol - 1, targetCol, targetCol + 1 };

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
