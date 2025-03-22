
namespace EdenClasslibrary.Types.LanguageTypes
{
    public class SkipObject : IObject
    {
        public SkipObject(Token token)
        {
            Token = token;
        }
        public string LanguageType
        {
            get
            {
                return "Skip";
            }
        }
        public static IObject Create(Token token)
        {
            return new SkipObject(token);
        }
        public Type Type => throw new NotImplementedException();

        public Token Token { get; }

        public string AsString()
        {
            throw new NotImplementedException();
        }

        public bool IsSameType(IObject other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return AsString();
        }
    }
}
