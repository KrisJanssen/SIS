using System;
using System.Linq;

namespace DevDefined.Common.Dsl
{
    public class StandardDsl
    {
        private Batch[] _batches;
        private Object _executeLock = new Object();

        protected NodeWriter NodeWriter
        {
            get { return DslEvaluationScope.Current.NodeWriter; }
        }

        public void Execute()
        {
            ExecuteBatches(_batches);
        }

        public void Add(params Batch[] batches)
        {
            if (_batches == null)
                _batches = batches;
            else
                _batches = _batches.Concat(batches).ToArray();
        }

        public Batch[] As(params Batch[] batches)
        {
            Batch asBatch = delegate
                                {
                                    ExecuteBatches(batches);
                                    return null;
                                };

            asBatch.Ignore();

            return new[] {asBatch};
        }

        public Batch[] Text(string value)
        {
            return new Batch[]
                       {
                           delegate
                               {
                                   NodeWriter.WriteNode(new TextNode(value));
                                   return null;
                               }
                       };
        }

        protected void ExecuteBatches(Batch[] batches)
        {
            if (batches != null)
                foreach (Batch batch in batches)
                {
                    ExecuteBatch(batch);
                }
        }

        protected void ExecuteBatch(Batch batch)
        {
            WriteStart(batch);
            Batch[] batches = batch(null);
            ExecuteBatches(batches);
            WriteEnd(batch);
        }

        protected void WriteStart(Batch batch)
        {
            if (batch.IsIgnored())
            {
                return;
            }
            string name = batch.Method.GetParameters()[0].Name;
            if (!string.IsNullOrEmpty(name)) NodeWriter.WriteStartNode(new NamedNode(name));
        }

        protected void WriteEnd(Batch batch)
        {
            if (batch.IsIgnored()) return;
            string name = batch.Method.GetParameters()[0].Name;
            if (!string.IsNullOrEmpty(name)) NodeWriter.WriteEndNode();
        }

        public static implicit operator Batch[](StandardDsl dsl)
        {
            Batch batch = delegate { return dsl._batches; };

            batch.Ignore();

            return new[] {batch};
        }
    }
}