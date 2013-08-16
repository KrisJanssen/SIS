using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.ModelBuilder.Inspectors;

namespace DevDefined.Common.ExtendedContainer
{
  public class ExtendedComponentBuilder : DefaultComponentModelBuilder
  {
    public ExtendedComponentBuilder(IKernel kernel) : base(kernel)
    {
    }

    protected override void InitializeContributors()
    {
      AddContributor(new GenericInspector());
      AddContributor(new ConfigurationModelInspector());
      AddContributor(new ExtendedConfigurationParametersInspector());
      AddContributor(new LifestyleModelInspector());
      AddContributor(new ConstructorDependenciesModelInspector());
      AddContributor(new PropertiesDependenciesModelInspector());
      AddContributor(new LifecycleModelInspector());
      AddContributor(new InterceptorInspector());
      AddContributor(new ComponentActivatorInspector());
      AddContributor(new ComponentProxyInspector());
    }
  }
}