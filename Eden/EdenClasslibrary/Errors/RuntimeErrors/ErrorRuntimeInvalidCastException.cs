using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeInvalidCastException : RuntimeError
    {
        private IObject _casted;
        private Type _expectedType;
        public ErrorRuntimeInvalidCastException(Type expectedType, IObject casted, Token token, string line) : base(token, line)
        {
            _casted = casted;
            _expectedType = expectedType;
        }

        public static AError Create(Type expectedType, IObject casted, Token token, string line)
        {
            return new ErrorRuntimeInvalidCastException(expectedType, casted, token, line);
        }

        public static IObject CreateErrorObject(Type expectedType, IObject casted, Token token, string line)
        {
            return new ErrorObject(token, Create(expectedType, casted, token, line));
        }

        public override string GetMessage()
        {
            return $"Failed to cast '{_casted.Type}({_casted.AsString()})' to '{_expectedType}'!";
        }
    }
}
