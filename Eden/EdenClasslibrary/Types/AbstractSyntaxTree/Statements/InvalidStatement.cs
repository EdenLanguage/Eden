using EdenClasslibrary.Errors;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class InvalidStatement : Statement
    {
        public AError Error { get; }
        public InvalidStatement(Token token, AError error) : base(token)
        {
            Error = error;
        }

        public static Statement Create(Token token, AError error)
        {
            return new InvalidStatement(token, error);
        }
        
        public static Statement Create(InvalidExpression invalidExpression)
        {
            return new InvalidStatement(invalidExpression.NodeToken, invalidExpression.Error);
        }

        public override string Print(int indents = 0)
        {
            return Error.PrintError();
        }

        public string ToAbstractSyntaxTree()
        {
            return ToAbstractSyntaxTree();
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(InvalidStatement)} {{{Error.GetMessage() }}};";
        }

        public override string ToString()
        {
            return Error.PrintError();
        }
    }
}