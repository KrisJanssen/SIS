using System;
using DevDefined.Common.LocalData;

namespace DevDefined.Common.Dsl
{
    public class DslEvaluationScope : IDisposable
    {
        private static string CurrentDslEvaluationScopeKey = "CurrentDslEvaluationScopeKey";

        private readonly NodeWriter _nodeWriter;
        private readonly DslEvaluationScope _previous;

        public DslEvaluationScope(NodeWriter nodeWriter)
        {
            _nodeWriter = nodeWriter;
            _previous = (DslEvaluationScope) Local.Data[CurrentDslEvaluationScopeKey];
            Local.Data[CurrentDslEvaluationScopeKey] = this;
        }

        public static DslEvaluationScope Current
        {
            get { return (DslEvaluationScope) Local.Data[CurrentDslEvaluationScopeKey]; }
        }

        public NodeWriter NodeWriter
        {
            get { return _nodeWriter; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Local.Data[CurrentDslEvaluationScopeKey] = _previous;
        }

        #endregion
    }
}