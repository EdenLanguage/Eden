using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalLiteralEvaluation : SemanticalError
    {
        public ErrorSemanticalLiteralEvaluation(Token token, string line) : base(token, line)
        {
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorSemanticalLiteralEvaluation( token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return new ErrorObject(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Cannot create definition of Literal with value '{Token.LiteralValue}' because it cannot be replaced with Literal value!";
        }
    }
}