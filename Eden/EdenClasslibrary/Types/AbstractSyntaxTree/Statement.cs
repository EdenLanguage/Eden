using System.Reflection.Emit;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// AST node for statement.
    /// Statement if a basic representation of line. Example: 'Var Int counter = 10;'.
    /// </summary>
    public abstract class Statement : ASTreeNode
    {
        public Statement(Token token) : base(token)
        {
        }

        public abstract string Print();
    }
}
