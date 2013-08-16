using System;
using System.Reflection;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Util;
using DevDefined.Common.Reflection;

namespace DevDefined.Common.ExtendedContainer
{
  public class ExtendedConfigurationParametersInspector : IContributeComponentModelConstruction
  {
    #region IContributeComponentModelConstruction Members

    public virtual void ProcessModel(IKernel kernel, ComponentModel model)
    {
      if (model.Configuration == null) return;

      IConfiguration parameters = model.Configuration.Children["parameters"];

      if (parameters == null) return;

      foreach (IConfiguration parameter in parameters.Children)
      {
        String name = parameter.Name;
        String value = parameter.Value;

        if (value == null && parameter.Children.Count != 0)
        {
          IConfiguration parameterValue = parameter.Children[0];
          model.Parameters.Add(name, parameterValue);
        }
        else
        {
          if (parameter.Attributes["type"] == "static")
          {
            value = StaticReflectionHelper.GetStaticValue<string>(parameter.Value);
          }

          model.Parameters.Add(name, value);
        }
      }

      foreach (ParameterModel parameter in model.Parameters)
      {
        if (parameter.Value == null || !ReferenceExpressionUtil.IsReference(parameter.Value))
        {
          continue;
        }

        String newKey = ReferenceExpressionUtil.ExtractComponentKey(parameter.Value);

        model.Dependencies.Add(new DependencyModel(DependencyType.ServiceOverride, newKey, null, false));
      }
    }
    
    #endregion
  }
}