using EdenClasslibrary.Errors;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class ErrorObject : IObject
    {
        private AError _error;

        public Type Type
        {
            get
            {
                return typeof(ErrorObject);
            }
        }

        public ErrorObject(AError error)
        {
            _error = error;
        }

        public string AsString()
        {
            return _error.PrintError();
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }
    }
}
