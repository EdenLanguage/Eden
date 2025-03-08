using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorFunctionNotDefined : AError
    {
        private ErrorFunctionNotDefined()
        {

        }

        public static AError Create()
        {
            return new ErrorFunctionNotDefined();
        }

        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $">     Invalid function call!";
        }

        public override string GetMessage()
        {
            return $"> There is no definition for function with that name!";
        }
    }
}
