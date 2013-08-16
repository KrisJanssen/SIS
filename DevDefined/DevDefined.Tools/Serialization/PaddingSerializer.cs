using System;
using System.Text;
using System.Windows.Forms;

namespace DevDefined.Tools.Serialization
{
    /// <summary>
    /// A class which can serialize a <see cref="Padding" /> instance to and from a simple
    /// string representation.
    /// </summary>
    public static class PaddingSerializer
    {
        private const char PaddingSeperator = ',';

        /// <summary>
        /// Deserializes the padding.
        /// </summary>
        /// <param name="encodedValue">The encoded value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Padding DeserializePadding(string encodedValue, Padding defaultValue)
        {
            if (!string.IsNullOrEmpty(encodedValue))
            {
                var intValues = new int[4];
                string[] values = encodedValue.Split(new[] {PaddingSeperator});

                if (values.Length >= 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!Int32.TryParse(values[i], out intValues[i]))
                        {
                            goto returnDefault;
                        }
                    }

                    return new Padding(intValues[0], intValues[1], intValues[2], intValues[3]);
                }
            }

            returnDefault:

            return defaultValue;
        }

        /// <summary>
        /// Serializes the padding.
        /// </summary>
        /// <param name="padding">The padding.</param>
        /// <returns></returns>
        public static string SerializePadding(Padding padding)
        {
            var builder = new StringBuilder();
            builder.Append(padding.Left);
            builder.Append(PaddingSeperator);
            builder.Append(padding.Top);
            builder.Append(PaddingSeperator);
            builder.Append(padding.Right);
            builder.Append(PaddingSeperator);
            builder.Append(padding.Bottom);

            return builder.ToString();
        }
    }
}