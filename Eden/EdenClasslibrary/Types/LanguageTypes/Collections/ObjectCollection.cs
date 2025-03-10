using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using System.Text;

namespace EdenClasslibrary.Types.LanguageTypes.Collections
{
    public class ObjectCollection : IObjectCollection, IIndexable
    {
        public List<IObject> Collection { get; set; }

        /// <summary>
        /// Returns type of IObject stored inside collection.
        /// </summary>
        public Type Type { get; set; }

        public IntObject Length
        {
            get
            {
                return IntObject.Create(Collection.Count) as IntObject;
            }
        }

        public IObject this[int index] 
        {
            get
            {
                return Collection[index];
            }
            set
            {
                Collection[index] = value;
            }
        }

        private ObjectCollection(Type type, params IObject[] items)
        {
            Type = type;
            Collection = new List<IObject>(items);
        }

        public static IObject Create(Type type, params IObject[] items)
        {
            return new ObjectCollection(type, items);
        }

        public string AsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            for (int i = 0; i < Collection.Count; i++)
            {
                sb.Append($"{(Collection[i] as IPrintable).PrettyPrint()}");
                if (i < Collection.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("]");

            string result = sb.ToString();
            return result;
        }

        public bool IsSameType(IObject other)
        {
            throw new NotImplementedException();
        }

        public void Add(IObject item)
        {
            Collection.Add(item);
        }
    }
}