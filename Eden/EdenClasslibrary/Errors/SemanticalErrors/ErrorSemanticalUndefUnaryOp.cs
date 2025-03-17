using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorSemanticalUndefUnaryOp : SemanticalError
    {
        private TokenType _operationToken;
        private IObject _type;

        private ErrorSemanticalUndefUnaryOp(TokenType operationToken, IObject type)
        {
            _operationToken = operationToken;
            _type = type;
        }

        public static AError Create(TokenType operationToken, IObject type)
        {
            return new ErrorSemanticalUndefUnaryOp(operationToken, type);
        }

        public static IObject CreateErrorObject(TokenType operationToken, IObject type)
        {
            return ErrorObject.Create(Create(operationToken, type));
        }

        public override string GetDetails()
        {
            return $"Operation '{_operationToken}{_type.Type}' is not defined!";
        }

        public override string GetMessage()
        {
            return $"Undefined unary operation!";
        }
    }
}
