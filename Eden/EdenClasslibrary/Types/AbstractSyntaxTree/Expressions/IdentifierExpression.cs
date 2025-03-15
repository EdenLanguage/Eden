using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class IdentifierExpression : Expression
    {
        public string Name
        {
            get
            {
                return NodeToken.LiteralValue;
            }
        }
        public IdentifierExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Name}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(IdentifierExpression)} {{ {Name} }}";
        }
    }
}
