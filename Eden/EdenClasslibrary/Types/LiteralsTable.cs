using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenClasslibrary.Types
{
    public class LiteralsTable
    {
        private Dictionary<string, Expression> _literalExps;

        public LiteralsTable()
        {
            _literalExps = new Dictionary<string, Expression>();
        }

        public Token Check(Expression exp)
        {
            Token contains = null;
            if (exp is BinaryExpression asBinary)
            {
                contains = Check(asBinary.Left);
                if(contains == null)
                {
                    contains = Check(asBinary.Right);
                }
            }
            else if (exp is UnaryExpression asUnary)
            {
                contains = Check(asUnary.Expression);
            }
            else if (exp is IntExpression || exp is CharExpression || exp is FloatExpression || exp is StringExpression || exp is BoolExpresion)
            {
                //  It is ok!
            }
            else
            {
                contains = exp.NodeToken;
            }
            return contains;
        }

        public void AddLiteral(LiteralStatement literalStatement)
        {
            string name = literalStatement.Name.NodeToken.LiteralValue;

            if (LiteralExists(name))
            {
                throw new Exception($"Literal '{name}' is already defined!");
            }

            _literalExps.Add(name, literalStatement.Expression);
        }

        public bool LiteralExists(string name)
        {
            return _literalExps.ContainsKey(name);
        }

        public Expression GetLiteral(string name)
        {
            return _literalExps[name];
        }
    }
}
