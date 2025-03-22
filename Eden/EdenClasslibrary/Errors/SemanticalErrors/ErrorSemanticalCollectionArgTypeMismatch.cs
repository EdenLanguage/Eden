using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalCollectionArgTypeMismatch : SemanticalError
    {
        private Type _expected;
        private Type _actual;

        public ErrorSemanticalCollectionArgTypeMismatch(Type expected, Type actual, Token token, string line) : base(token, line)
        {
            _expected = expected;
            _actual = actual;
        }

        public static AError Create(Type expected, Type actual, Token token, string line)
        {
            return new ErrorSemanticalCollectionArgTypeMismatch(expected, actual, token, line);
        }

        public static IObject CreateErrorObject(Type expected, Type actual, Token token, string line)
        {
            return new ErrorObject(token, Create(expected, actual, token, line));
        }

        public override string GetMessage()
        {
            return $"Collection is of type '{Variables.NameFromType(_expected)}' but provided value is of type '{Variables.NameFromType(_actual)}'!";
        }
    }
}
