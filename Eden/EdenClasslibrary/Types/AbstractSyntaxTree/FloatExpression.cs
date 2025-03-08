using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Globalization;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class FloatExpression : VariableValueExpression, IPrintable
    {
        public float Value
        {
            get
            {
                // If this throws idk, theoretically it was parsed in Lexer before so it should work now...
                float parsed = 0;
                bool couldParse = float.TryParse(NodeToken.LiteralValue, CultureInfo.InvariantCulture, out parsed);
                return parsed;
            }
        }
        public FloatExpression(Token token) : base(token, typeof(FloatObject)) { }

        public override string ToString()
        {
            return PrettyPrint();
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
