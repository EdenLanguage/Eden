using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeDivideByZero : RuntimeError
    {
        private IObject _devidedByZero;
        public ErrorRuntimeDivideByZero(IObject obj, string line) : base(obj.Token, line)
        {
            _devidedByZero = obj;
        }

        public static AError Create(IObject obj, string line)
        {
            return new ErrorRuntimeDivideByZero(obj,line);
        }
        public static IObject CreateErrorObject(IObject obj, string line)
        {
            return new ErrorObject(obj.Token, Create(obj, line));
        }

        public override string GetMessage()
        {
            return $"Divide by zero error!";
        }
    }
}
