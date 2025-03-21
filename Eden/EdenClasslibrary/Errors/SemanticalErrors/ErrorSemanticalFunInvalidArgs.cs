using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorSemanticalFunInvalidArgs : SemanticalError
    {
        private string _funcName;
        private IObject[] _args;

        public ErrorSemanticalFunInvalidArgs(string funcname, IObject[] args, Token token, string line) : base(token, line)
        {
            _funcName = funcname;
            _args = args;
        }

        public static AError Create(string funcname, IObject[] args, Token token, string line)
        {
            return new ErrorSemanticalFunInvalidArgs(funcname, args, token, line);
        }

        public static IObject CreateErrorObject(string funcname, IObject[] args, Token token, string line)
        {
            return new ErrorObject(token, Create(funcname, args, token, line));
        }

        public override string GetMessage()
        {
            return $"There is no definition for '{_funcName}()' function with {_args.Length} arguments!";
        }
    }
}
