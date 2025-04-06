using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeVarAlreadyDefined : RuntimeError
    {
        private string _variableName;

        public ErrorRuntimeVarAlreadyDefined(string varname, Token token, string line) : base(token, line)
        {
            _variableName = varname;
        }

        public static AError Create(string varname, Token token, string line)
        {
            return new ErrorRuntimeVarAlreadyDefined(varname, token, line);
        }

        public static IObject CreateErrorObject(string varname, Token token, string line)
        {
            return new ErrorObject(token, Create(varname, token, line));
        }

        public override string GetMessage()
        {
            return $"Variable '{_variableName}' is already defined!";
        }
    }
}
