// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageResource.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The image resource.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Resources
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The image resource.
    /// </summary>
    public abstract class ImageResource : Resource<Image>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResource"/> class.
        /// </summary>
        protected ImageResource()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResource"/> class.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        protected ImageResource(Image image)
            : base(image)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="ImageResource"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static ImageResource FromImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            ImageResource resource = new FromImageResource(image);
            return resource;
        }

        #endregion

        /// <summary>
        /// The from image resource.
        /// </summary>
        private sealed class FromImageResource : ImageResource
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="FromImageResource"/> class.
            /// </summary>
            /// <param name="image">
            /// The image.
            /// </param>
            public FromImageResource(Image image)
                : base(image)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// The load.
            /// </summary>
            /// <returns>
            /// The <see cref="Image"/>.
            /// </returns>
            protected override Image Load()
            {
                return (Image)this.Reference.Clone();
            }

            #endregion
        }
    }
}