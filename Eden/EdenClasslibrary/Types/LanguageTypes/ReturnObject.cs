
namespace EdenClasslibrary.Types.LanguageTypes
{
    public class ReturnObject : IObject
    {
        public IObject WrappedObject { get; set; }
        public Type Type
        {
            get
            {
                return typeof(ReturnObject);
            }
        }

        public Token Token { get; }

        public ReturnObject(Token token, IObject wrappedObj)
        {
            Token = token;
            WrappedObject = wrappedObj;
        }
        public string AsString()
        {
            return $"{nameof(ReturnObject)}: {WrappedObject.AsString()}";
        }

        public bool IsSameType(IObject other)
        {
            if (Type == other.Type) return true;
            return false;
        }

        public override string ToString()
        {
            return $"Return : {WrappedObject.ToString()}";
        }
    }
}