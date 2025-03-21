using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalIllegalAssing : SemanticalError
    {
        public ErrorSemanticalIllegalAssing(Token token, string line) : base(token, line)
        {
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorSemanticalIllegalAssing(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return ErrorObject.Create(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Assingment operation is not defined for '{Token.Keyword}' token";
        }
    }
}
