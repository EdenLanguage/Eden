using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    class ErrorSyntacticalTokenNotVarType : AError
    {
        private Token _actual;

        private ErrorSyntacticalTokenNotVarType(Token actual)
        {
            _actual = actual;
        }

        public static AError Create(Token actual)
        {
            return new ErrorSyntacticalTokenNotVarType(actual);
        }

        public override string GetDetails()
        {
            return $"Expected token should be of Variable type but acutal token is '{_actual.Keyword}' with value '{_actual.LiteralValue}'. File: '{_actual.Filename}' Line: '{_actual.Line}' Column: '{_actual.Start}'";
        }

        public override string GetMessage()
        {
            return $"Token is not Variable type!";
        }
    }
}
