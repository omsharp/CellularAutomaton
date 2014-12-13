using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CellularAutomaton
{
    public static class Extensions
    {

        /// <summary>
        /// Extension method. Clones an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="source">The object to be cloned.</param>
        /// <returns>A clone of the source object.</returns>
        public static T Clone<T>(this T source)
        {
            if ((typeof(T).IsSerializable == false))
                throw new ArgumentException("The type must be serializable", "source");

            if (ReferenceEquals(source, null))
                throw new ArgumentNullException("source", "You can't clone null!");

            var formatter = new BinaryFormatter();
            var stream    = new MemoryStream();

            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
