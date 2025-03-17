namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public abstract class SemanticalError : AError
    {
        public override string ErrorType
        {
            get
            {
                return "Semantical analysis error";
            }
        }
    }
}
