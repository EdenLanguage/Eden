namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    /// <summary>
    /// AST node for statement.
    /// Statement if a basic representation of line. Example: 'Var Int counter = 10;'.
    /// </summary>
    public abstract class Statement : AbstractSyntaxTreeNode
    {
        public Statement(Token token) : base(token)
        {
        }
    }
}
