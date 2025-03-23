using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalIndexAssignError : SemanticalError
    {
        private IObject _collection;
        private IObject _assingmentObj;
        public ErrorSemanticalIndexAssignError(IObject collection, IObject assingmentObj, string line) : base(collection.Token, line)
        {
            _collection = collection;
            _assingmentObj = assingmentObj;
        }

        public static AError Create(IObject collection, IObject assingmentObj, string line)
        {
            return new ErrorSemanticalIndexAssignError(collection, assingmentObj, line);
        }

        public static IObject CreateErrorObject(IObject collection, IObject assingmentObj, string line)
        {
            return ErrorObject.Create(collection.Token, Create(collection, assingmentObj, line));
        }

        public override string GetMessage()
        {
            return $"Cannot assign '{_collection.LanguageType}({_collection.Token.LiteralValue}) = {_assingmentObj.LanguageType}({_assingmentObj.Token.LiteralValue})'";
        }
    }
}
