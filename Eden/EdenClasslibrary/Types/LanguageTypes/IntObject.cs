
using System.Reflection.Metadata.Ecma335;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class IntObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(IntObject);
            }
        }
        public int Value { get; set; }

        private IntObject(int value)
        {
            Value = value;
        }

        public static IObject Create(int value)
        {
            return new IntObject(value);
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }
        public string AsString()
        {
            return $"{Value}";
        }

        public override string ToString()
        {
            return $"Int: {Value}";
        }
    }
}
