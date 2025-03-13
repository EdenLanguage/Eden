using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class SkipStatement : Statement, IPrintable
    {
        public SkipStatement(Token token) : base(token) { }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Skip;";
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(SkipStatement)}";
        }

        public string ToASTFormat()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
