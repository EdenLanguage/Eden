namespace EdenClasslibrary.Types.LanguageTypes.Collections
{
    public interface IIndexable
    {
        IntObject Length { get; }
        IObject this[int index] { get; set; }
    }
}