using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class InvalidStatement : Statement, IPrintable
    {
        private AError _error;
        public InvalidStatement(Token token, AError error) : base(token)
        {
            _error = error;
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Invalid statement: '{ _error.GetMessage() }'";
        }
        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(InvalidStatement)} {{{_error.GetMessage() }}};";
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
