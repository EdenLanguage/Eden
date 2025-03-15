using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
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
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Statement statement in _statements)
            {
                sb.AppendLine($"{statement.Print(indents)}");
            }

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BlockStatement)} {{");
            foreach (Statement statement in Statements)
            {
                sb.AppendLine($"{statement.ToAbstractSyntaxTree(indent + 1)}");
            }
            sb.AppendLine($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
