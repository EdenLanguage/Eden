using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorUndefinedOperation : AError
    {
        private IObject _left;
        private TokenType _operatorToken;
        private IObject _right;

        private ErrorUndefinedOperation(IObject left, TokenType operatorToken, IObject right)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }
        
        public static AError Create(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorUndefinedOperation(left, operatorToken, right);
        }
        public static IObject CreateErrorObject(IObject left, TokenType operatorToken, IObject right)
        {
            return new ErrorObject(Create(left, operatorToken, right));
        }

        public override string GetDetails()
        {
            return $">     Expression failed! '{_left.AsString()} {_operatorToken} {_right.AsString()}'";
        }

        public override string GetMessage()
        {
            return $"> Undefined operation! '{_left.Type} {_operatorToken} {_right.Type}' is not defined!";
        }
    }
}
