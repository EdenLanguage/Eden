using EdenClasslibrary.Errors;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class InvalidStatement : Statement
    {
        private AError _error;
        public InvalidStatement(Token token, AError error) : base(token)
        {
            _error = error;
            HasErrors = true;
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
            return _error.PrintError();
        }

        public string ToAbstractSyntaxTree()
        {
            return ToAbstractSyntaxTree();
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(InvalidStatement)} {{{_error.GetMessage() }}};";
        }

        public override string ToString()
        {
            return _error.PrintError();
        }
    }
}