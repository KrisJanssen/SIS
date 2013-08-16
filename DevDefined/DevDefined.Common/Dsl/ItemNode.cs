using System;

namespace DevDefined.Common.Dsl
{
    public abstract class ItemNode : AbstractNode
    {
        public abstract string Evaluate(object source);
    }

    public class ItemNode<T> : ItemNode
    {
        public ItemNode()
        {
        }

        public ItemNode(Func<T, string> func)
        {
            Func = func;
        }

        public Func<T, string> Func { get; set; }

        public override string Evaluate(object source)
        {
            return Func((T) source);
        }
    }
}