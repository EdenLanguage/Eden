using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class CharObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(CharObject);
            }
        }

        public char Value { get; set; }

        private CharObject(char value)
        {
            Value = value;
        }

        public static IObject Create(char value)
        {
            return new CharObject(value);
        }

        public override string ToString()
        {
            return Print();
        }

        public string ToAbstractSyntaxTree()
        {
            return ToAbstractSyntaxTree();
        }

        public string ToAbstractSyntaxTree(int indent = 0)
        {
            string symbol = ((int)Value).ToString();
            return $"{Common.IndentCreator(indent)}{nameof(CharObject)} {{ {symbol} }}";
        }

        public string Print(int indents = 0)
        {
            string symbol = ((int)Value).ToString();
            return $"{Common.IndentCreator(indents)}{symbol}";
        }

        public bool IsSameType(IObject other)
        {
            return Type == other.Type;
        }

        public string AsString()
        {
            return Print();
        }
    }
}
