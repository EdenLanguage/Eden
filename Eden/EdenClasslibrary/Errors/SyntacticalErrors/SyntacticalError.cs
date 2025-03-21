using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public abstract class SyntacticalError : AError
    {
        protected SyntacticalError(Token token, string line) : base(token, line)
        {
        }

        public override string ErrorType
        {
            get
            {
                return "Syntactical error";
            }
        }
    }
}
