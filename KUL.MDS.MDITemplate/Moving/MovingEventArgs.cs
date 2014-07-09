// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovingEventArgs.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The moving event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Moving
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The moving event args.
    /// </summary>
    public sealed class MovingEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// The rectangle.
        /// </summary>
        private Rectangle rectangle;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovingEventArgs"/> class.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        public MovingEventArgs(Rectangle rect)
        {
            this.rectangle = rect;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the rectangle.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }

            set
            {
                this.rectangle = value;
            }
        }

        #endregion
    }
}