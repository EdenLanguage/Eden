namespace EdenClasslibrary.Types.LanguageTypes
{
    public class FloatObject : IObject
    {
        public float Value { get; set; }
        public Type Type
        {
            get
            {
                return typeof(FloatObject);
            }
        }

        private FloatObject(float value)
        {
            Value = value;
        }

        public static IObject Create(float value)
        {
            return new FloatObject(value);
        }

        public string AsString()
        {
            return $"{Value}";
        }

        public bool IsSameType(IObject other)
        {
            if (Type == other.Type) return true;
            return false;
        }

        public override string ToString()
        {
            return $"Float: {Value}";
        }
    }
}
