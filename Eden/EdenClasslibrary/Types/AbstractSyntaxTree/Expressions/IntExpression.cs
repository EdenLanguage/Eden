using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class IntExpression : VariableValueExpression
    {
        public IntExpression(Token token) : base(token, typeof(IntObject)) { }

        public int Value { get; set; }

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
            return $"{Common.IndentCreator(indent)}{nameof(IntExpression)} {{ {Value} }}";
        }
    }
}
