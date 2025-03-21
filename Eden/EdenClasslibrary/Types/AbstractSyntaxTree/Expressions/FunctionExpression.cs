using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class FunctionExpression : Expression
    {
        private List<Expression> _functionArguments;
        public Expression Name { get; set; }
        public Expression Type { get; set; }
        public Expression[] Arguments
        {
            get
            {
                return _functionArguments.ToArray();
            }
        }
        public Statement Body { get; set; }
        public FunctionExpression(Token token) : base(token)
        {
            _functionArguments = new List<Expression>();
        }

        public void AddFuncArgument(Expression argument)
        {
            _functionArguments.Add(argument);
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indents)}Function");
            sb.Append($" {Type.Print()}");
            sb.Append($" {Name.Print()}");
            sb.Append("(");

            //  Function args.
            for (int i = 0; i < Arguments.Length; i++)
            {
                sb.Append($"{Arguments[i].Print()}");
                if (i < Arguments.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(")");
            sb.AppendLine(" {");
            sb.Append($"{Body.Print(indents + 1)}");
            sb.Append("}");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(FunctionExpression)} {{");
            sb.AppendLine($"{Type.ToAbstractSyntaxTree(indent + 1)},");
            sb.Append($"{Name.ToAbstractSyntaxTree(indent + 1)}");

            if (Arguments.Length > 0)
            {
                sb.Append(",");
                for (int i = 0; i < Arguments.Length; i++)
                {
                    sb.AppendLine();
                    sb.Append($"{Arguments[i].ToAbstractSyntaxTree(indent + 1)}");
                    if (i < Arguments.Length - 1)
                    {
                        sb.Append($",");
                    }
                }
            }

            sb.Append($",");
            sb.AppendLine();
            sb.Append($"{Body.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string result = sb.ToString();
            return result;
        }
    }
}