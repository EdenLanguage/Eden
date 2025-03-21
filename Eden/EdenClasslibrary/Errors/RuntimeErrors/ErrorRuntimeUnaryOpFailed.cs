using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeUnaryOpFailed : RuntimeError
    {
        private TokenType _operatorToken;
        private IObject _right;

        public ErrorRuntimeUnaryOpFailed(TokenType op, IObject right, string line) : base(right.Token, line)
        {
            _operatorToken = op;
            _right = right;
        }

        public static AError Create(TokenType op, IObject right, string line)
        {
            return new ErrorRuntimeUnaryOpFailed(op, right, line);
        }

        public static IObject CreateErrorObject(TokenType op, IObject right, string line)
        {
            return new ErrorObject(right.Token, Create(op, right, line));
        }

        public override string GetMessage()
        {
            return $"Fatal error during '{_operatorToken}{_right.Type}' operation!";
        }
    }
}
