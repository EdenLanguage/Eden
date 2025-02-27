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
            sb.AppendLine("{");

            foreach(Statement statement in _statements)
            {
                sb.AppendLine($"\tStatement: {statement.ToString()}");
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
    }
}
