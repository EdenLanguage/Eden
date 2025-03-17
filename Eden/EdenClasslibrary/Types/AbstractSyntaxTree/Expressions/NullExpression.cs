using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class NullExpression : VariableValueExpression
    {
        public object Value
        {
            get
            {
                return null;
            }
        }
        public NullExpression(Token token) : base(token, typeof(NullObject)) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}null";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(NullExpression)} {{ null }}";
        }
    }
}
