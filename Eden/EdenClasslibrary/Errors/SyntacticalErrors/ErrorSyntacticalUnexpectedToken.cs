using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalUnexpectedToken : SyntacticalError
    {
        private Token _actual;
        private TokenType _expected;

        public ErrorSyntacticalUnexpectedToken(TokenType expected, Token token, string line) : base(token, line)
        {
            _actual = token;
            _expected = expected;
        }

        public static AError Create(TokenType expected, Token token, string line)
        {
            return new ErrorSyntacticalUnexpectedToken(expected, token, line);
        }

        public override string GetMessage()
        {
            return $"Parser expected '{_expected}' token but actual token was '{_actual.Keyword}'.";
        }
    }
}
