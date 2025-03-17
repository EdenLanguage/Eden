namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    /// <summary>
    /// Base clas for expressions.
    /// Expression is something like: 
    ///     Var Float pi = -->3.14<--;
    ///     Var Int sum = -->3 + 5 + 2 * 1<--;
    ///     Var String name = -->"Clown"<--;
    /// </summary>
    public abstract class Expression : AbstractSyntaxTreeNode
    {
        protected Expression(Token token) : base(token) { }
    }
}
