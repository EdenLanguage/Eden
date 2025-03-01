﻿using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
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

        public override string ParenthesesPrint()
        {
            return "NotImplementedException";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return "NotImplementedException";
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
