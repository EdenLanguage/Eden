namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Basic node of AbstractSyntaxTree
    /// </summary>
    public abstract class ASTreeNode
    {
        public Token NodeToken { get; set; }
        public ASTreeNode(Token token)
        {
            NodeToken = token;
        }
        public abstract override string ToString();
        public abstract string ToAST(int indent = 0);
        public abstract string ToPrettyAST(int indent = 0);
    }
}
