using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorCollectionArgumentTypeMismatch : AError
    {
        private Type _expected;
        private Type _actual;

        private ErrorCollectionArgumentTypeMismatch(Type expected, Type actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public static AError Create(Type expected, Type actual)
        {
            return new ErrorCollectionArgumentTypeMismatch(expected, actual);
        }

        public static IObject CreateErrorObject(Type expected, Type actual)
        {
            return new ErrorObject(Create(expected, actual));
        }

        public override string GetDetails()
        {
            return $"Collection is of type '{_expected}' but provided value is of type '{_actual}'!";
        }

        public override string GetMessage()
        {
            return $"Collection argument faced mismatch!";
        }
    }
}
