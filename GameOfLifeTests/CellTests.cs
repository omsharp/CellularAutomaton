using NUnit.Framework;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestFixture]
    public class CellTests
    {
        private Cell _cell;
        private int  _row;
        private int  _column;

        [SetUp]
        public void Setup()
        {
            _row    = 2;
            _column = 5;
            
            _cell = new Cell(_row,_column);
        }

        [Test]
        public void Row_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_row,_cell.Row);
        }

        [Test]
        public void Column_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_column,_cell.Column);
        }

        [Test]
        public void When_Cell_Is_First_Created_LifeStatus_Is_Dead()
        {
            Assert.AreEqual(LifeStatus.Dead, _cell.LifeStatus);
        }

        [Test]
        public void When_Cell_Is_Revived_LifeStatus_Is_Alive()
        {
            
        }
        
    }
}
