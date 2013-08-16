using System;
using System.Collections.Generic;
using DevDefined.Common.LocalData;

namespace DevDefined.Common.Dsl
{
    public class ComponentEvaluationScope : IDisposable
    {
        private static string CurrentComponentEvaluationScopeKey = "ComponentEvaluationScopeKey";

        private readonly ComponentNode _componentNode;
        private readonly ComponentEvaluationScope _previous;
        private readonly Dictionary<string, object> _viewParameters = new Dictionary<string, object>();

        public ComponentEvaluationScope(ComponentNode componentNode)
        {
            _componentNode = componentNode;
            _previous = (ComponentEvaluationScope) Local.Data[CurrentComponentEvaluationScopeKey];
            Local.Data[CurrentComponentEvaluationScopeKey] = this;
        }

        public static ComponentEvaluationScope Current
        {
            get { return (ComponentEvaluationScope) Local.Data[CurrentComponentEvaluationScopeKey]; }
        }

        public ComponentNode ComponentNode
        {
            get { return _componentNode; }
        }

        public Dictionary<string, object> ViewParameters
        {
            get { return _viewParameters; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Local.Data[CurrentComponentEvaluationScopeKey] = _previous;
        }

        #endregion
    }
}