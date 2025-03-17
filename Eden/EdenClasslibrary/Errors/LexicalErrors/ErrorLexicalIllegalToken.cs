using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.LexicalErrors
{
    public class ErrorLexicalIllegalToken : LexicalError
    {
        private Token _illegalToken;
        private ErrorLexicalIllegalToken(Token token)
        {
            _illegalToken = token;
        }

        public static AError Create(Token token)
        {
            return new ErrorLexicalIllegalToken(token);
        }
        public override string GetDetails()
        {
            return $"Token literal: '{_illegalToken.LiteralValue}'. Line: '{_illegalToken.Line}'. Column: '{_illegalToken.Start}'. File: '{_illegalToken.Filename}'";
        }

        public override string GetMessage()
        {
            return $"Lexer encountered illegal token!";
        }
    }
}
