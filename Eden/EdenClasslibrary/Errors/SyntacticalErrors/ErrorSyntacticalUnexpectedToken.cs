using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalUnexpectedToken : SyntacticalError
    {
        private Token _actual;
        private TokenType _expected;

        private ErrorSyntacticalUnexpectedToken(Token actual, TokenType expected)
        {
            _actual = actual;
            _expected = expected;
        }

        public static AError Create(Token actual, TokenType expected)
        {
            return new ErrorSyntacticalUnexpectedToken(actual, expected);
        }

        public override string GetDetails()
        {
            return $"Parser expected '{_expected}' token but acutal token was '{_actual.Keyword}'. File: '{_actual.Filename}' Line: '{_actual.Line}' Column: '{_actual.Start}'";
        }

        public override string GetMessage()
        {
            return $"Unexpected token encountered!";
        }
    }
}
