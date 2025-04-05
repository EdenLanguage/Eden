using System.Text;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class ListArgumentsExpression : Expression
    {
        public VariableTypeExpression Type { get; set; }
        public List<Expression> Arguments { get; set; }

        /// <summary>
        /// Accepts either a Literal value or a Literal statement during the parsing stage and evaluates it afterward.
        /// </summary>
        public Expression SizeExpression { get; set; }
        public int Capacity
        {
            get
            {
                return Arguments.Capacity;
            }
        }
        public ListArgumentsExpression(Token token) : base(token)
        {
            Arguments = new List<Expression>();
        }

        public void AddArgument(Expression expression)
        {
            Arguments.Add(expression);
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            for (int i = 0; i < Arguments.Count; i++)
            {
                sb.Append($"{Arguments[i].Print()}");
                if (i < Arguments.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ListArgumentsExpression)} {{");
            for (int i = 0; i < Arguments.Count; i++)
            {
                sb.AppendLine($"{Arguments[i].ToAbstractSyntaxTree(indent + 1)},");
            }
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string result = sb.ToString();
            return result;
        }
    }
}
