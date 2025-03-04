
namespace EdenClasslibrary.Types.LanguageTypes
{
    public class IntObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(IntObject);
            }
        }
        public int Value { get; set; }

        public IntObject(int value)
        {
            Value = value;
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }
        public string AsString()
        {
            return $"{Value}";
        }
    }
}
