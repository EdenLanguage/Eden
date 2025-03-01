using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class FloatExpression : Expression
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
            return Value.ToString(CultureInfo.InvariantCulture);
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tKeyword: {NodeToken.Keyword.ToString().Pastel(ConsoleColor.Yellow)}");
            //sb.AppendLine($"\tValue: {NodeToken.Keyword.ToString().Pastel(ConsoleColor.Blue)}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }
        public override string ParenthesesPrint()
        {
            return $"{Value.ToString(CultureInfo.InvariantCulture)}";
        }

        public override string ToAST(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{nameof(FloatExpression)}: {Value}";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(FloatExpression).Pastel(Color.Orange)}: {Value}";
        }
    }
}
