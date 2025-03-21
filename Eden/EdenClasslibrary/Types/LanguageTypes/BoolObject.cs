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

        public Token Token { get; }

        private BoolObject(Token token,bool value)
        {
            Token = token;
            Value = value;
        }

        public static IObject Create(Token token, bool value)
        {
            return new BoolObject(token, value);
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
