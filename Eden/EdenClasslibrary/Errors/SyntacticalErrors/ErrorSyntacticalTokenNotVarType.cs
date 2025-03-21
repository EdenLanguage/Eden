using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalTokenNotVarType : SyntacticalError
    {
        private Token _actual;

        public ErrorSyntacticalTokenNotVarType(Token token, string line) : base(token, line)
        {
            _actual = token;
        }

        public static AError Create(Token actual, string line)
        {
            return new ErrorSyntacticalTokenNotVarType(actual, line);
        }

        public override string GetMessage()
        {
            return $"Token '{_actual.LiteralValue}' is not variable type!";
        }
    }
}
