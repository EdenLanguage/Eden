using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class StringExpression : VariableValueExpression
    {
        public string Value { get; set; }
        public StringExpression(Token token) : base(token, typeof(StringObject)) { }
        
        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}\"{Value}\"";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(StringExpression)} {{ \"{Value}\" }}";
        }
    }
}
