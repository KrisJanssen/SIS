using System.Collections.Generic;

namespace DevDefined.Common.Dsl
{
    public interface INode
    {
        INode Parent { get; set; }
        List<INode> Nodes { get; }
    }
}