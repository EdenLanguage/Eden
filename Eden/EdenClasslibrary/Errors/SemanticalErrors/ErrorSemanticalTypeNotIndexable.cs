using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalTypeNotIndexable : SemanticalError
    {
        private IObject _object;

        private ErrorSemanticalTypeNotIndexable(IObject obj)
        {
            _object = obj;
        }

        public static AError Create(IObject obj)
        {
            return new ErrorSemanticalTypeNotIndexable(obj);
        }

        public static IObject CreateErrorObject(IObject obj)
        {
            return new ErrorObject(Create(obj));
        }

        public override string GetDetails()
        {
            return $"Object of type '{_object.Type}' doesn't have defined indexer!";
        }

        public override string GetMessage()
        {
            return $"This type cannot be indexed over!";
        }
    }
}
