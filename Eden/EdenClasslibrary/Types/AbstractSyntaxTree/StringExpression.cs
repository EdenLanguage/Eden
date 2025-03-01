using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class StringExpression : Expression
    {
        public string Value
        {
            get
            {
                // This this one requires refactoring. I don't realy know whether responsibility of
                // literal value of string should be delegated to lexer or done here...
                return NodeToken.LiteralValue.Replace("\"","");
            }
        }
        public StringExpression(Token token) : base(token) { }

        public override string ParenthesesPrint()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override string ToAST(int indents = 0)
        {
            return $"{nameof(StringExpression)}: \"{Value}\"";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{"String".Pastel(Color.DeepSkyBlue)}: \"{Value}\"";
        }
    }
}
