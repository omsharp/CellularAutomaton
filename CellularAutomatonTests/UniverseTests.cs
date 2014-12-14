using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CellularAutomaton;
using Moq;
using NUnit.Framework;


namespace CellularAutomatonTests
{
    [TestFixture]
    public class UniverseTests
    {
        private const int ROWS_COUNT    = 12;
        private const int COLUMNS_COUNT = 16;
        
        private Universe            _universe;
        private Mock<ICellularGrid> _gridMock;
 
        [SetUp]
        public void Setup()
        {
            _gridMock = new Mock<ICellularGrid>(MockBehavior.Strict);

            _gridMock.SetupGet(g => g.RowsCount).Returns(ROWS_COUNT);
            _gridMock.SetupGet(g => g.ColumnsCount).Returns(COLUMNS_COUNT);

            //setup cells
            _gridMock.SetupGet(g => g.Cells).Returns(() =>
            {
                var list = new List<Cell>();

                for (var row = 0; row < ROWS_COUNT; row++)
                {
                    for (var column = 0; column < COLUMNS_COUNT; column++)
                    {
                        list.Add(Cell.MakeCell(row, column));
                    }
                }

                return list.AsEnumerable();
            });
            

            TypeDescriptor.AddAttributes(_gridMock.Object, new SerializableAttribute());

            _universe = Universe.MakeUniverse(_gridMock.Object);
        }

        [Test]
        public void MakeUniverse_ArgumentLessThanOne_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Universe.MakeUniverse(null));
        }

      
        [Test]
        public void Rules_Get_ReturnsCollectionOfIRules()
        {
            CollectionAssert.AllItemsAreInstancesOfType(_universe.Rules, typeof(IRule));
        }

        

        [Test]
        public void NextCycle_NormalCall_FiresCycleFinishedEvent()
        {
            var fired = false;

            _universe.CycleFinished += (sender, e) => fired = true;
            _universe.NextCycle();
            
            Assert.That(fired, Is.True.After(200));
        }

        [Test]
        public void NextCycle_NormalCall_AgeIncreasedByOne()
        {
            var oldAge = _universe.Age;

            _universe.NextCycle();
            
            Assert.AreEqual(_universe.Age, oldAge + 1);
        }

        [Test]
        public void NextCycle_RulesNotOverlapping_ApplyRules()
        {
            var reviveRule  = new Mock<IRule>(MockBehavior.Strict);
            var killRule    = new Mock<IRule>(MockBehavior.Strict);
            var evolvedRule = new Mock<IRule>(MockBehavior.Strict);
            
            reviveRule.Setup(r => r.Transform(It.IsAny<ICellularGrid>()))
                      .Returns<ICellularGrid>(grid =>
                      {
                          foreach (var cell in grid.Cells.Where(c => c.Row % 3 == 0))
                          {
                              cell.Revive();
                          }

                          return grid;
                      });

            killRule.Setup(r => r.Transform(It.IsAny<ICellularGrid>()))
                    .Returns<ICellularGrid>(grid =>
                    {
                        foreach (var cell in grid.Cells.Where(c => c.Row == 10))
                        {
                            cell.Kill();
                        }

                        return grid;
                    });

            evolvedRule.Setup(r => r.Transform(It.IsAny<CellularGrid>()))
                       .Returns<ICellularGrid>(grid =>
                       {
                           foreach (var cell in grid.Cells.Where(c => c.Column == 4 && 
                                                                      c.Row % 3 != 0 && 
                                                                      c.Row != 10))
                           {
                               cell.Revive();
                               cell.Evolve();
                           }

                           return grid;
                       });

            _universe.Rules.Add(reviveRule.Object);
            _universe.Rules.Add(killRule.Object);
            _universe.Rules.Add(evolvedRule.Object);

            _universe.NextCycle();

            var firstApplied = _universe.Grid.Cells.Where(c => c.Row % 3 == 0)
                                                   .All(c => c.Status == CellStatus.Alive);

            var secondApplied = _universe.Grid.Cells.Where(c => c.Row == 10)
                                                    .All(c => c.Status == CellStatus.Dead);

            var thirdApplied = _universe.Grid.Cells.Where(c => c.Row == 7 && c.Column == 3)
                                                   .All(c => c.Status == CellStatus.Alive && c.Generation == 2);


            Assert.IsTrue(secondApplied);
            Assert.IsTrue(firstApplied);
            Assert.IsTrue(thirdApplied);
        }

        [Test]
        public void ToString_NormalCall_SomeString()
        {
            var actual = _universe.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

     
    }
}