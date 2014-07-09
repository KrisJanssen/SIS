// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyItem2.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Re-implements System.Drawing.PropertyItem so that the data is serializable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Re-implements System.Drawing.PropertyItem so that the data is serializable.
    /// </summary>
    [Serializable]
    internal sealed class PropertyItem2
    {
        #region Constants

        /// <summary>
        /// The id property name.
        /// </summary>
        private const string idPropertyName = "id";

        /// <summary>
        /// The len property name.
        /// </summary>
        private const string lenPropertyName = "len";

        /// <summary>
        /// The pi element name.
        /// </summary>
        private const string piElementName = "exif";

        /// <summary>
        /// The type property name.
        /// </summary>
        private const string typePropertyName = "type";

        /// <summary>
        /// The value property name.
        /// </summary>
        private const string valuePropertyName = "value";

        #endregion

        #region Static Fields

        /// <summary>
        /// The property item image.
        /// </summary>
        private static Image propertyItemImage;

        #endregion

        #region Fields

        /// <summary>
        /// The id.
        /// </summary>
        private int id;

        /// <summary>
        /// The len.
        /// </summary>
        private int len;

        /// <summary>
        /// The type.
        /// </summary>
        private short type;

        /// <summary>
        /// The value.
        /// </summary>
        private byte[] value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyItem2"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="len">
        /// The len.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public PropertyItem2(int id, int len, short type, byte[] value)
        {
            this.id = id;
            this.len = len;
            this.type = type;

            if (value == null)
            {
                this.value = new byte[0];
            }
            else
            {
                this.value = (byte[])value.Clone();
            }

            if (len != this.value.Length)
            {
                Tracing.Ping("len != value.Length: id=" + id + ", type=" + type);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets the len.
        /// </summary>
        public int Len
        {
            get
            {
                return this.len;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public short Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public byte[] Value
        {
            get
            {
                return (byte[])this.value.Clone();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from blob.
        /// </summary>
        /// <param name="blob">
        /// The blob.
        /// </param>
        /// <returns>
        /// The <see cref="PropertyItem2"/>.
        /// </returns>
        public static PropertyItem2 FromBlob(string blob)
        {
            PropertyItem2 pi2;

            if (blob.Length > 0 && blob[0] == '<')
            {
                string idStr = GetProperty(blob, idPropertyName);
                string lenStr = GetProperty(blob, lenPropertyName);
                string typeStr = GetProperty(blob, typePropertyName);
                string valueStr = GetProperty(blob, valuePropertyName);

                int id = int.Parse(idStr, CultureInfo.InvariantCulture);
                int len = int.Parse(lenStr, CultureInfo.InvariantCulture);
                short type = short.Parse(typeStr, CultureInfo.InvariantCulture);
                byte[] value = Convert.FromBase64String(valueStr);

                pi2 = new PropertyItem2(id, len, type, value);
            }
            else
            {
                // Old way of serializing: .NET serialized!
                byte[] bytes = Convert.FromBase64String(blob);
                MemoryStream ms = new MemoryStream(bytes);
                BinaryFormatter bf = new BinaryFormatter();
                SerializationFallbackBinder sfb = new SerializationFallbackBinder();
                sfb.AddAssembly(Assembly.GetExecutingAssembly());
                bf.Binder = sfb;
                pi2 = (PropertyItem2)bf.Deserialize(ms);
            }

            return pi2;
        }

        /// <summary>
        /// The from property item.
        /// </summary>
        /// <param name="pi">
        /// The pi.
        /// </param>
        /// <returns>
        /// The <see cref="PropertyItem2"/>.
        /// </returns>
        public static PropertyItem2 FromPropertyItem(PropertyItem pi)
        {
            return new PropertyItem2(pi.Id, pi.Len, pi.Type, pi.Value);
        }

        /// <summary>
        /// The to blob.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToBlob()
        {
            string blob = string.Format(
                "<{0} {1}=\"{2}\" {3}=\"{4}\" {5}=\"{6}\" {7}=\"{8}\" />", 
                piElementName, 
                idPropertyName, 
                this.id.ToString(CultureInfo.InvariantCulture), 
                lenPropertyName, 
                this.len.ToString(CultureInfo.InvariantCulture), 
                typePropertyName, 
                this.type.ToString(CultureInfo.InvariantCulture), 
                valuePropertyName, 
                Convert.ToBase64String(this.value));

            return blob;
        }

        /// <summary>
        /// The to property item.
        /// </summary>
        /// <returns>
        /// The <see cref="PropertyItem"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public PropertyItem ToPropertyItem()
        {
            PropertyItem pi = GetPropertyItem();

            pi.Id = this.Id;
            pi.Len = this.Len;
            pi.Type = this.Type;
            pi.Value = this.Value;

            return pi;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get property.
        /// </summary>
        /// <param name="blob">
        /// The blob.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetProperty(string blob, string propertyName)
        {
            string findMe = propertyName + "=\"";
            int startIndex = blob.IndexOf(findMe) + findMe.Length;
            int endIndex = blob.IndexOf("\"", startIndex);
            string propertyValue = blob.Substring(startIndex, endIndex - startIndex);
            return propertyValue;
        }

        /// <summary>
        /// The get property item.
        /// </summary>
        /// <returns>
        /// The <see cref="PropertyItem"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static PropertyItem GetPropertyItem()
        {
            if (propertyItemImage == null)
            {
                Stream stream =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("PaintDotNet.SystemLayer.PropertyItem.png");
                propertyItemImage = Image.FromStream(stream);
            }

            PropertyItem pi = propertyItemImage.PropertyItems[0];
            pi.Id = 0;
            pi.Len = 0;
            pi.Type = 0;
            pi.Value = new byte[0];

            return pi;
        }

        #endregion
    }
}