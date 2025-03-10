using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ListArgumentsExpression : Expression, IPrintable
    {
        public VariableTypeExpression Type { get; set; }
        public List<Expression> Arguments { get; set; }
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

        public void SetCapacity(int size)
        {
            Arguments = new List<Expression>(size);
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

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ListArgumentsExpression)} {{");
            for(int i = 0; i < Arguments.Count; i++)
            {
                sb.AppendLine($"{(Arguments[i] as IPrintable).PrettyPrintAST(indent + 1)},");
            }
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            for (int i = 0; i < Arguments.Count; i++)
            {
                sb.Append($"{(Arguments[i] as IPrintable).PrettyPrint()}");
                if (i < Arguments.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]");

            string result = sb.ToString();
            return result;
        }
    }
}
