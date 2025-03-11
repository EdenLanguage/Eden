using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenClasslibrary.Types.LanguageTypes
{
    /// <summary>
    /// TODO: Implement that, it is placeholder.
    /// </summary>
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

        public IntObject Length
        {
            get
            {
                return IntObject.Create(Value.Length) as IntObject;
            }
        }

        public IObject this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();

            }
        }

        public static IObject Create(string initialValue)
        {
            return new StringObject(initialValue);
        }

        private StringObject(string value)
        {
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
            throw new NotImplementedException();
        }

        public bool Greater(IObjectComparable other)
        {
            if (IsSameType(other))
            {
                return Value.Length > (other as StringObject).Value.Length;
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
                return Value.Length < (other as StringObject).Value.Length;
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
                return Value == (other as StringObject).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
