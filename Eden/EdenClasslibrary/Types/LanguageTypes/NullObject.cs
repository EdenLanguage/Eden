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

        private NullObject(object value)
        {
            Value = value;
        }

        public static IObject Create()
        {
            return new NullObject(null);
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
