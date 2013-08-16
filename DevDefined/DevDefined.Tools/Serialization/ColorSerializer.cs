using System;
using System.Drawing;

namespace DevDefined.Tools.Serialization
{
    /// <summary>
    /// The type of Color
    /// </summary>
    public enum ColorFormat
    {
        /// <summary>
        /// A well known named color.
        /// </summary>         
        NamedColor,

        /// <summary>
        /// A color made up of ARGB components.
        /// </summary>
        ARGBColor
    }

    /// <summary>
    /// Deserializes a color from a string.
    /// </summary>
    public static class ColorSerializer
    {
        /// <summary>
        /// Deserializes the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static Color DeserializeColor(string color)
        {
            string[] pieces = color.Split(new[] {':'});

            var colorType = (ColorFormat)
                            Enum.Parse(typeof (ColorFormat), pieces[0], true);

            switch (colorType)
            {
                case ColorFormat.NamedColor:
                    return Color.FromName(pieces[1]);

                case ColorFormat.ARGBColor:
                    byte a, r, g, b;
                    a = byte.Parse(pieces[1]);
                    r = byte.Parse(pieces[2]);
                    g = byte.Parse(pieces[3]);
                    b = byte.Parse(pieces[4]);

                    return Color.FromArgb(a, r, g, b);
            }

            return Color.Empty;
        }

        /// <summary>
        /// Serializes the color to a string.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static string SerializeColor(Color color)
        {
            if (color.IsNamedColor)
                return string.Format("{0}:{1}",
                                     ColorFormat.NamedColor, color.Name);
            else
                return string.Format("{0}:{1}:{2}:{3}:{4}",
                                     ColorFormat.ARGBColor,
                                     color.A, color.R, color.G, color.B);
        }
    }
}