using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.LexicalErrors
{
    public abstract class LexicalError : AError
    {
        public LexicalError(Token token, string line) : base(token, line) { }

        public override string ErrorType
        {
            get
            {
                return "Lexical error";
            }
        }
    }
}
