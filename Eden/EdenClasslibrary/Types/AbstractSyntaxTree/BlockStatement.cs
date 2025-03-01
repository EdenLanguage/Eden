using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class BlockStatement : Statement
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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Block {");

            foreach(Statement statement in _statements)
            {
                sb.AppendLine($"\t{statement.ToString()}");
            }
            
            sb.AppendLine("}");
            string result = sb.ToString();
            return result;
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Statement statement in Statements)
            {
                sb.AppendLine(statement.Print());
            }
            return sb.ToString();
        }

        public override string ToAST(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indents)}{nameof(BlockStatement)} {{");

            foreach (Statement statement in _statements)
            {
                sb.AppendLine($"{statement.ToAST(indents + 1)}");
            }

            sb.AppendLine("}");
            string result = sb.ToString();
            return result;
        }

        public override string ToPrettyAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{"Block".Pastel(Color.Orange)} {{");

            foreach (Statement statement in _statements)
            {
                sb.AppendLine($"{statement.ToPrettyAST(indent + 1)}");
            }

            sb.AppendLine("}");
            string result = sb.ToString();
            return result;
        }
    }
}
