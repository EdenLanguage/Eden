namespace EdenClasslibrary.Types.LanguageTypes
{
    public class NoneObject : IObject
    {
        public Token Token { get; }

        public Type Type
        {
            get
            {
                return typeof(NoneObject);
            }
        }

        public string LanguageType
        {
            get
            {
                return "None";
            }
        }

        private NoneObject(Token token)
        {
            Token = token;
        }

        public static IObject Create(Token token)
        {
            return new NoneObject(token);
        }

        public override string ToString()
        {
            return AsString();
        }

        public string AsString()
        {
            return "None";
        }

        public bool IsSameType(IObject other)
        {
            if (Type == other.Type)
            {
                return true;
            }
            else return false;
        }
    }
}