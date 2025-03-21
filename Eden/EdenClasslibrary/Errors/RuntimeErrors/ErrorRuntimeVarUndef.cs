using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeVarUndef : RuntimeError
    {
        private string _variableName;

        public ErrorRuntimeVarUndef(string varname, Token token, string line) : base(token, line)
        {
            _variableName = varname;
        }

        public static AError Create(string varname, Token token, string line)
        {
            return new ErrorRuntimeVarUndef(varname, token, line);
        }

        public static IObject CreateErrorObject(string varname, Token token, string line)
        {
            return new ErrorObject(token, Create(varname, token, line));
        }

        public override string GetMessage()
        {
            return $"Variable '{_variableName}' is not defined!";
        }
    }
}
