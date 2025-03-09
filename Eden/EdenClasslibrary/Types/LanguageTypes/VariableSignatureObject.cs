namespace EdenClasslibrary.Types.LanguageTypes
{
    public class VariableSignatureObject : IObject
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        private VariableSignatureObject(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public static IObject Create(string name, Type type)
        {
            return new VariableSignatureObject(name, type);
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
