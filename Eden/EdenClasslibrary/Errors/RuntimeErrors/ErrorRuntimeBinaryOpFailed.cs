using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeBinaryOpFailed : RuntimeError
    {
        private IObject _left;
        private TokenType _operatorToken;
        private IObject _right;

        public ErrorRuntimeBinaryOpFailed(IObject left, TokenType operatorToken, IObject right, string line) : base(left.Token, line)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }

        public static AError Create(IObject left, TokenType operatorToken, IObject right, string line)
        {
            return new ErrorRuntimeBinaryOpFailed(left, operatorToken, right, line);
        }
        public static IObject CreateErrorObject(IObject left, TokenType operatorToken, IObject right, string line)
        {
            return new ErrorObject(left.Token, Create(left, operatorToken, right, line));
        }


        public override string GetMessage()
        {
            return $"Fatal error during '{_left.Type}' '{_operatorToken}' '{_right.Type}' operation!";
        }
    }
}
