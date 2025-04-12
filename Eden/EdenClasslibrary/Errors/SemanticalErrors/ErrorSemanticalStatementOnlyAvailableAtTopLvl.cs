using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalStatementOnlyAvailableAtTopLvl : SemanticalError
    {
        public ErrorSemanticalStatementOnlyAvailableAtTopLvl(Token token, string line) : base(token, line) { }

        public static AError Create(Token token, string line)
        {
            return new ErrorSemanticalStatementNotDefinedInTopLvl(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Statement '{Token.LiteralValue}' is only possible at Top-Level scope!";
        }
    }
}
