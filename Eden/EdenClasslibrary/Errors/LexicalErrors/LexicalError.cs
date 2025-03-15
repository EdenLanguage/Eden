namespace EdenClasslibrary.Errors.LexicalErrors
{
    public abstract class LexicalError : AError
    {
        public override string ErrorType
        {
            get
            {
                return "Lexical analysis error";
            }
        }
    }
}
