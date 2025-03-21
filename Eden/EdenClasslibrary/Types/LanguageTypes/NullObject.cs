namespace EdenClasslibrary.Types.LanguageTypes
{
    public class NullObject : IObject
    {
        /*  Well to be honest null type doesn't require Value field because null can only be null, nothing else.
         */

        public Type Type
        {
            get
            {
                return typeof(NullObject);
            }
        }
        public object Value { get; set; }

        public Token Token { get; }

        private NullObject(Token token, object value)
        {
            Token = token;
            Value = value;
        }

        public static IObject Create(Token token)
        {
            return new NullObject(token, null);
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }

        public string AsString()
        {
            return "Null";
        }

        public override string ToString()
        {
            return "Null";
        }
    }
}
