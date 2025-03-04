namespace EdenClasslibrary.Types.LanguageTypes
{
    public interface IObject
    {
        Type Type { get; }
        bool IsSameType(IObject other);
        string AsString();
    }
}
