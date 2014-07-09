// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedComponentBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The extended component builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.ExtendedContainer
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.ModelBuilder;
    using Castle.MicroKernel.ModelBuilder.Inspectors;

    /// <summary>
    /// The extended component builder.
    /// </summary>
    public class ExtendedComponentBuilder : DefaultComponentModelBuilder
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedComponentBuilder"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public ExtendedComponentBuilder(IKernel kernel)
            : base(kernel)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The initialize contributors.
        /// </summary>
        protected override void InitializeContributors()
        {
            this.AddContributor(new GenericInspector());
            this.AddContributor(new ConfigurationModelInspector());
            this.AddContributor(new ExtendedConfigurationParametersInspector());
            this.AddContributor(new LifestyleModelInspector());
            this.AddContributor(new ConstructorDependenciesModelInspector());
            this.AddContributor(new PropertiesDependenciesModelInspector());
            this.AddContributor(new LifecycleModelInspector());
            this.AddContributor(new InterceptorInspector());
            this.AddContributor(new ComponentActivatorInspector());
            this.AddContributor(new ComponentProxyInspector());
        }

        #endregion
    }
}