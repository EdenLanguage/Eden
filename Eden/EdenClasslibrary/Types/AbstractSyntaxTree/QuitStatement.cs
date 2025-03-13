using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class QuitStatement : Statement, IPrintable
    {
        public QuitStatement(Token token) : base(token)
        {
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Quit;";
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(QuitStatement)}";
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
