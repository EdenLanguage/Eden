namespace EdenClasslibrary.Types.LanguageTypes.Collections
{
    public interface IObjectComparable : IObject
    {
        bool Greater(IObjectComparable other);
        bool Lesser(IObjectComparable other);
        bool Equal(IObjectComparable other);
    }
}
