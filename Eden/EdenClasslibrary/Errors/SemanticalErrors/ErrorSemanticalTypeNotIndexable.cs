using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalTypeNotIndexable : SemanticalError
    {
        private IObject _object;

        public ErrorSemanticalTypeNotIndexable(IObject obj, Token token, string line) : base(token, line)
        {
            _object = obj;
        }

        public static AError Create(IObject obj, Token token, string line)
        {
            return new ErrorSemanticalTypeNotIndexable(obj, token, line);
        }

        public static IObject CreateErrorObject(IObject obj, Token token, string line)
        {
            return new ErrorObject(token, Create(obj, token, line));
        }

        public override string GetMessage()
        {
            return $"This type cannot be indexed over!";
        }
    }
}
