using Castle.Core.Resource;
using Castle.MicroKernel;
using Castle.Windsor.Configuration.Interpreters;
using DevDefined.Common.ExtendedContainer;
using NUnit.Framework;

namespace DevDefined.Common.Tests.ExtendedContainer
{
  [TestFixture]
  public class ExtendedWindsorContainerTests
  {
    [Test]
    public void WhenValueOfTypeIsStatic_LooksUpValueFromStaticField()
    {
      // this code was written to support an answer to a stack overflow question
      // as seen here: http://stackoverflow.com/questions/381734/castle-windsor-how-to-specify-a-runtime-value-as-a-parameter-e-g-value-returne#384341

      var kernel = new DefaultKernel();
      kernel.ComponentModelBuilder = new ExtendedComponentBuilder(kernel);

      var container = new ExtendedWindsorContainer(
        new XmlInterpreter(
          new StaticContentResource(
            @"
<castle>
  <components>
    <component id=""test""
           type=""DevDefined.Common.Tests.ExtendedContainer.TestComponent,DevDefined.Common.Tests"">
      <parameters>
        <value type=""static"">DevDefined.Common.Tests.ExtendedContainer.StaticConfig.TheValue, DevDefined.Common.Tests</value>
      </parameters>
    </component>
  </components>
</castle>
")));

      var component = container.Resolve<TestComponent>();

      Assert.AreEqual("Success", component.Value);
    }
  }
}