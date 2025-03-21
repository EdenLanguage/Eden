using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalUnexpectedExpression : SyntacticalError
    {
        private Token _actual;

        public ErrorSyntacticalUnexpectedExpression(Token token, string line) : base(token, line)
        {
            _actual = token;
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorSyntacticalUnexpectedExpression(token, line);
        }

        public override string GetMessage()
        {
            return $"Token '{_actual.Keyword}' was unexpected.";
        }
    }
}
