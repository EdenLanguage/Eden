using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeVarUndef : RuntimeError
    {
        private string _variableName;
        private ErrorRuntimeVarUndef(string variableName)
        {
            _variableName = variableName;
        }

        public static AError Create(string variableName)
        {
            return new ErrorRuntimeVarUndef(variableName);
        }

        public static IObject CreateErrorObject(string variableName)
        {
            return new ErrorObject(Create(variableName));
        }

        public override string GetDetails()
        {
            return $"Variable not defined!";
        }

        public override string GetMessage()
        {
            return $"Variable '{_variableName}' is not defined!";
        }
    }
}
