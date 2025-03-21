using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.Globalization;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class FloatObject : IObjectComparable
    {
        public float Value { get; set; }
        public Type Type
        {
            get
            {
                return typeof(FloatObject);
            }
        }

        public Token Token { get; }

        private FloatObject(Token token, float value)
        {
            Token = token;
            Value = value;
        }

        public static IObject Create(Token token, float value)
        {
            return new FloatObject(token, value);
        }

        public string AsString()
        {
            return $"{Value.ToString(CultureInfo.InvariantCulture)}";
        }

        public bool IsSameType(IObject other)
        {
            if (Type == other.Type) return true;
            return false;
        }

        public override string ToString()
        {
            return $"Float: {Value}";
        }
        public bool Greater(IObjectComparable other)
        {
            if (IsSameType(other))
            {
                return Value > (other as FloatObject).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public bool Lesser(IObjectComparable other)
        {
            if (IsSameType(other))
            {
                return Value < (other as FloatObject).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public bool Equal(IObjectComparable other)
        {
            if (IsSameType(other))
            {
                return Value == (other as FloatObject).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
