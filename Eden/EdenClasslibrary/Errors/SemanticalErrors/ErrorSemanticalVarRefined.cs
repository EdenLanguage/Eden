using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    class ErrorSemanticalVarRefined : SemanticalError
    {
        private string _variableName;
        private ErrorSemanticalVarRefined(string variableName)
        {
            _variableName = variableName;
        }

        public static AError Create(string variableName)
        {
            return new ErrorSemanticalVarRefined(variableName);
        }

        public static IObject CreateErrorObject(string variableName)
        {
            return new ErrorObject(Create(variableName));
        }

        public override string GetDetails()
        {
            return $"Variable redefined!";
        }

        public override string GetMessage()
        {
            return $"Variable with name '{_variableName}' is already defined!";
        }
    }
}
