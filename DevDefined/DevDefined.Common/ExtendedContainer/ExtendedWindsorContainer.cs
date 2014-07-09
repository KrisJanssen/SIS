// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedWindsorContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The extended windsor container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.ExtendedContainer
{
    using System;

    using Castle.MicroKernel;
    using Castle.Windsor;
    using Castle.Windsor.Configuration;
    using Castle.Windsor.Installer;

    /// <summary>
    /// The extended windsor container.
    /// </summary>
    public class ExtendedWindsorContainer : WindsorContainer
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedWindsorContainer"/> class.
        /// </summary>
        /// <param name="interpreter">
        /// The interpreter.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ExtendedWindsorContainer(IConfigurationInterpreter interpreter)
            : base(CreateKernel(), new DefaultComponentInstaller())
        {
            if (interpreter == null)
            {
                throw new ArgumentNullException("interpreter");
            }

            interpreter.ProcessResource(interpreter.Source, this.Kernel.ConfigurationStore);

            this.RunInstaller();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create kernel.
        /// </summary>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        private static IKernel CreateKernel()
        {
            var kernel = new DefaultKernel();
            kernel.ComponentModelBuilder = new ExtendedComponentBuilder(kernel);
            return kernel;
        }

        #endregion
    }
}