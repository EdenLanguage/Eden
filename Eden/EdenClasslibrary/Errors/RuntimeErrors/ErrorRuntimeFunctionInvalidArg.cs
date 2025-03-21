using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeFunctionInvalidArg : RuntimeError
    {
        private string _funcName;
        private IObject _obj;

        public ErrorRuntimeFunctionInvalidArg(string funcname, IObject obj, Token token, string line) : base(token, line)
        {
            _funcName = funcname;
            _obj = obj;
        }

        public static AError Create(string funcname, IObject obj, Token token, string line)
        {
            return new ErrorRuntimeFunctionInvalidArg(funcname, obj, token, line);
        }

        public static IObject CreateErrorObject(string funcname, IObject obj, Token token, string line)
        {
            return new ErrorObject(token, Create(funcname, obj, token, line));
        }

        public override string GetMessage()
        {
            return $"No definition for function '{_funcName}()' that accepts '{_obj.AsString()}' as an argument!";
        }
    }
}
