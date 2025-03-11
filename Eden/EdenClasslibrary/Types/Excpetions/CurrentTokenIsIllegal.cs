using EdenClasslibrary.Errors;

namespace EdenClasslibrary.Types.Excpetions
{
    public class CurrentTokenIsIllegal : Exception
    {
        public AError CreateError(Token token)
        {
            return ErrorIllegalToken.Create(token);
        }
    }
}
