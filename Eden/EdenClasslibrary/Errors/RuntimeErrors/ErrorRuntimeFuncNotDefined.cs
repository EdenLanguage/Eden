using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeFuncNotDefined : RuntimeError
    {
        private string _funcName;

        public ErrorRuntimeFuncNotDefined(Token token, string line) : base(token, line)
        {
            _funcName = token.LiteralValue;
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorRuntimeFuncNotDefined(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Function '{_funcName}()' is not defined!";
        }
    }
}
