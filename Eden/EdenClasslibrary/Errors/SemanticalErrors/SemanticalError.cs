using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public abstract class SemanticalError : AError
    {
        protected SemanticalError(Token token, string line) : base(token, line)
        {
        }

        public override string ErrorType
        {
            get
            {
                return "Semantical error";
            }
        }
    }
}
