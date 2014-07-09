// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DslToXml.cs" company="">
//   
// </copyright>
// <summary>
//   The dsl to xml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// The dsl to xml.
    /// </summary>
    public static class DslToXml
    {
        #region Public Methods and Operators

        /// <summary>
        /// The to xml.
        /// </summary>
        /// <param name="dsl">
        /// The dsl.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToXml(StandardDsl dsl)
        {
            var nodeWriter = new NodeWriter();

            using (var scope = new DslEvaluationScope(nodeWriter))
            {
                dsl.Execute();
            }

            return ToXml(nodeWriter);
        }

        /// <summary>
        /// The to xml.
        /// </summary>
        /// <param name="nodeWriter">
        /// The node writer.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToXml(NodeWriter nodeWriter)
        {
            var builder = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(builder))
            {
                WriteXml(writer, nodeWriter.Nodes);
            }

            return builder.ToString();
        }

        /// <summary>
        /// The write xml.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        public static void WriteXml(XmlWriter writer, IEnumerable<INode> nodes)
        {
            foreach (INode node in nodes)
            {
                if (node is NamedNode)
                {
                    writer.WriteStartElement(((NamedNode)node).Name);
                    WriteXml(writer, node.Nodes);
                    writer.WriteEndElement();
                }
                else if (node is TextNode)
                {
                    writer.WriteString(((TextNode)node).Text);
                }
                else if (node is ComponentNode)
                {
                    writer.WriteStartElement("table");

                    var componentNode = (ComponentNode)node;

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
                    var itemNode = (ItemNode)node;
                    writer.WriteString(itemNode.Evaluate(item));
                }
                else
                {
                    writer.WriteElementString(
                        "Error", 
                        string.Format("Unsupported node type \"{0}\" encountered", node.GetType()));
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The write header node.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="section">
        /// The section.
        /// </param>
        private static void WriteHeaderNode(XmlWriter writer, SectionNode section)
        {
            WriteXml(writer, section.Nodes);
        }

        /// <summary>
        /// The write items.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="section">
        /// The section.
        /// </param>
        private static void WriteItems(XmlWriter writer, SectionNode section)
        {
            ComponentEvaluationScope scope = ComponentEvaluationScope.Current;

            var items = (IEnumerable)scope.ComponentNode.Parameters["source"];

            foreach (object item in items)
            {
                scope.ViewParameters["item"] = item;
                WriteXml(writer, section.Nodes);
            }
        }

        #endregion
    }
}