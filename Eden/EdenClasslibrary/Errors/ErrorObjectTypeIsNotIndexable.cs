using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorObjectTypeIsNotIndexable : AError
    {
        private IObject _object;

        private ErrorObjectTypeIsNotIndexable(IObject obj)
        {
            _object = obj;
        }

        public static AError Create(IObject obj)
        {
            return new ErrorObjectTypeIsNotIndexable(obj);
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
            return $"Object type cannot be indexed over!";
        }
    }
}
