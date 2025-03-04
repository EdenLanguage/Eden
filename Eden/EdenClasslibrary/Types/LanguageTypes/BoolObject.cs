
namespace EdenClasslibrary.Types.LanguageTypes
{
    public class BoolObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(BoolObject);
            }
        }
        public bool Value { get; set; }
        public BoolObject(bool value)
        {
            Value = value;
        }
        public string AsString()
        {
            return $"{Value}";
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }
    }
}
