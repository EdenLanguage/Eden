
using System.Net.Http.Headers;

namespace EdenClasslibrary.Types.LanguageTypes
{
    /// <summary>
    /// TODO: Implement that, it is placeholder.
    /// </summary>
    public class StringObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(StringObject);
            }
        }

        public string Value { get; set; }

        public static IObject Create(string initialValue)
        {
            return new StringObject();
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
