using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Base clas for expressions.
    /// Expression is something like: 
    ///     Var Float pi = -->3.14<--;
    ///     Var Int sum = -->3 + 5 + 2 * 1<--;
    ///     Var String name = -->"Clown"<--;
    /// </summary>
    public abstract class Expression : ASTreeNode
    {
        protected Expression(Token token) : base(token) { }
    }
}
