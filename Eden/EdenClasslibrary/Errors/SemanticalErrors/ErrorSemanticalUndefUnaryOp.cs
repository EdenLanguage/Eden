using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorSemanticalUndefUnaryOp : SemanticalError
    {
        private TokenType _operationToken;
        private IObject _type;

        public ErrorSemanticalUndefUnaryOp(TokenType op, IObject type, string line) : base(type.Token, line)
        {
            _operationToken = op;
            _type = type;
        }

        public static AError Create(TokenType op, IObject type, string line)
        {
            return new ErrorSemanticalUndefUnaryOp(op, type, line);
        }

        public static IObject CreateErrorObject(TokenType op, IObject type, string line)
        {
            return ErrorObject.Create(type.Token, Create(op, type, line));
        }

        public override string GetMessage()
        {
            return $"Operation '{_operationToken}{_type.Type}' is not defined!";
        }
    }
}
