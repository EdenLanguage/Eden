using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorUnaryOperationFailed : AError
    {
        private TokenType _operatorToken;
        private IObject _right;

        private ErrorUnaryOperationFailed(TokenType operatorToken, IObject right)
        {
            _operatorToken = operatorToken;
            _right = right;
        }

        public static AError Create(TokenType operatorToken, IObject right)
        {
            return new ErrorUnaryOperationFailed(operatorToken, right);
        }

        public static IObject CreateErrorObject(TokenType operatorToken, IObject right)
        {
            return new ErrorObject(Create(operatorToken, right));
        }

        public override string GetDetails()
        {
            return $"> Expression evaluation failed! {_operatorToken} {_right.ToString()}";
        }

        public override string GetMessage()
        {
            return $">     There was a fatal error while evaluation expression with types: {_operatorToken} {_right.Type}";
        }
    }
}
