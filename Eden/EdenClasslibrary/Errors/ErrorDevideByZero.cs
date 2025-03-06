using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorDevideByZero : AError
    {
        private ErrorDevideByZero() { }

        public static AError Create()
        {
            return new ErrorDevideByZero();
        }
        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $"> Invalid operation!";
        }

        public override string GetMessage()
        {
            return $">     It is not possible to devide by zero!";
        }
    }
}
