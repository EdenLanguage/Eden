using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorIncFunctionInvalidArgument : AError
    {
        private IObject _obj;

        private ErrorIncFunctionInvalidArgument(IObject obj)
        {
            _obj = obj;
        }

        public static AError Create(IObject obj)
        {
            return new ErrorIncFunctionInvalidArgument(obj);
        }

        public static IObject CreateErrorObject(IObject obj)
        {
            return new ErrorObject(Create(obj));
        }

        public override string GetDetails()
        {
            return $"There is no definition for Inc() function that accepts '{_obj.Type.Name}' as an argument!";
        }

        public override string GetMessage()
        {
            return $"Inc() function doesnt accept such type of arguments!";
        }
    }
}
