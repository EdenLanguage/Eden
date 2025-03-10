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
            "Var",
            "Function",
            "Return",
            "If",
            "List",
            "Null",
            "Else",
            "Structure",
            "True",
            "False"
        };

        public static TokenType ToTokenType(string keyword)
        {
            TokenType type = TokenType.Keyword;
            switch (keyword)
            {
                case "If": type = TokenType.If; break;
                case "Else": type = TokenType.Else; break;
                case "Function": type = TokenType.Function; break;
                case "Var": 
                case "Return": 
                case "Structure": 
                case "List": 
                    type = TokenType.Keyword; break;
                case "Null":
                    type = TokenType.Null; break;
                case "Int":
                case "Float":
                case "Bool":
                case "String":
                    type = TokenType.VariableType; break;
                case "True":
                case "False":
                    type = TokenType.Bool; break;
                case "^":
                    type = TokenType.Power; break;
                default: type = TokenType.Illegal; break;
            }
            return type;
        }

        public static bool IsKeyword(string keyword)
        {
            return LanguageKeyword.Contains(keyword) || Variables.IsVariableType(keyword);
        }
    }
}
