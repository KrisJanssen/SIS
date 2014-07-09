// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisposableAction.cs" company="">
//   
// </copyright>
// <summary>
//   The disposable action.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common
{
    using System;

    /// <summary>
    /// The disposable action.
    /// </summary>
    public class DisposableAction : IDisposable
    {
        #region Fields

        /// <summary>
        /// The _action.
        /// </summary>
        private readonly Action _action;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableAction"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public DisposableAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this._action = action;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this._action();
        }

        #endregion
    }
}