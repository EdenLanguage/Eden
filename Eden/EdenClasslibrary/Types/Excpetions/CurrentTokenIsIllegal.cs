using EdenClasslibrary.Errors;
using EdenClasslibrary.Errors.LexicalErrors;

namespace EdenClasslibrary.Types.Excpetions
{
    public class CurrentTokenIsIllegal : Exception
    {
        public AError CreateError(Token token)
        {
            return ErrorLexicalIllegalToken.Create(token);
        }
    }
}
