using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using System.Globalization;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class FloatExpression : VariableValueExpression
    {
        public float Value { get; set; }
        public FloatExpression(Token token) : base(token, typeof(FloatObject)) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Value}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(FloatExpression)} {{ {Value.ToString(CultureInfo.InvariantCulture)} }}";
        }
    }
}