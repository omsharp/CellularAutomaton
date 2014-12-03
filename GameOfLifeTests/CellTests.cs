using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void CreateNewCell()
        {
            var cell = new Cell();
        }
    }
}
