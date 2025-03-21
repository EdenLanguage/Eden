using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.Text;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class StringObject : IObjectComparable, IIndexable
    {
        public Type Type
        {
            get
            {
                return typeof(StringObject);
            }
        }

        public string Value { get; set; }

        public int Length
        {
            get
            {
                return Value.Length;
            }
        }

        public Token Token { get; }

        public IObject this[int index]
        {
            get
            {
                char val = Value[index];
                return CharObject.Create(Token, val);
            }
            set
            {
                StringBuilder sb = new StringBuilder(Value);

                sb[index] = (value as CharObject).Value;

                Value = sb.ToString();
            }
        }

        public static IObject Create(Token token, string initialValue)
        {
            return new StringObject(token, initialValue);
        }

        private StringObject(Token token, string value)
        {
            Token = token;
            Value = value;
        }

        public string AsString()
        {
            return Value;
        }

        public override string ToString()
        {
            return AsString();
        }

        public bool IsSameType(IObject other)
        {
            if (Type == other.Type)
            {
                return true;
            }
            else return false;
        }

        public bool Greater(IObjectComparable other)
        {
            return Value.Length > (other as StringObject).Value.Length;
        }

        public bool Lesser(IObjectComparable other)
        {
            return Value.Length < (other as StringObject).Value.Length;
        }

        public bool Equal(IObjectComparable other)
        {
            return Value == (other as StringObject).Value;
        }
    }
}