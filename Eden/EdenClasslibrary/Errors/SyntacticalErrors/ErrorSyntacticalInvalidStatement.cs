using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalInvalidStatement : SyntacticalError
    {
        public ErrorSyntacticalInvalidStatement(Token token, string line) : base(token, line)
        {
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorSyntacticalInvalidStatement(token, line);
        }
        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Invalid statement!";
        }
    }
}
