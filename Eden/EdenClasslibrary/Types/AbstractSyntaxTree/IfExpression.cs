using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IfExpression : Expression, IPrintable
    {
        /*  This is how 'IfExpression' looks like.
         *  If ( 'Expression' ) { 'BlockStatement' } Else { 'BlockStatement' }
         *  If ( 'ConditionExpression' ) { 'FulfielldBlock' } Else { 'AlternativeBlock' }
         */
        public Expression ConditionExpression { get; set; }
        public BlockStatement FulfielldBlock { get; set; }
        public BlockStatement AlternativeBlock { get; set; }
        public IfExpression(Token token) : base(token) { }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(IfExpression)} {{");

            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Condition {{");
            sb.AppendLine($"{(ConditionExpression as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}FulfielldBlock {{");
            sb.AppendLine($"{(FulfielldBlock as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");

            if(AlternativeBlock != null)
            {
                sb.Append(",");
                sb.AppendLine($"{Common.IndentCreator(indent + 1)}FulfielldBlock {{");
                sb.AppendLine($"{(AlternativeBlock as IPrintable).PrettyPrintAST(indent + 2)}");
                sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            }

            sb.AppendLine($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"If({(ConditionExpression as IPrintable).PrettyPrint()}) {{");
            sb.Append($"{FulfielldBlock.PrettyPrint(indents + 1)}");
            sb.AppendLine("}");

            if (AlternativeBlock != null)
            {
                sb.AppendLine("Else {");
                sb.Append($"{AlternativeBlock.PrettyPrint(indents + 1)}");
                sb.AppendLine("}");
            }

            string result = sb.ToString();
            return result;
        }
    }
}
