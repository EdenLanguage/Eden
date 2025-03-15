using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class InvalidExpression : Expression
    {
        public AError Error { get; }
        private InvalidExpression(Token token, AError error) : base(token)
        {
            Error = error;
        }

        public static Expression Create(Token token, AError error)
        {
            return new InvalidExpression(token, error);
        }

        public override string ToString()
        {
            return Error.PrintError();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Invalid expression";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(InvalidExpression)} {{ {NodeToken.LiteralValue} }}";
        }
    }
}
