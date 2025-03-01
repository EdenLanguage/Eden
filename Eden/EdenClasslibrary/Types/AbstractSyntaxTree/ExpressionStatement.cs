using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ExpressionStatement : Statement, IPrintable
    {
        public Expression Expression { get; set; }
        public ExpressionStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public override string Print()
        {
            return Expression.ParenthesesPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            //return $"{Expression.ToPrettyAST(indent)}";
            return "";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{(Expression as IPrintable).PrettyPrint()};";
        }
    }
}
