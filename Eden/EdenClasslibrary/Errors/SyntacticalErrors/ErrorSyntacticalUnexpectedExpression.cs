using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalUnexpectedExpression : SyntacticalError
    {
        private Token _actual;

        private ErrorSyntacticalUnexpectedExpression(Token actual)
        {
            _actual = actual;
        }

        public static AError Create(Token actual)
        {
            return new ErrorSyntacticalUnexpectedExpression(actual);
        }

        public override string GetDetails()
        {
            return $"Token '{_actual.Keyword}' was unexpected. File: '{_actual.Filename}' Line: '{_actual.Line}' Column: '{_actual.Start}'";
        }

        public override string GetMessage()
        {
            return $"Unexpected token encountered!";
        }
    }
}
