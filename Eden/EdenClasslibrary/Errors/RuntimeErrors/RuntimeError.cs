namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public abstract class RuntimeError : AError
    {
        public override string ErrorType
        {
            get
            {
                return "Runtime error";
            }
        }
    }
}
