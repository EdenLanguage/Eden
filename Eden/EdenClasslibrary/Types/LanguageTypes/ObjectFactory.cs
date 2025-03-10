namespace EdenClasslibrary.Types.LanguageTypes
{
    public static class ObjectFactory
    {
        public static IObject Create(Type type)
        {
            IObject result = null;
            switch (type.Name)
            {
                case "IntObject":
                    result = IntObject.Create(0);
                    break;
                case "FloatObject":
                    result = FloatObject.Create(0);
                    break;
                case "NullObject":
                    result = NullObject.Create();
                    break;
                case "StringObject":
                    result = StringObject.Create("");
                    break;
                default:
                    result = NullObject.Create();
                    break;
            }
            return result;
        }
    }
}