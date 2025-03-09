using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorInvalidStatement : AError
    {
        private ErrorInvalidStatement() { }

        public static AError Create()
        {
            return new ErrorInvalidStatement();
        }
        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $">     Invalid statement!";
        }

        public override string GetMessage()
        {
            return $"> Provided statement is not valid!";
        }
    }
}
