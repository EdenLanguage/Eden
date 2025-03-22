namespace EdenClasslibrary.Types.LanguageTypes
{
    public class VariableSignatureObject : IObject
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public Token Token { get; }

        private VariableSignatureObject(Token token, string name, Type type)
        {
            Token = token;
            Name = name;
            Type = type;
        }
        public string LanguageType
        {
            get
            {
                return "Variable";
            }
        }
        public static IObject Create(Token token, string name, Type type)
        {
            return new VariableSignatureObject(token, name, type);
        }

        public override string ToString()
        {
            return $"Var {Type} {Name}";
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
