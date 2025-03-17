using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeBinaryOpFailed : RuntimeError
    {
        private IObject _left;
        private TokenType _operatorToken;
        private IObject _right;

        private ErrorRuntimeBinaryOpFailed(IObject left, TokenType operatorToken, IObject right)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }

        public static AError Create(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorRuntimeBinaryOpFailed(left, operatorToken, right);
        }
        public static IObject CreateErrorObject(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorObject(Create(left, operatorToken, right));
        }

        public override string GetDetails()
        {
            return $"Expression evaluation failed: {_left.Type} {_operatorToken} {_right.Type}";
        }

        public override string GetMessage()
        {
            return $"There was a fatal error while evaluating expression";
        }
    }
}
