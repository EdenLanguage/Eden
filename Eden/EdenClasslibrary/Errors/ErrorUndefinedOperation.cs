using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors
{
    public class ErrorUndefinedOperation : AError
    {
        private Token _left;
        private Token _operatorToken;
        private Token _right;
        
        public ErrorUndefinedOperation(Token left, Token operatorToken, Token right)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }
        
        public static AError Create(Token left, Token operatorToken, Token right)
        {
            return new ErrorUndefinedOperation(left, operatorToken, right);
        }

        public override string GetDetails()
        {
            return $"> File: '{Path.GetFileName(_left.Filename)}'. Line: '{_operatorToken.Line}'. Column:'{_operatorToken.TokenStartingLinePosition}'.";
        }

        public override string GetMessage()
        {
            return $"> Undefined operation! {_left.Keyword} {_operatorToken.Keyword} {_right.Keyword} is not defined!";
        }
    }
}
