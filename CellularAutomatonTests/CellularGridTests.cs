﻿using System;
using System.Linq;
using CellularAutomaton;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    public class InitRule : IRule
    {

        public bool Condition(Cell cell, CellularGrid grid)
        {
            return (cell.Row % 2 == 0 || cell.Column % 3 == 0);
        }

        public void Action(Cell cell)
        {
            cell.Revive();
        }
    }

    [TestFixture]
    public class CellularGridTests
    {
        private const int ROWS_COUNT    = 10;
        private const int COLUMNS_COUNT = 15;
        
        private const int ROWS_PLUS_ONE    = ROWS_COUNT    + 1;
        private const int COLUMNS_PLUS_ONE = COLUMNS_COUNT + 1;

        private CellularGrid _grid;

        [SetUp]
        public void Setup()
        {
            _grid = new CellularGrid(ROWS_COUNT, COLUMNS_COUNT, new InitRule());
        }

        [Test]
        public void GetNeighboringCells_NegativeArguments_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _grid.GetNeighboringCells(-1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _grid.GetNeighboringCells( 1, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _grid.GetNeighboringCells(-1,  1));
        }

        [Test]
        public void GetNeighboringCells_ArgumentOutOfBoundaries_ThrowsException()
        {
            //rows and columns counts are out of boundaries, since the universe is zero based.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _grid.GetNeighboringCells(ROWS_COUNT, COLUMNS_COUNT));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _grid.GetNeighboringCells(ROWS_PLUS_ONE, COLUMNS_PLUS_ONE));
        }

        [Test]
        public void GetNeighboringCells_GridIsOnlyOneCell_ReturnsEmptyCollection()
        {
            var grid   = new CellularGrid(1, 1, new InitRule());
            var actual = grid.GetNeighboringCells(0, 0);

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleRowTargetIsFirstCell_ReturnsSingRightNeighbor()
        {
            var grid      = new CellularGrid(1, 5, new InitRule());
            var neighbors = grid.GetNeighboringCells(0, 0).ToArray();

            Assert.AreEqual(neighbors.Single().Column, 1);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleRowTargetIsLastCell_ReturnsSingLeftNeighbor()
        {
            var columnCount = 5;
            var grid        = new CellularGrid(1, columnCount, new InitRule());
            var lastColumn  = columnCount - 1;
            var neighbors   = grid.GetNeighboringCells(0, lastColumn).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 0);
            Assert.AreEqual(neighbors.Single().Column, lastColumn - 1);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleColumnTargetIsFirstCell_ReturnsSingBelowNeighbor()
        {
            var grid      = new CellularGrid(5, 1, new InitRule());
            var neighbors = grid.GetNeighboringCells(0, 0);

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleColumnTargetIsLastCell_ReturnsSingAboveNeighbor()
        {
            var rowCount  = 5;
            var grid      = new CellularGrid(rowCount, 1, new InitRule());
            var lastRow   = rowCount - 1;
            var neighbors = grid.GetNeighboringCells(lastRow, 0).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, lastRow - 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleColumn_ReturnsSingleNeighbor()
        {
            var grid      = new CellularGrid(5, 1, new InitRule());
            var neighbors = grid.GetNeighboringCells(0, 0).ToArray();

            Assert.AreEqual(neighbors.Count(), 1);
            Assert.AreEqual(neighbors.Single().Row, 1);
            Assert.AreEqual(neighbors.Single().Column, 0);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleRowCellInMiddle_ReturnsSingleNeighbor()
        {
            var grid       = new CellularGrid(1, 10, new InitRule());
            var targetCol  = 5;
            var neighbors  = grid.GetNeighboringCells(0, targetCol).ToArray();
            var colIndices = new[] {targetCol - 1, targetCol + 1};
            var colInCount = neighbors.Count(neighbor => colIndices.Contains(neighbor.Column));

            Assert.AreEqual(neighbors.Count(), 2);
            Assert.AreEqual(colInCount, 2);
        }

        [Test]
        public void GetNeighboringCells_GridIsSingleColumnCellInMiddle_ReturnsSingleNeighbor()
        {
            var grid       = new CellularGrid(10, 1, new InitRule());
            var targetRow  = 5;
            var neighbors  = grid.GetNeighboringCells(targetRow, 0);
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

            var neighbors = _grid.GetNeighboringCells(targetRow, targetCol).ToArray();

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
            var neighbors = _grid.GetNeighboringCells(0, 0).ToArray();

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
            var lastColumn = _grid.ColumnsCount - 1;

            var neighbors = _grid.GetNeighboringCells(0, lastColumn).ToArray();

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
            var lastRow = _grid.RowsCount - 1;
            var lastColumn = _grid.ColumnsCount - 1;

            var neighbors = _grid.GetNeighboringCells(lastRow, lastColumn).ToArray();

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
            var lastRow = _grid.RowsCount - 1;

            var neighbors = _grid.GetNeighboringCells(lastRow, 0).ToArray();

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

            var neighbors = _grid.GetNeighboringCells(targetRow, targetCol).ToArray();

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
            var targetRow = _grid.RowsCount - 1;
            var targetCol = 6;

            var neighbors = _grid.GetNeighboringCells(targetRow, targetCol).ToArray();

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

            var neighbors = _grid.GetNeighboringCells(targetRow, targetCol).ToArray();

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
            var targetCol = _grid.ColumnsCount - 1;

            var neighbors = _grid.GetNeighboringCells(targetRow, targetCol).ToArray();

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
        public void CountAliveNeighbors_FourCellsGrid_Return3()
        {
            var grid = new CellularGrid(2, 2,null);

            grid.Cells[0,1].Revive();
            grid.Cells[1,1].Revive();
            grid.Cells[1,0].Revive();

            var count = grid.CountAliveNeighbors(0, 0);
            
            Assert.AreEqual(3, count);
        }

    }
}
