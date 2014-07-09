// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="">
//   
// </copyright>
// <summary>
//   The resource.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Resources
{
    /// <summary>
    /// The resource.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class Resource<T>
    {
        #region Fields

        /// <summary>
        /// The resource.
        /// </summary>
        protected T resource;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}"/> class.
        /// </summary>
        protected Resource()
        {
            this.resource = default(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}"/> class.
        /// </summary>
        /// <param name="resource">
        /// The resource.
        /// </param>
        protected Resource(T resource)
        {
            this.resource = resource;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the reference.
        /// </summary>
        public T Reference
        {
            get
            {
                if (this.resource == null)
                {
                    this.resource = this.Load();
                }

                return this.resource;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get copy.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetCopy()
        {
            return this.Load();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The load.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected abstract T Load();

        #endregion
    }
}