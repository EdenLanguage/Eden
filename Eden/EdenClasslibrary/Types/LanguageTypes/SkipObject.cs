
namespace EdenClasslibrary.Types.LanguageTypes
{
    public class SkipObject : IObject
    {
        public SkipObject()
        {
        }
        public static IObject Create()
        {
            return new SkipObject();
        }
        public Type Type => throw new NotImplementedException();

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
