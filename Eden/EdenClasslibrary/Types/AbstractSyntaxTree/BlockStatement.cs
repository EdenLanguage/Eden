using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class BlockStatement : Statement, IPrintable
    {
        private List<Statement> _statements;
        public Statement[] Statements
        {
            get
            {
                return _statements.ToArray();
            }
        }
        public BlockStatement(Token token) : base(token)
        {
            _statements = new List<Statement>();
        }

        public void AddStatement(Statement statement)
        {
            _statements.Add(statement);
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BlockStatement)} {{");
            foreach(Statement statement in Statements)
            {
                sb.AppendLine($"{(statement as IPrintable).PrettyPrintAST(indent + 1)}");
            }
            sb.AppendLine($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Statement statement in _statements)
            {
                sb.AppendLine($"{(statement as IPrintable).PrettyPrint(indents)}");
            }

            string result = sb.ToString();
            return result;
        }
    }
}
