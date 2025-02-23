using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenClasslibrary.Types
{
    public static class Keyword
    {
        static string[] LanguageKeyword = new string[]
        {
            "var",
            "function",
            "int",
            "return",
            "if",
            "else",
            "bool",
            "true",
            "false"
        };

        public static TokenType ToTokenType(string keyword)
        {
            TokenType type = TokenType.Keyword;
            switch (keyword)
            {
                case "var": type = TokenType.Var; break;
                case "int":
                case "bool":
                    type = TokenType.VarType; break;
                default: type = TokenType.Keyword; break;
            }
            return type;
        }

        public static bool IsKeyword(string keyword)
        {
            return LanguageKeyword.Contains(keyword);
        }
    }
}
