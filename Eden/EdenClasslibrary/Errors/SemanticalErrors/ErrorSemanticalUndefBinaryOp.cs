using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalUndefBinaryOp : SemanticalError
    {
        private IObject _left;
        private Token _operatorToken;
        private IObject _right;

        public ErrorSemanticalUndefBinaryOp(IObject left, Token operatorToken, IObject right, string line) : base(left.Token, line)
        {
            _left = left;
            _operatorToken = operatorToken;
            _right = right;
        }

        public static AError Create(IObject left, Token operatorToken, IObject right, string line)
        {
            return new ErrorSemanticalUndefBinaryOp(left, operatorToken, right, line);
        }
        public static IObject CreateErrorObject(IObject left, Token operatorToken, IObject right, string line)
        {
            return new ErrorObject(left.Token, Create(left, operatorToken, right, line));
        }

        public override string GetMessage()
        {
            return $"Operation '{_left.LanguageType}({_left.AsString()}) {_operatorToken.LiteralValue} {_right.LanguageType}({_right.AsString()})' is not defined!";
        }
    }
}
