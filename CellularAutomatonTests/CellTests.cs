using System;
using CellularAutomaton;
using NUnit.Framework;

namespace CellularAutomatonTests
{
    [TestFixture]
    public class CellTests
    {
        private Cell _cell;

        private const int ROW    = 2;
        private const int COLUMN = 4;

        [SetUp]
        public void Setup()
        {
            _cell = Cell.MakeCell(ROW, COLUMN);
        }

        [Test]
        public void MakeCell_NegativeArguments_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Cell.MakeCell(-1, -2));
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
        public void Kill_StatusIsDead_ThrowsException()
        {
            _cell.Kill();
            Assert.Throws<InvalidOperationException>(() => _cell.Kill());
        }

        [Test]
        public void Revive_StatusNotAlive_GenerationSetToOne()
        {
            _cell.Kill();
            _cell.Revive();
            Assert.AreEqual(_cell.Generation, 1);
        }

        [Test]
        public void Revive_StatusIsAlive_ThrowsException()
        {
            _cell.Revive();
            Assert.Throws<InvalidOperationException>(() => _cell.Revive());
        }

        [Test]
        public void MoveToNextGeneration_StatusIsAlive_GenerationIncreasedByOne()
        {
            var beforeGeneration = _cell.Generation;
            _cell.Revive();  // Generation = 1
            _cell.MoveToNextGeneration();  // Generation = 2
            Assert.AreEqual(_cell.Generation, beforeGeneration + 2);
        }

        [Test]
        public void MoveToNextGeneration_StatusIsAlive_ReturnsGenerationAfterIncreaseing()
        {
            _cell.Revive(); //  Status = Alive  .. Generation = 1
            _cell.MoveToNextGeneration(); //  Status = Alive  ..  Generation = 2
            Assert.AreEqual(_cell.MoveToNextGeneration(), 3);
        }

        [Test]
        public void MoveToNextGeneration_StatusIsInactive_ThrowsException()
        {
            // at this point Status = Incactive (newly created cell)
            Assert.Throws<InvalidOperationException>(() => _cell.MoveToNextGeneration());
        }

        [Test]
        public void MoveToNextGeneration_StatusIsDead_ThrowsException()
        {
            _cell.Kill(); // Status = Dead
            Assert.Throws<InvalidOperationException>(() => _cell.MoveToNextGeneration());
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
            var expected = 5;

            for (var i = 0; i < expected; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesRevived, expected);
        }

        [Test]
        public void TimesKilled_Get_NumberOfKills()
        {
            var expected = 5;

            for (var i = 0; i < expected; i++)
            {
                _cell.Revive();
                _cell.Kill();
            }

            Assert.AreEqual(_cell.TimesKilled, expected);
        }

        [Test]
        public void Revive_StatusNotAlive_FireRevivedEvent()
        {
            var fired = false;
            _cell.Revived += (sender, arg) => fired = true;
            _cell.Revive();
            Assert.That(fired, Is.True.After(200));
        }

        [Test]
        public void Kill_StatusIsNotDead_FireKilledEvent()
        {
            var fired = false;
            _cell.Killed += (sender, arg) => fired = true;
            _cell.Kill();
            Assert.That(fired,Is.True.After(200));
        }

        [Test]
        public void ToString_NormalCall_ReturnsIdBasedOnRowAndColumn()
        {
            var actual = _cell.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
 
        }
    }

}
