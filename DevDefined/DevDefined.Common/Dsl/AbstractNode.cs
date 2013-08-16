using System.Collections.Generic;

namespace DevDefined.Common.Dsl
{
    public abstract class AbstractNode : INode
    {
        private readonly List<INode> _nodes = new List<INode>();

        #region INode Members

        public INode Parent { get; set; }

        public List<INode> Nodes
        {
            get { return _nodes; }
        }

        #endregion
    }
}