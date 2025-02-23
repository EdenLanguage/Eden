using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenClasslibrary.Types
{
    public enum TokenType
    {
        Illegal,
        Eof,
        Indentifier,
        Keyword,
        Number,
        Assign,
        Equal,
        LesserOrEqual,
        GreaterOrEqual,
        LeftArrow,
        RightArrow,
        Inequal,
        ExclemationMark,
        Plus,
        Minus,
        Comma,
        Semicolon,
        LeftParenthesis,
        RightParenthesis,
        LeftBracket,
        RightBracket,
        Function,
        Var,
        VarType,
    }

    public class Token
    {
        public TokenType Keyword { get; set; }
        public string Value { get; set; }

        public Token()
        {

        }

        public bool CanParseNextToken()
        {
            return Keyword != TokenType.Illegal && Keyword != TokenType.Eof;
        }

        public Token(TokenType keyword, string value)
        {
            Keyword = keyword;
            Value = value;
        }

        public void SetAttributes(TokenType keyword, string value)
        {
            Keyword = keyword;
            Value = value;
        }

        public void SetAttributes(TokenType keyword, char value)
        {
            Keyword = keyword;
            Value = value.ToString();
        }

        public bool IsAssignType()
        {
            return Value == "=";
        }

        public bool IsVariableType()
        {
            string[] types = new string[]
            {
                "int",
                "float",
                "bool",
            };

            bool goodKeyword = Keyword == TokenType.VarType;
            bool isType = types.Contains(Value);
            
            return goodKeyword && isType;
        }
    }
}
