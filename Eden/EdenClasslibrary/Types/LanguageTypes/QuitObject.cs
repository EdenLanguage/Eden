namespace EdenClasslibrary.Types.LanguageTypes
{
    public class QuitObject : IObject
    {
        public Type Type => throw new NotImplementedException();

        public static IObject Create()
        {
            return new QuitObject();
        }

        public string AsString()
        {
            throw new NotImplementedException();
        }

        public bool IsSameType(IObject other)
        {
            throw new NotImplementedException();
        }
    }
}
