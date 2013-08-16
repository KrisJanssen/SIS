using System;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Configuration;
using Castle.Windsor.Installer;

namespace DevDefined.Common.ExtendedContainer
{
  public class ExtendedWindsorContainer : WindsorContainer
  {
    public ExtendedWindsorContainer(IConfigurationInterpreter interpreter)
      : base(CreateKernel(), new DefaultComponentInstaller())
    {
      if (interpreter == null) throw new ArgumentNullException("interpreter");

      interpreter.ProcessResource(interpreter.Source, Kernel.ConfigurationStore);

      RunInstaller();
    }

    static IKernel CreateKernel()
    {
      var kernel = new DefaultKernel();
      kernel.ComponentModelBuilder = new ExtendedComponentBuilder(kernel);
      return kernel;
    }
  }
}