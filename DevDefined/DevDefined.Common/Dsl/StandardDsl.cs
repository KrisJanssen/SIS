// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardDsl.cs" company="">
//   
// </copyright>
// <summary>
//   The standard dsl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System;
    using System.Linq;

    /// <summary>
    /// The standard dsl.
    /// </summary>
    public class StandardDsl
    {
        #region Fields

        /// <summary>
        /// The _batches.
        /// </summary>
        private Batch[] _batches;

        /// <summary>
        /// The _execute lock.
        /// </summary>
        private object _executeLock = new Object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the node writer.
        /// </summary>
        protected NodeWriter NodeWriter
        {
            get
            {
                return DslEvaluationScope.Current.NodeWriter;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="dsl">
        /// The dsl.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator Batch[](StandardDsl dsl)
        {
            Batch batch = delegate { return dsl._batches; };

            batch.Ignore();

            return new[] { batch };
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="batches">
        /// The batches.
        /// </param>
        public void Add(params Batch[] batches)
        {
            if (this._batches == null)
            {
                this._batches = batches;
            }
            else
            {
                this._batches = this._batches.Concat(batches).ToArray();
            }
        }

        /// <summary>
        /// The as.
        /// </summary>
        /// <param name="batches">
        /// The batches.
        /// </param>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public Batch[] As(params Batch[] batches)
        {
            Batch asBatch = delegate
                {
                    this.ExecuteBatches(batches);
                    return null;
                };

            asBatch.Ignore();

            return new[] { asBatch };
        }

        /// <summary>
        /// The execute.
        /// </summary>
        public void Execute()
        {
            this.ExecuteBatches(this._batches);
        }

        /// <summary>
        /// The text.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public Batch[] Text(string value)
        {
            return new Batch[] { delegate
                {
                    this.NodeWriter.WriteNode(new TextNode(value));
                    return null;
                } };
        }

        #endregion

        #region Methods

        /// <summary>
        /// The execute batch.
        /// </summary>
        /// <param name="batch">
        /// The batch.
        /// </param>
        protected void ExecuteBatch(Batch batch)
        {
            this.WriteStart(batch);
            Batch[] batches = batch(null);
            this.ExecuteBatches(batches);
            this.WriteEnd(batch);
        }

        /// <summary>
        /// The execute batches.
        /// </summary>
        /// <param name="batches">
        /// The batches.
        /// </param>
        protected void ExecuteBatches(Batch[] batches)
        {
            if (batches != null)
            {
                foreach (Batch batch in batches)
                {
                    this.ExecuteBatch(batch);
                }
            }
        }

        /// <summary>
        /// The write end.
        /// </summary>
        /// <param name="batch">
        /// The batch.
        /// </param>
        protected void WriteEnd(Batch batch)
        {
            if (batch.IsIgnored())
            {
                return;
            }

            string name = batch.Method.GetParameters()[0].Name;
            if (!string.IsNullOrEmpty(name))
            {
                this.NodeWriter.WriteEndNode();
            }
        }

        /// <summary>
        /// The write start.
        /// </summary>
        /// <param name="batch">
        /// The batch.
        /// </param>
        protected void WriteStart(Batch batch)
        {
            if (batch.IsIgnored())
            {
                return;
            }

            string name = batch.Method.GetParameters()[0].Name;
            if (!string.IsNullOrEmpty(name))
            {
                this.NodeWriter.WriteStartNode(new NamedNode(name));
            }
        }

        #endregion
    }
}