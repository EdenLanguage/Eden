namespace EdenClasslibrary.Types.LanguageTypes.Collections
{
    public interface IObjectCollection : IObject
    {
        List<IObject> Collection { get; set; }
        void Add(IObject item);
    }
}
