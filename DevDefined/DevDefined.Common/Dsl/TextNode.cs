namespace DevDefined.Common.Dsl
{
    public class TextNode : AbstractNode
    {
        public TextNode()
        {
        }

        public TextNode(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}