namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public abstract class SyntacticalError : AError
    {
        public override string ErrorType
        {
            get
            {
                return "Syntactical analysis error";
            }
        }
    }
}
