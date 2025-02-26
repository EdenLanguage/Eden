﻿using Pastel;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IntExpression : Expression
    {
        public int Value
        {
            get
            {
                int parsed = 0;
                bool couldParse = int.TryParse(NodeToken.LiteralValue, out parsed);
                return parsed;
            }
        }
        public IntExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"{Value}";
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tKeyword: {NodeToken.Keyword.ToString().Pastel(ConsoleColor.Yellow)}");
            //sb.AppendLine($"\tValue: {NodeToken.Keyword.ToString().Pastel(ConsoleColor.Blue)}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }
        public override string ParenthesesPrint()
        {
            return $"{Value}";
        }
    }
}
