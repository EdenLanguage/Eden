using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeDivideByZero : RuntimeError
    {
        private ErrorRuntimeDivideByZero() { }

        public static AError Create()
        {
            return new ErrorRuntimeDivideByZero();
        }
        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $"Dividing by zero is not defied!";
        }

        public override string GetMessage()
        {
            return $"Divide by zero error!";
        }
    }
}
