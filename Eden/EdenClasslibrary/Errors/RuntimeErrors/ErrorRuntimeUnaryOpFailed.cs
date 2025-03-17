using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeUnaryOpFailed : RuntimeError
    {
        private TokenType _operatorToken;
        private IObject _right;

        private ErrorRuntimeUnaryOpFailed(TokenType operatorToken, IObject right)
        {
            _operatorToken = operatorToken;
            _right = right;
        }

        public static AError Create(TokenType operatorToken, IObject right)
        {
            return new ErrorRuntimeUnaryOpFailed(operatorToken, right);
        }

        public static IObject CreateErrorObject(TokenType operatorToken, IObject right)
        {
            return new ErrorObject(Create(operatorToken, right));
        }

        public override string GetDetails()
        {
            return $"There was a fatal error while evaluating expression: '{_operatorToken}{_right.Type}'";
        }

        public override string GetMessage()
        {
            return $"Expression evaluation failed!";
        }
    }
}
