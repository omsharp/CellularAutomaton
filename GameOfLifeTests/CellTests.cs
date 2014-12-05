using NUnit.Framework;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestFixture]
    public class CellTests
    {
        private Cell _cell;
        private int _row;
        private int _column;

        [SetUp]
        public void Setup()
        {
            _row    = 2;
            _column = 4;
            _cell   = new Cell(_row, _column);
        }

        [Test]
        public void Row_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_row, _cell.Row);
        }

        [Test]
        public void Column_Property_Is_Set_In_Constructor()
        {
            Assert.AreEqual(_column, _cell.Column);
        }

        [Test]
        public void When_Cell_First_Created_Generation_Is_Zero()
        {
            Assert.AreEqual(_cell.Generation, 0);
        }

        [Test]
        public void When_Cell_First_Created_Status_Is_Inactive()
        {
            Assert.AreEqual(CellStatus.Inactive, _cell.Status);
        }

        [Test]
        public void When_Cell_Revived_Status_Is_Alive()
        {
            _cell.Revive();
            Assert.AreEqual(_cell.Status, CellStatus.Alive);
        }

        [Test]
        public void When_Cell_Revived_Generation_Set_To_One()
        {
            _cell.Kill();
            _cell.Revive();
            Assert.AreEqual(_cell.Generation, 1);
        }

        [Test]
        public void Revive_Should_Not_Change_Generation_When_Status_Is_Alive()
        {
            _cell.Revive(); // Status = Alive  /  Generation = 1
            _cell.MoveToNextGeneration(); // Status = Alive / Generation = 2 
            _cell.Revive(); 
            // Generation should still be 2. Revive shouldn't change Generaion value while cell is alive.
            Assert.AreEqual(_cell.Generation, 2);
        }

        [Test]
        public void When_MoveToNextGeneration_Called_Generation_Increase_By_One()
        {
            var beforeGeneration = _cell.Generation;
            _cell.MoveToNextGeneration();
            Assert.AreEqual(_cell.Generation, beforeGeneration + 1);
        }

        [Test]
        public void MoveToNextGeneration_Should_Return_Generation_After_Increaseing()
        {
            _cell.Revive(); // 1
            _cell.MoveToNextGeneration(); // 2
            Assert.AreEqual(_cell.MoveToNextGeneration(), 3);
        }

        [Test]
        public void When_Cell_Killed_Status_Is_Dead()
        {
            _cell.Kill();
            Assert.AreEqual(_cell.Status, CellStatus.Dead);
        }

        [Test]
        public void When_Cell_Killed_Generation_Set_To_Zero()
        {
            _cell.Revive();
            _cell.MoveToNextGeneration();
            _cell.Kill();
            Assert.AreEqual(_cell.Generation, 0);
        }

        [Test]
        public void Property_TimesRevived_Returns_Number_Of_Revives()
        {
            for (var i = 0; i < 3; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesRevived, 3);
        }

        [Test]
        public void Property_TimesKilled_Returns_Number_Of_Kills()
        {
            for (var i = 0; i < 3; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesKilled, 3);
        }

        [Test]
        public void OnRevived_Event_Fire_When_Cell_Revived()
        {
            var fired = false;
            _cell.OnRevived += (sender, arg) => fired = true;
            _cell.Revive();
            Assert.IsTrue(fired);
        }

        [Test]
        public void OnKilled_Event_Fire_When_Cell_Killed()
        {
            var fired = false;
            _cell.OnKilled += (sender, arg) => fired = true;
            _cell.Kill();
            Assert.IsTrue(fired);
        }
    }
}
