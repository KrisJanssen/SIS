using System.Collections.Generic;

namespace DevDefined.Common.Dsl
{
    public class ComponentNode : AbstractNode
    {
        private readonly Dictionary<string, SectionNode> _sections = new Dictionary<string, SectionNode>();
        private Dictionary<string, object> _parameters = new Dictionary<string, object>();

        public string Name { get; set; }

        public Dictionary<string, SectionNode> Sections
        {
            get { return _sections; }
        }

        public Dictionary<string, object> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}