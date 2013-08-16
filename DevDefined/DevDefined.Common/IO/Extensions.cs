using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevDefined.Common.IO
{
    /// <summary>
    /// A static class that provides some common extensions for simplifying IO
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// A handy utility method that allows you to flush the contents of one stream to another
        /// </summary>
        /// <param name="input">the input stream with the new Flush(...) method</param>
        /// <param name="output">the target for the contents of the input stream</param>
        /// <param name="closeInput">should the inputstream be closed when done?</param>
        /// <param name="closeOutput">should the outputstream be closed when done?</param>
        public static void Pipe(this Stream input, Stream output, bool closeInput, bool closeOutput)
        {
            var buf = new byte[1024];
            int c = 0;
            while ((c = input.Read(buf, 0, buf.Length)) > 0)
            {
                output.Write(buf, 0, c);
            }
            if (closeOutput) output.Close();
            if (closeInput) input.Close();
        }

        /// <summary>
        /// Overload which closes both input and output streams.
        /// </summary>
        public static void Pipe(this Stream source, Stream destination)
        {
            Pipe(source, destination, true, true);
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Enumerate<T>(this Stream input) where T : class
        {
            return Enumerate<T>(input, true);
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Enumerate<T>(this Stream input, bool closeStream) where T : class
        {
            try
            {
                var bformatter = new BinaryFormatter();
                while (input.CanRead && (input.Position < input.Length))
                {
                    var t = bformatter.Deserialize(input) as T;
                    if (t != null) yield return t;
                }
            }
            finally
            {
                if (closeStream) input.Close();
            }
        }

        public static byte[] Serialize<T>(this IEnumerable<T> sequence) where T : class
        {
            var stream = new MemoryStream();
            sequence.Flush(stream, true);
            return stream.ToArray();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Deserialize<T>(this byte[] serialized) where T : class
        {
            var stream = new MemoryStream(serialized);
            return stream.Enumerate<T>(true);
        }

        public static void Flush<T>(this IEnumerable<T> sequence, Stream output, bool closeStream) where T : class
        {
            try
            {
                var bformatter = new BinaryFormatter();
                foreach (T t in sequence)
                {
                    bformatter.Serialize(output, t);
                }
            }
            finally
            {
                if (closeStream) output.Close();
            }
        }

        public static byte[] ReadToEndAsBytes(this Stream source)
        {
            var stream = new MemoryStream();

            try
            {
                source.Pipe(stream);
                return stream.ToArray();
            }
            finally
            {
                stream.Dispose();
            }
        }
    }
}