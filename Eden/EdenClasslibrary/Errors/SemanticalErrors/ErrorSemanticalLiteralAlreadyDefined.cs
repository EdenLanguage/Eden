using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalLiteralAlreadyDefined : SemanticalError
    {
        public ErrorSemanticalLiteralAlreadyDefined(Token token, string line) : base(token, line)
        {
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorSemanticalLiteralAlreadyDefined(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Literal '{Token.LiteralValue}' is already defined!";
        }
    }
}
