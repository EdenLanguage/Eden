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

        private BoolObject(bool value)
        {
            Value = value;
        }

        public static IObject Create(bool value)
        {
            return new BoolObject(value);
        }

        public string AsString()
        {
            return $"{Value}";
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }

        public override string ToString()
        {
            return $"Bool: {Value}";
        }
    }
}
