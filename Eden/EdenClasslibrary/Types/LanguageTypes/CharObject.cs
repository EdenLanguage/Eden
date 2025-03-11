using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class CharObject : IObject, IPrintable
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
            return PrettyPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            string symbol = ((int)Value).ToString();
            return $"{Common.IndentCreator(indent)}{nameof(CharObject)} {{ {symbol} }}";
        }

        public string PrettyPrint(int indents = 0)
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
            return PrettyPrint();
        }
    }
}
