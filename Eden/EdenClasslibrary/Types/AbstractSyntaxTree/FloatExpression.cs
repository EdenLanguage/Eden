using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Globalization;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class FloatExpression : Expression, IPrintable
    {
        public double Value
        {
            get
            {
                // If this throws idk, theoretically it was parsed in Lexer before so it should work now...
                double parsed = 0;
                bool couldParse = double.TryParse(NodeToken.LiteralValue, CultureInfo.InvariantCulture, out parsed);
                return parsed;
            }
        }
        public FloatExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
        public override string ParenthesesPrint()
        {
            return $"{Value.ToString(CultureInfo.InvariantCulture)}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(FloatExpression)} {{ {Value.ToString(CultureInfo.InvariantCulture)} }}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Value}";
        }
    }
}
