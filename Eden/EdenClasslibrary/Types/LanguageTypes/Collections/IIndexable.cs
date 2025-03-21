namespace EdenClasslibrary.Types.LanguageTypes.Collections
{
    public interface IIndexable
    {
        int Length { get; }
        IObject this[int index] { get; set; }
    }
}