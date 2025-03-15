using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeFunctionInvalidArg : RuntimeError
    {
        private string _funcName;
        private IObject _obj;

        private ErrorRuntimeFunctionInvalidArg(string funcname, IObject obj)
        {
            _funcName = funcname;
            _obj = obj;
        }

        public static AError Create(string funcname, IObject obj)
        {
            return new ErrorRuntimeFunctionInvalidArg(funcname, obj);
        }

        public static IObject CreateErrorObject(string funcname, IObject obj)
        {
            return new ErrorObject(Create(funcname, obj));
        }

        public override string GetDetails()
        {
            return $"There is no definition for '{_funcName}()' function that accepts '{_obj.AsString()}' as an argument!";
        }

        public override string GetMessage()
        {
            return $"Function '{_funcName}()' function doesn't accept such type of arguments!";
        }
    }
}
