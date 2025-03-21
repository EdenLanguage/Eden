namespace EdenClasslibrary.Types.LanguageTypes
{
    public static class ObjectFactory
    {
        public static IObject Create(Token token, Type type)
        {
            IObject result = null;
            switch (type.Name)
            {
                case "IntObject":
                    result = IntObject.Create(token, 0);
                    break;
                case "FloatObject":
                    result = FloatObject.Create(token, 0);
                    break;
                case "NullObject":
                    result = NullObject.Create(token);
                    break;
                case "StringObject":
                    result = StringObject.Create(token, "");
                    break;
                default:
                    result = NullObject.Create(token);
                    break;
            }
            return result;
        }
    }
}