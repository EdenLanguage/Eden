using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalUndefBinaryOp : SemanticalError
    {
        private IObject _left;
        private TokenType _operatorToken;
        private IObject _right;

        private ErrorSemanticalUndefBinaryOp(IObject left, TokenType operatorToken, IObject right)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }
        
        public static AError Create(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorSemanticalUndefBinaryOp(left, operatorToken, right);
        }
        public static IObject CreateErrorObject(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorObject(Create(left, operatorToken, right));
        }

        public override string GetDetails()
        {
            return $"Binary expression '{_left.AsString()} {_operatorToken} {_right.AsString()}' is not defined!";
        }

        public override string GetMessage()
        {
            return $"Undefined binary operation!";
        }
    }
}
