namespace EdenClasslibrary.Types.LanguageTypes
{
    public class QuitObject : IObject
    {
        public Type Type => throw new NotImplementedException();

        public Token Token { get; }

        private QuitObject(Token token)
        {
            Token = token;
        }

        public static IObject Create(Token token)
        {
            return new QuitObject(token);
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
