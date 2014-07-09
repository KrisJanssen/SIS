// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentNode.cs" company="">
//   
// </copyright>
// <summary>
//   The component node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System.Collections.Generic;

    /// <summary>
    /// The component node.
    /// </summary>
    public class ComponentNode : AbstractNode
    {
        #region Fields

        /// <summary>
        /// The _sections.
        /// </summary>
        private readonly Dictionary<string, SectionNode> _sections = new Dictionary<string, SectionNode>();

        /// <summary>
        /// The _parameters.
        /// </summary>
        private Dictionary<string, object> _parameters = new Dictionary<string, object>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            get
            {
                return this._parameters;
            }

            set
            {
                this._parameters = value;
            }
        }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        public Dictionary<string, SectionNode> Sections
        {
            get
            {
                return this._sections;
            }
        }

        #endregion
    }
}