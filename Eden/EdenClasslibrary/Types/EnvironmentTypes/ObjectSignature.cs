namespace EdenClasslibrary.Types.EnvironmentTypes
{
    public class ObjectSignature
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        private ObjectSignature(string name, Type type)
        {
            Name = name;
            Type = type;
        }
        public static ObjectSignature Create(string name, Type type)
        {
            return new ObjectSignature(name, type);
        }

        public override string ToString()
        {
            return $"{Type.Name} {Name}";
        }
    }
}
