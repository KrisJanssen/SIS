// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   A static class that provides some common extensions for simplifying IO
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.IO
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// A static class that provides some common extensions for simplifying IO
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="serialized">
        /// The serialized.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Deserialize<T>(this byte[] serialized) where T : class
        {
            var stream = new MemoryStream(serialized);
            return stream.Enumerate<T>(true);
        }

        /// <summary>
        /// The enumerate.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Enumerate<T>(this Stream input) where T : class
        {
            return Enumerate<T>(input, true);
        }

        /// <summary>
        /// The enumerate.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="closeStream">
        /// The close stream.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static IEnumerable<T> Enumerate<T>(this Stream input, bool closeStream) where T : class
        {
            try
            {
                var bformatter = new BinaryFormatter();
                while (input.CanRead && (input.Position < input.Length))
                {
                    var t = bformatter.Deserialize(input) as T;
                    if (t != null)
                    {
                        yield return t;
                    }
                }
            }
            finally
            {
                if (closeStream)
                {
                    input.Close();
                }
            }
        }

        /// <summary>
        /// The flush.
        /// </summary>
        /// <param name="sequence">
        /// The sequence.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <param name="closeStream">
        /// The close stream.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
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
                if (closeStream)
                {
                    output.Close();
                }
            }
        }

        /// <summary>
        /// A handy utility method that allows you to flush the contents of one stream to another
        /// </summary>
        /// <param name="input">
        /// the input stream with the new Flush(...) method
        /// </param>
        /// <param name="output">
        /// the target for the contents of the input stream
        /// </param>
        /// <param name="closeInput">
        /// should the inputstream be closed when done?
        /// </param>
        /// <param name="closeOutput">
        /// should the outputstream be closed when done?
        /// </param>
        public static void Pipe(this Stream input, Stream output, bool closeInput, bool closeOutput)
        {
            var buf = new byte[1024];
            int c = 0;
            while ((c = input.Read(buf, 0, buf.Length)) > 0)
            {
                output.Write(buf, 0, c);
            }

            if (closeOutput)
            {
                output.Close();
            }

            if (closeInput)
            {
                input.Close();
            }
        }

        /// <summary>
        /// Overload which closes both input and output streams.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        public static void Pipe(this Stream source, Stream destination)
        {
            Pipe(source, destination, true, true);
        }

        /// <summary>
        /// The read to end as bytes.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
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

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="sequence">
        /// The sequence.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Serialize<T>(this IEnumerable<T> sequence) where T : class
        {
            var stream = new MemoryStream();
            sequence.Flush(stream, true);
            return stream.ToArray();
        }

        #endregion
    }
}