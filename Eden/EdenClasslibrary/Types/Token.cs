using System.Text.Json;
using System.Text.Json.Serialization;

namespace EdenClasslibrary.Types
{
    public enum TokenType
    {
        //  Basics
        Illegal,
        Eof,                // \0
        Identifier,        // Variable name, function name
        Keyword,            // Language specific name like: 'for', 'foreach', 'Function', 'Var' ...
        VariableType,       // 'Int', 'Float' ...

        //  Operators
        Assign,             // =
        Equal,              // ==
        Inequal,            // !=
        LesserOrEqual,      // <=
        GreaterOrEqual,     // >=
        LeftArrow,          // <
        RightArrow,         // >
        ExclemationMark,    // ?
        Plus,               // +
        Minus,              // -
        Star,               // *
        Slash,              // /
        Power,              // ^
        Comma,              // ,
        Dot,                // .

        //  Symbols
        Semicolon,          // ;
        LeftParenthesis,    // {
        RightParenthesis,   // }
        LeftBracket,        // [
        RightBracket,       // ]

        //  Variable values for variable types: 'Float' -> '0.123123' 
        Bool,
        Int,
        Float,
        String,
    }

    public class Token
    {
        #region Properties
        public TokenType Keyword { get; set; }
        public string LiteralValue { get; set; }
        public int TokenStartingLinePosition { get; set; }
        public int TokenEndingLinePosition { get; set; }
        public int Line { get; set; }
        public string Filename { get; set; }
        #endregion

        public Token()
        {
            LiteralValue = string.Empty;
            Filename = string.Empty;
        }

        public bool Equals(Token other)
        {
            if (Keyword != other.Keyword || LiteralValue != other.LiteralValue || TokenStartingLinePosition != other.TokenStartingLinePosition || TokenEndingLinePosition != other.TokenEndingLinePosition || Line != other.Line || Filename != other.Filename)
            {
                return false;
            }
            else return true;
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            return JsonSerializer.Serialize(this, options);
        }

        public bool IsValid()
        {
            return Keyword != TokenType.Illegal;
        }

        public bool IsNotEof()
        {
            return Keyword != TokenType.Eof;
        }

        public bool IsValidAndNotEof()
        {
            return IsValid() && IsNotEof();
        }

        public Token(TokenType keyword, string value)
        {
            Keyword = keyword;
            LiteralValue = value;
        }

        public Token(TokenType keyword, string value, int line, int startPos)
        {
            Keyword = keyword;
            LiteralValue = value;
            TokenStartingLinePosition = startPos;
            TokenEndingLinePosition = startPos + value.Length - 1;
            Line = line;
            Filename = string.Empty;
        }

        public void SetAttributes(TokenType keyword, string value, int line, int startPos, string filename)
        {
            Keyword = keyword;
            LiteralValue = value;
            Line = line;
            TokenStartingLinePosition = startPos;
            TokenEndingLinePosition = startPos + value.Length - 1;
            Filename = filename;
        }

        public void SetAttributes(TokenType keyword, char value, int line, int startPos, string filename)
        {
            Keyword = keyword;
            LiteralValue = value.ToString();
            Line = line;
            TokenStartingLinePosition = startPos;
            TokenEndingLinePosition = startPos;    // Same as starting because 'char' type has 1 length. So Start is equal to begining.
            Filename = filename;
        }

        public bool IsAssignType()
        {
            return Keyword == TokenType.Assign;
        }

        public bool IsVariableType()
        {
            bool goodKeyword = Keyword == TokenType.VariableType;
            bool isType = Variables.IsVariableType(LiteralValue);
            return goodKeyword && isType;
        }
    }
}
