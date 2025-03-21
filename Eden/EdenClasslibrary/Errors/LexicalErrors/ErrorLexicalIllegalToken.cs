using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.LexicalErrors
{
    public class ErrorLexicalIllegalToken : LexicalError
    {
        public ErrorLexicalIllegalToken(Token token, string line) : base(token, line) { }

        public static AError Create(Token token, string line)
        {
            return new ErrorLexicalIllegalToken(token, line);
        }
        public override string GetMessage()
        {
            return $"Token '{Token.LiteralValue}' is illegal!";
        }
    }
}
