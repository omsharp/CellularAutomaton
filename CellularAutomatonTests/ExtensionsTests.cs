using CellularAutomaton;
using NUnit.Framework;

namespace CellularAutomatonTests
{
    public class ExtensionsTests
    {
        [Test]
        public void Clone_Serializable_ReturnsCloneOfSource()
        {
            var original = Cell.MakeCell(3, 9);
            original.Revive();
            original.Evolve();

            var copy = original.Clone();

            Assert.AreEqual(original.Row, copy.Row);
            Assert.AreEqual(original.Column, copy.Column);
            Assert.AreEqual(original.Status, copy.Status);
            Assert.AreEqual(original.Generation, copy.Generation);
        }

        [Test]
        public void Clone_Serializeable_CloneDoesNotReferenceEqualsSource()
        {
            var origianl = Cell.MakeCell(3, 9);
            var copy = origianl.Clone();

            Assert.IsFalse(ReferenceEquals(origianl, copy));
        }

        

    }
}
