// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerLocalLifestyle.cs" company="">
//   
// </copyright>
// <summary>
//   The per local lifestyle manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.LocalData
{
    using System;

    using Castle.MicroKernel;
    using Castle.MicroKernel.Lifestyle;

    /// <summary>
    /// The per local lifestyle manager.
    /// </summary>
    public class PerLocalLifestyleManager : AbstractLifestyleManager
    {
        #region Fields

        /// <summary>
        /// The per local object id.
        /// </summary>
        private readonly string PerLocalObjectID = "PerLocalLifestyleManager_" + Guid.NewGuid();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object Resolve(CreationContext context)
        {
            object instance = Local.Data[this.PerLocalObjectID];

            if (instance == null)
            {
                instance = base.Resolve(context);
                Local.Data[this.PerLocalObjectID] = instance;
            }

            return instance;
        }

        #endregion
    }
}