namespace EdenClasslibrary.Types.LanguageTypes
{
    public interface IObject
    {
        string LanguageType { get; }
        Token Token { get; }
        Type Type { get; }
        bool IsSameType(IObject other);
        string AsString();
    }
}
