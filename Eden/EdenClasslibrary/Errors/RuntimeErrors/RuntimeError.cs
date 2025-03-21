using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public abstract class RuntimeError : AError
    {
        protected RuntimeError(Token token, string line) : base(token, line) { }

        public override string ErrorType
        {
            get
            {
                return "Runtime error";
            }
        }
    }
}
