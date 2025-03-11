using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors
{
    public class ErrorIllegalToken : AError
    {
        private Token _illegalToken;
        private ErrorIllegalToken(Token token)
        {
            _illegalToken = token;
        }

        public static AError Create(Token token)
        {
            return new ErrorIllegalToken(token);
        }
        public override string GetDetails()
        {
            return $"Illegal input: '{_illegalToken.LiteralValue}', Line: '{_illegalToken.Line}', Column: '{_illegalToken.TokenStartingLinePosition}'";
        }

        public override string GetMessage()
        {
            return $"Parser encountered illegal token!";
        }
    }
}
