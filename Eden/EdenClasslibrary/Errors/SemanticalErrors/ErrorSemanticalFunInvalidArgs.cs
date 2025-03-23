using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using System.Text;

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
            StringBuilder sb = new StringBuilder();

            if(_args.Length > 0)
            {
                sb.Append(" with '");
                for(int i = 0; i < _args.Length; i++)
                {
                    IObject arg = _args[i];
                    sb.Append($"{arg.LanguageType}({arg.AsString()})");
                    if(i < _args.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append("' argument");
                if(_args.Length != 1)
                {
                    sb.Append("s");
                }
            }

            string result = sb.ToString();

            return $"There is no definition for '{_funcName}()' function{result}!";
        }
    }
}
