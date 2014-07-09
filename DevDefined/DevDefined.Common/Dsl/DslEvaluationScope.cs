// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DslEvaluationScope.cs" company="">
//   
// </copyright>
// <summary>
//   The dsl evaluation scope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System;

    using DevDefined.Common.LocalData;

    /// <summary>
    /// The dsl evaluation scope.
    /// </summary>
    public class DslEvaluationScope : IDisposable
    {
        #region Static Fields

        /// <summary>
        /// The current dsl evaluation scope key.
        /// </summary>
        private static string CurrentDslEvaluationScopeKey = "CurrentDslEvaluationScopeKey";

        #endregion

        #region Fields

        /// <summary>
        /// The _node writer.
        /// </summary>
        private readonly NodeWriter _nodeWriter;

        /// <summary>
        /// The _previous.
        /// </summary>
        private readonly DslEvaluationScope _previous;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DslEvaluationScope"/> class.
        /// </summary>
        /// <param name="nodeWriter">
        /// The node writer.
        /// </param>
        public DslEvaluationScope(NodeWriter nodeWriter)
        {
            this._nodeWriter = nodeWriter;
            this._previous = (DslEvaluationScope)Local.Data[CurrentDslEvaluationScopeKey];
            Local.Data[CurrentDslEvaluationScopeKey] = this;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        public static DslEvaluationScope Current
        {
            get
            {
                return (DslEvaluationScope)Local.Data[CurrentDslEvaluationScopeKey];
            }
        }

        /// <summary>
        /// Gets the node writer.
        /// </summary>
        public NodeWriter NodeWriter
        {
            get
            {
                return this._nodeWriter;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Local.Data[CurrentDslEvaluationScopeKey] = this._previous;
        }

        #endregion
    }
}