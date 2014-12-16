using System;
using CellularAutomaton;
using NUnit.Framework;

namespace CellularAutomatonTests
{
    [TestFixture]
    public class CellTests
    {
        private SquareCell _cell;

        private const int ROW    = 2;
        private const int COLUMN = 4;

        [SetUp]
        public void Setup()
        {
            _cell = new SquareCell(ROW, COLUMN);
        }

        [Test]
        public void Cell_FirstCreated_GenerationIsZero()
        {
            Assert.AreEqual(_cell.Generation, 0);
        }

        [Test]
        public void Revive_StatusNotAlive_ChangeAliveToTrue()
        {
            _cell.Revive();

            Assert.IsTrue(_cell.Alive);
        }

        [Test]
        public void Kill_StatusNotDead_ChangeAliveFalse()
        {
            _cell.Revive();
            _cell.Kill();

            Assert.IsFalse(_cell.Alive);
        }

        [Test]
        public void Kill_StatusIsDead_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _cell.Kill());
        }

        [Test]
        public void Revive_StatusNotAlive_GenerationSetToOne()
        {
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
        public void Evolve_StatusIsAlive_GenerationIncreasedByOne()
        {
            var beforeGeneration = _cell.Generation;

            _cell.Revive();  // Generation = 1
            _cell.Evolve();  // Generation = 2

            Assert.AreEqual(_cell.Generation, beforeGeneration + 2);
        }

        [Test]
        public void Evolve_StatusIsInactive_ThrowsException()
        {
            // at this point Status = Incactive (newly created cell)
            Assert.Throws<InvalidOperationException>(() => _cell.Evolve());
        }

        [Test]
        public void Kill_GenerationIsNotZero_SetGenerationToZero()
        {
            _cell.Revive();
            _cell.Evolve();
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
            _cell.Revive();
            _cell.Kill();

            Assert.That(fired,Is.True.After(200));
        }

        [Test]
        public void Evolve_Ok_FiredEvolvedEvent()
        {
            var fired = false;
            
            _cell.Evolved += (sender, arg) => fired = true;
            _cell.Revive();
            _cell.Evolve();
            
            Assert.That(fired, Is.True.After(200));
        }
        [Test]
        public void ToString_NormalCall_ReturnsIdBasedOnRowAndColumn()
        {
            var actual = _cell.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

    }

}
