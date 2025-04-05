using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class LiteralIdentifier : Expression
    {
        public string Value
        {
            get
            {
                return NodeToken.LiteralValue;
            }
        }
        public LiteralIdentifier(Token token) : base(token)
        {
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
            return $"{Common.IndentCreator(indent)}{nameof(LiteralIdentifier)} {{ {Value} }}";
        }
    }
}
