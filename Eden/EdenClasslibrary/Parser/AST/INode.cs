using EdenClasslibrary.Types;
using System.Text;

namespace EdenClasslibrary.Parser.AST
{
    public interface INode
    {
        Token Token { get; set; }
    }

    public class Expression : INode
    {
        public Token Token { get; set; }
    }

    public class Identifier : Expression
    {
        public string Value { get; set; }
    }

    public class IntLiteral : Expression
    {
        public long Value { get; set; }
    }


    public class BinaryExpression : Expression
    {
        //    8      +       8
        // [Left] [Token] [Right]
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }

    public class Statement : INode
    {
        public Token Token { get; set; }

    }

    public class BlockStatement : Statement
    {
        public Statement[] Statements
        {
            get
            {
                return _statements.ToArray();
            }
        }
        private List<Statement> _statements;
        public BlockStatement()
        {
            _statements = new List<Statement>();
        }

        public void AddStatement(Statement statement)
        {
            _statements.Add(statement);
        }
    }

    public class VariableStatement : Statement
    {
        //   var    int   variable  =  10 + 20;
        // [Token] [Type]  [Name]     [Expression]
        public Identifier Name { get; set; }
        public Token Type { get; set; }
        public Expression Value { get; set; }

    }

    public class ExpressionStatement : Statement
    {
        //    10 + 20      ;
        // [Expression]
        // [Expression statement]
        public Expression Expression { get; set; }
        public ExpressionStatement()
        {
        }

    }

    public class ReturnStatement : Statement
    {
        public Token NodeToken { get; set; }
        public Expression ReturnExpression { get; set; }
        public ReturnStatement()
        {
        }
    }
}
