using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class InvalidStatement : Statement, IPrintable
    {
        public InvalidStatement(Token token) : base(token) { }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Invalid statement: '{"statement"}'";
        }

        public override string Print()
        {
            return "Parser encountered invalid statement!";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(InvalidStatement)} {{ Invalid statement }};";
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
