using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DevDefined.Common.Dsl
{
    public static class DslToXml
    {
        public static string ToXml(StandardDsl dsl)
        {
            var nodeWriter = new NodeWriter();

            using (var scope = new DslEvaluationScope(nodeWriter))
            {
                dsl.Execute();
            }

            return ToXml(nodeWriter);
        }

        public static string ToXml(NodeWriter nodeWriter)
        {
            var builder = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(builder))
            {
                WriteXml(writer, nodeWriter.Nodes);
            }

            return builder.ToString();
        }

        public static void WriteXml(XmlWriter writer, IEnumerable<INode> nodes)
        {
            foreach (INode node in nodes)
            {
                if (node is NamedNode)
                {
                    writer.WriteStartElement(((NamedNode) node).Name);
                    WriteXml(writer, node.Nodes);
                    writer.WriteEndElement();
                }
                else if (node is TextNode)
                {
                    writer.WriteString(((TextNode) node).Text);
                }
                else if (node is ComponentNode)
                {
                    writer.WriteStartElement("table");

                    var componentNode = (ComponentNode) node;

                    using (new ComponentEvaluationScope(componentNode))
                    {
                        WriteHeaderNode(writer, componentNode.Sections["header"]);
                        WriteItems(writer, componentNode.Sections["item"]);
                    }

                    writer.WriteEndElement();
                }
                else if (node is ItemNode)
                {
                    object item = ComponentEvaluationScope.Current.ViewParameters["item"];
                    var itemNode = (ItemNode) node;
                    writer.WriteString(itemNode.Evaluate(item));
                }
                else
                {
                    writer.WriteElementString("Error", string.Format("Unsupported node type \"{0}\" encountered", node.GetType()));
                }
            }
        }

        private static void WriteHeaderNode(XmlWriter writer, SectionNode section)
        {
            WriteXml(writer, section.Nodes);
        }

        private static void WriteItems(XmlWriter writer, SectionNode section)
        {
            ComponentEvaluationScope scope = ComponentEvaluationScope.Current;

            var items = (IEnumerable) scope.ComponentNode.Parameters["source"];

            foreach (object item in items)
            {
                scope.ViewParameters["item"] = item;
                WriteXml(writer, section.Nodes);
            }
        }
    }
}