// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedConfigurationParametersInspector.cs" company="">
//   
// </copyright>
// <summary>
//   The extended configuration parameters inspector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.ExtendedContainer
{
    using Castle.Core;
    using Castle.Core.Configuration;
    using Castle.MicroKernel;
    using Castle.MicroKernel.ModelBuilder;
    using Castle.MicroKernel.Util;

    using DevDefined.Common.Reflection;

    /// <summary>
    /// The extended configuration parameters inspector.
    /// </summary>
    public class ExtendedConfigurationParametersInspector : IContributeComponentModelConstruction
    {
        #region Public Methods and Operators

        /// <summary>
        /// The process model.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public virtual void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (model.Configuration == null)
            {
                return;
            }

            IConfiguration parameters = model.Configuration.Children["parameters"];

            if (parameters == null)
            {
                return;
            }

            foreach (IConfiguration parameter in parameters.Children)
            {
                string name = parameter.Name;
                string value = parameter.Value;

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

                string newKey = ReferenceExpressionUtil.ExtractComponentKey(parameter.Value);

                model.Dependencies.Add(new DependencyModel(DependencyType.ServiceOverride, newKey, null, false));
            }
        }

        #endregion
    }
}