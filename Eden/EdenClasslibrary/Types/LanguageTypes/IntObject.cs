using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class IntObject : IObjectComparable
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

        public bool Greater(IObjectComparable other)
        {
            if (IsSameType(other))
            {
                return Value > (other as IntObject).Value;
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
                return Value < (other as IntObject).Value;
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
                return Value == (other as IntObject).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
