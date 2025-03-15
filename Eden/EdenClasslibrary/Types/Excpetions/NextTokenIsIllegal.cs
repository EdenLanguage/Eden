using EdenClasslibrary.Errors.LexicalErrors;
using EdenClasslibrary.Errors;

namespace EdenClasslibrary.Types.Excpetions
{
    class NextTokenIsIllegal : Exception
    {
        public AError CreateError(Token token)
        {
            return ErrorLexicalIllegalToken.Create(token);
        }
    }
}
