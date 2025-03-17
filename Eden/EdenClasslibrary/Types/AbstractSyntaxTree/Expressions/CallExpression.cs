using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class CallExpression : Expression
    {
        /*  Add(5, 10)
         *  Expression ( Expression[] )
         */
        private List<Expression> _arguments;
        public Expression Function { get; set; }
        public Expression[] Arguments
        {
            get
            {
                return _arguments.ToArray();
            }
        }
        public CallExpression(Token token) : base(token)
        {
            _arguments = new List<Expression>();
        }

        public void AddArgumentExpression(Expression argument)
        {
            _arguments.Add(argument);
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Function.Print()}");
            sb.Append("(");
            for (int i = 0; i < Arguments.Length; i++)
            {
                sb.Append($"{Arguments[i].Print()}");
                if (i < Arguments.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");
            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(CallExpression)} {{");
            sb.AppendLine($"{Function.ToAbstractSyntaxTree(indent + 1)},");

            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Arguments {{");
            foreach (Expression argument in Arguments)
            {
                sb.AppendLine($"{argument.ToAbstractSyntaxTree(indent + 2)},");
            }
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}};");

            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}