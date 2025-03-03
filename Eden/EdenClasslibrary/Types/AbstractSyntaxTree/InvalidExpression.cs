using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    class InvalidExpression : Expression, IPrintable
    {
        public InvalidExpression(Token token) : base(token) { }

        public override string ParenthesesPrint()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Invalid expression: '{"expression"}'";
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indent)}{nameof(InvalidExpression)} {{ {NodeToken.LiteralValue} }}");

            string toStr = sb.ToString();
            return toStr;
        }

        public string ToASTFormat()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
