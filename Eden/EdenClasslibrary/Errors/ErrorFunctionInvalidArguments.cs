using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorFunctionInvalidArguments : AError
    {
        private ErrorFunctionInvalidArguments()
        {

        }

        public static AError Create()
        {
            return new ErrorFunctionInvalidArguments();
        }

        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $"Invalid function call!";
        }

        public override string GetMessage()
        {
            return $"There is no definition for function with that name that has provided amount of arguments!";
        }
    }
}
