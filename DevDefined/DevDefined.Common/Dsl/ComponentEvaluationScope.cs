// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentEvaluationScope.cs" company="">
//   
// </copyright>
// <summary>
//   The component evaluation scope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System;
    using System.Collections.Generic;

    using DevDefined.Common.LocalData;

    /// <summary>
    /// The component evaluation scope.
    /// </summary>
    public class ComponentEvaluationScope : IDisposable
    {
        #region Static Fields

        /// <summary>
        /// The current component evaluation scope key.
        /// </summary>
        private static string CurrentComponentEvaluationScopeKey = "ComponentEvaluationScopeKey";

        #endregion

        #region Fields

        /// <summary>
        /// The _component node.
        /// </summary>
        private readonly ComponentNode _componentNode;

        /// <summary>
        /// The _previous.
        /// </summary>
        private readonly ComponentEvaluationScope _previous;

        /// <summary>
        /// The _view parameters.
        /// </summary>
        private readonly Dictionary<string, object> _viewParameters = new Dictionary<string, object>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentEvaluationScope"/> class.
        /// </summary>
        /// <param name="componentNode">
        /// The component node.
        /// </param>
        public ComponentEvaluationScope(ComponentNode componentNode)
        {
            this._componentNode = componentNode;
            this._previous = (ComponentEvaluationScope)Local.Data[CurrentComponentEvaluationScopeKey];
            Local.Data[CurrentComponentEvaluationScopeKey] = this;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        public static ComponentEvaluationScope Current
        {
            get
            {
                return (ComponentEvaluationScope)Local.Data[CurrentComponentEvaluationScopeKey];
            }
        }

        /// <summary>
        /// Gets the component node.
        /// </summary>
        public ComponentNode ComponentNode
        {
            get
            {
                return this._componentNode;
            }
        }

        /// <summary>
        /// Gets the view parameters.
        /// </summary>
        public Dictionary<string, object> ViewParameters
        {
            get
            {
                return this._viewParameters;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Local.Data[CurrentComponentEvaluationScopeKey] = this._previous;
        }

        #endregion
    }
}