using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalStatementNotDefinedInTopLvl : SemanticalError
    {
        public ErrorSemanticalStatementNotDefinedInTopLvl(Token token, string line) : base(token, line) { }

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
            return $"Statement '{Token.LiteralValue}' cannot be used in Top-Level statements scope!";
        }
    }
}
