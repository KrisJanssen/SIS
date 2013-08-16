using System;
using System.Collections.Generic;

namespace DevDefined.Common.Dsl
{
    public class NodeWriter
    {
        private readonly List<INode> _rootNodes = new List<INode>();
        private INode _currentNode;
        private int _ignoreWriteEndCount;

        public IEnumerable<INode> Nodes
        {
            get { return _rootNodes; }
        }

        public INode CurrentNode
        {
            get { return _currentNode; }
        }

        public string ExtractName
        {
            get
            {
                var namedNode = _currentNode as NamedNode;

                if (namedNode != null)
                {
                    _ignoreWriteEndCount++;
                    _currentNode = _currentNode.Parent;
                    _currentNode.Nodes.Remove(namedNode);
                    return namedNode.Name;
                }

                throw new InvalidOperationException("The CurrentNode is not of type NamedNode, and can not be popped to fetch the name");
            }
        }

        public NodeWriter WriteStartNode(INode node)
        {
            if (_currentNode != null)
            {
                _currentNode.Nodes.Add(node);
                node.Parent = _currentNode;
            }
            else
            {
                _rootNodes.Add(node);
            }

            _currentNode = node;

            return this;
        }

        public NodeWriter WriteNode(INode node)
        {
            if (_currentNode != null)
            {
                _currentNode.Nodes.Add(node);
            }
            else
            {
                _rootNodes.Add(node);
            }

            return this;
        }

        public NodeWriter WriteEndNode()
        {
            if (_ignoreWriteEndCount > 0)
            {
                _ignoreWriteEndCount--;
            }
            else
            {
                if (_currentNode == null) throw new InvalidOperationException("WriteEndNode called does not match any oustanding call to WriteStartNode");
                _currentNode = _currentNode.Parent;
            }

            return this;
        }
    }
}