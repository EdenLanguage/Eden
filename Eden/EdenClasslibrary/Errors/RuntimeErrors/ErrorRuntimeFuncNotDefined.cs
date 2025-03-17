using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeFuncNotDefined : RuntimeError
    {
        private string _funcName;
        private ErrorRuntimeFuncNotDefined(string funcname)
        {
            _funcName = funcname;
        }

        public static AError Create(string funcname)
        {
            return new ErrorRuntimeFuncNotDefined(funcname);
        }

        public static IObject CreateErrorObject(string funcname)
        {
            return new ErrorObject(Create(funcname));
        }

        public override string GetDetails()
        {
            return $"Function with name '{_funcName}' is not defined!";
        }

        public override string GetMessage()
        {
            return $"Function not defined!";
        }
    }
}
