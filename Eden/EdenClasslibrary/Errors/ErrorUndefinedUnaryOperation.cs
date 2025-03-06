using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorUndefinedUnaryOperation : AError
    {
        private TokenType _operationToken;
        private IObject _type;

        private ErrorUndefinedUnaryOperation(TokenType operationToken, IObject type)
        {
            _operationToken = operationToken;
            _type = type;
        }

        public static AError Create(TokenType operationToken, IObject type)
        {
            return new ErrorUndefinedUnaryOperation(operationToken, type);
        }

        public override string GetDetails()
        {
            return $"> Expression failed! {_operationToken} {_type.ToString()}";
        }

        public override string GetMessage()
        {
            return $">     Undefined operation! {_operationToken} {_type.Type} is not defined!";
        }
    }
}
