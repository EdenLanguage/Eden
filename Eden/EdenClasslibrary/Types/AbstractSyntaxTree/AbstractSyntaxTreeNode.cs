namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Basic node of AbstractSyntaxTree
    /// </summary>
    public abstract class AbstractSyntaxTreeNode
    {
        public Token NodeToken { get; set; }
        public AbstractSyntaxTreeNode(Token token)
        {
            NodeToken = token;
        }
        public abstract override string ToString();
        public abstract string Print(int indents = 0);
        public abstract string ToAbstractSyntaxTree(int indent = 0);
    }
}
