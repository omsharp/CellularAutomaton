using CellularAutomaton;
using NUnit.Framework;

namespace CellularAutomatonTests
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
        public void Cell_FirstCreated_GenerationIsZero()
        {
            Assert.AreEqual(_cell.Generation, 0);
        }

        [Test]
        public void Status_CellFirstCreated_StatusIsInactive()
        {
            Assert.AreEqual(CellStatus.Inactive, _cell.Status);
        }

        [Test]
        public void Revive_StatusNotAlive_ChangeStatusToAlive()
        {
            _cell.Revive();
            Assert.AreEqual(_cell.Status, CellStatus.Alive);
        }

        [Test]
        public void Kill_StatusNotDead_ChangeStatusToDead()
        {
            _cell.Revive();
            _cell.Kill();
            Assert.AreEqual(_cell.Status, CellStatus.Dead);
        }

        [Test]
        public void Revive_StatusNotAlive_GenerationSetToOne()
        {
            _cell.Kill();
            _cell.Revive();
            Assert.AreEqual(_cell.Generation, 1);
        }

        [Test]
        public void Revive_StatusIsAlive_GenerationNotChanged()
        {
            _cell.Revive(); // Status = Alive  /  Generation = 1
            _cell.MoveToNextGeneration(); // Status = Alive / Generation = 2 
            _cell.Revive(); 
            // Generation should still be 2. Revive shouldn't change Generaion value while cell is alive.
            Assert.AreEqual(_cell.Generation, 2);
        }

        [Test]
        public void MoveToNextGeneration_NormalCall_GenerationIncreasedByOne()
        {
            var beforeGeneration = _cell.Generation;
            _cell.Revive();  // Generation = 1
            _cell.MoveToNextGeneration();  // Generation = 2
            Assert.AreEqual(_cell.Generation, beforeGeneration + 2);
        }

        [Test]
        public void MoveToNextGeneration_NormalCall_ReturnsGenerationAfterIncreaseing()
        {
            _cell.Revive(); // 1
            _cell.MoveToNextGeneration(); // 2
            Assert.AreEqual(_cell.MoveToNextGeneration(), 3);
        }

        [Test]
        public void MoveToNextGeneration_StatusIsDead_ThrowException()
        {
            _cell.Kill();
            Assert.Throws<MovingToNextGenerationFailedException>(() => _cell.MoveToNextGeneration());
        }

        [Test]
        public void Kill_GenerationIsNotZero_SetGenerationToZero()
        {
            _cell.Revive();
            _cell.MoveToNextGeneration();
            _cell.Kill();
            Assert.AreEqual(_cell.Generation, 0);
        }

        [Test]
        public void TimesRevived_Get_NumberOfRevives()
        {
            for (var i = 0; i < 5; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesRevived, 5);
        }

        [Test]
        public void TimesKilled_Get_NumberOfKills()
        {
            for (var i = 0; i < 5; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesKilled, 5);
        }

        [Test]
        public void Revive_StatusNotAlive_FireRevivedEvent()
        {
            var fired = false;
            _cell.Revived += (sender, arg) => fired = true;
            _cell.Revive();
            Assert.That(fired, Is.True.After(500));
        }

        [Test]
        public void Kill_StatusIsNotDead_FireKilledEvent()
        {
            var fired = false;
            _cell.Killed += (sender, arg) => fired = true;
            _cell.Kill();
            Assert.That(fired,Is.True.After(500));
        }

        [Test]
        public void ToString_NormalCall_ReturnsIdBasedOnRowAndColumn()
        {
            var actual   = _cell.ToString();
            var expected = string.Format("[{0},{1}]", _cell.Row, _cell.Column);
            StringAssert.AreEqualIgnoringCase(actual,expected);
        }
    }

}
