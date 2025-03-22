using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalUndefBinaryOpExp : SemanticalError
    {
        public ErrorSemanticalUndefBinaryOpExp(Token token, string line) : base(token, line) { }

        public static AError Create(Token token, string line)
        {
            return new ErrorSemanticalUndefBinaryOpExp(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Binary operator '{Token.LiteralValue}' is not defined!";
        }
    }
}
