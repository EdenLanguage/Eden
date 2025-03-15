using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalInvalidStatement : SyntacticalError
    {
        private ErrorSyntacticalInvalidStatement() { }

        public static AError Create()
        {
            return new ErrorSyntacticalInvalidStatement();
        }
        public static IObject CreateErrorObject()
        {
            return new ErrorObject(Create());
        }

        public override string GetDetails()
        {
            return $"Invalid statement!";
        }

        public override string GetMessage()
        {
            return $"Provided statement is not valid!";
        }
    }
}
