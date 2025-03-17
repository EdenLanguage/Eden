using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class BoolExpresion : VariableValueExpression
    {
        public BoolExpresion(Token token) : base(token, typeof(BoolObject)) { }

        public bool Value
        {
            get
            {
                bool result = false;
                bool couldParse = bool.TryParse(NodeToken.LiteralValue, out result);
                if (couldParse == false) throw new Exception($"Parser could not parse '{NodeToken.LiteralValue}' to Bool value!");
                return result;
            }
        }

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
            return $"{Common.IndentCreator(indent)}{nameof(BoolExpresion)} {{ {Value} }}";
        }
    }
}
