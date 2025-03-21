using System.Text;
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
        Function,
        List,
        Var,
        And,
        Or,
        Loop,
        Sisyphus,
        Skip,
        Quit,
        If,
        Else,
        Return,
        VariableType,       // 'Int', 'Float' ...

        //  Operators
        Assign,             // =
        Equal,              // ==
        Inequal,            // !=
        LesserOrEqual,      // <=
        GreaterOrEqual,     // >=
        LeftArrow,          // <
        RightArrow,         // >
        ExclemationMark,    // !
        Plus,               // +
        Minus,              // -
        Star,               // *
        Slash,              // /
        Power,              // ^
        Comma,              // ,
        Dot,                // .
        QuenstionMark,      // ?
        Tilde,              // ~
        Comment,            // //

        //  Symbols
        Semicolon,          // ;
        LeftParenthesis,    // (
        RightParenthesis,   // )
        LeftBracket,        // {
        RightBracket,       // }
        LeftSquareBracket,  // [
        RightSquareBracket, // ]

        //  Variable values for variable types: 'Float' -> '0.123123' 
        Bool,
        Int,
        Float,
        String,
        Char,
        Null,
    }

    public class Token
    {
        #region Properties
        public TokenType Keyword { get; set; }
        public string LiteralValue { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Line { get; set; }
        public string Filename { get; set; }
        #endregion

        #region Constructor
        public Token()
        {
            LiteralValue = string.Empty;
            Filename = string.Empty;
        }

        public Token(TokenType keyword, string value)
        {
            Keyword = keyword;
            LiteralValue = value;
            Start = 1;
            End = 1;
            Line = 1;
            Filename = string.Empty;
        }

        public Token(TokenType keyword, string value, int line, int startPos, string filename = "")
        {
            Keyword = keyword;
            LiteralValue = value;
            Start = startPos;
            End = startPos + value.Length - 1;
            Line = line;
            Filename = filename;
        }
        #endregion

        #region Public
        #endregion
        public bool Equals(Token other)
        {
            bool keyword = Keyword == other.Keyword;
            bool literal = LiteralValue == other.LiteralValue;
            bool start = Start == other.Start;
            bool end = End == other.End;
            bool line = Line == other.Line;
            bool filename = Filename == other.Filename;

            if(keyword && literal && start && end && line && filename)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Keyword} : {LiteralValue}";
        }

        public string ToJSON()
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

        public bool IsSemicolon()
        {
            return Keyword == TokenType.Semicolon;
        }

        public bool IsNotEof()
        {
            return Keyword != TokenType.Eof;
        }

        public string PrintTokenDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine($"/tKeyword: {Keyword}");
            sb.AppendLine($"/tLine: {Line}");
            sb.AppendLine($"/tStart position: {Start}");
            sb.AppendLine($"/tEnd position: {End}");
            sb.AppendLine($"/tFilename: {Filename}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public bool IsValidAndNotEof()
        {
            return IsValid() && IsNotEof();
        }


        public void SetAttributes(TokenType keyword, string value, int line, int startPos, string filename)
        {
            Keyword = keyword;
            LiteralValue = value;
            Line = line;
            Start = startPos;
            End = startPos + value.Length - 1;
            Filename = filename;
        }

        public void SetAttributes(TokenType keyword, char value, int line, int startPos, string filename)
        {
            Keyword = keyword;
            LiteralValue = value.ToString();
            Line = line;
            Start = startPos;
            End = startPos;    // Same as starting because 'char' type has 1 length. So Start is equal to begining.
            Filename = filename;
        }

        public bool IsType(TokenType token)
        {
            if(Keyword == token)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAssignType()
        {
            return Keyword == TokenType.Assign;
        }

        /// <summary>
        /// Indicates whether we can evaluate expression with this token as the first.
        /// </summary>
        /// <returns></returns>
        public bool CanEvaluateExpression()
        {
            return Keyword != TokenType.VariableType && Keyword != TokenType.Keyword;
        }

        public bool IsVariableType()
        {
            bool goodKeyword = Keyword == TokenType.VariableType;
            bool isType = Variables.IsVariableType(LiteralValue);
            return goodKeyword && isType;
        }

        public static Token ProgramRootToken
        {
            get
            {
                return new Token(TokenType.Identifier, "Root");
            }
        }
    }
}
