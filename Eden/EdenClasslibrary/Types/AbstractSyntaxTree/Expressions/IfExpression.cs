using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class IfExpression : Expression
    {
        /*  This is how 'IfExpression' looks like.
         *  If ( 'Expression' ) { 'BlockStatement' } Else { 'BlockStatement' }
         *  If ( 'ConditionExpression' ) { 'FulfielldBlock' } Else { 'AlternativeBlock' }
         */
        public Expression ConditionExpression { get; set; }
        public Statement FulfielldBlock { get; set; }
        public Statement AlternativeBlock { get; set; }
        public IfExpression(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"If({ConditionExpression.Print()}) {{");
            sb.Append($"{FulfielldBlock.Print(indents + 1)}");
            sb.AppendLine("}");

            if (AlternativeBlock != null)
            {
                sb.AppendLine("Else {");
                sb.Append($"{AlternativeBlock.Print(indents + 1)}");
                sb.AppendLine("}");
            }

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(IfExpression)} {{");

            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Condition {{");
            sb.AppendLine($"{ConditionExpression.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}FulfielldBlock {{");
            sb.AppendLine($"{FulfielldBlock.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");

            if (AlternativeBlock != null)
            {
                sb.Append(",");
                sb.AppendLine($"{Common.IndentCreator(indent + 1)}FulfielldBlock {{");
                sb.AppendLine($"{AlternativeBlock.ToAbstractSyntaxTree(indent + 2)}");
                sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            }

            sb.AppendLine($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
