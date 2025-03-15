using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorSemanticalFunInvalidArgs : SemanticalError
    {
        private string _funcName;
        private IObject[] _args;

        private ErrorSemanticalFunInvalidArgs(string funcname, IObject[] args)
        {
            _funcName = funcname;
            _args = args;
        }

        public static AError Create(string funcname, IObject[] args)
        {
            return new ErrorSemanticalFunInvalidArgs(funcname, args);
        }

        public static IObject CreateErrorObject(string funcname, IObject[] args)
        {
            return new ErrorObject(Create(funcname, args));
        }

        public override string GetDetails()
        {
            return $"There is no definition for '{_funcName}()' function with {_args.Length} arguments!";
        }

        public override string GetMessage()
        {
            return $"Invalid function call!";
        }
    }
}
