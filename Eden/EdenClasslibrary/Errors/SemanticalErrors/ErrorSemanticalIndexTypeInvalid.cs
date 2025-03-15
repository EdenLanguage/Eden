using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalIndexTypeInvalid : AError
    {
        private IObject _index;

        private ErrorSemanticalIndexTypeInvalid(IObject index)
        {
            _index = index;
        }

        public static AError Create(IObject index)
        {
            return new ErrorSemanticalIndexTypeInvalid(index);
        }

        public static IObject CreateErrorObject(IObject index)
        {
            return new ErrorObject(Create(index));
        }

        public override string GetDetails()
        {
            return $"Used indexer: '{_index.Type}'";
        }

        public override string GetMessage()
        {
            return $"Indexer is not valid!";
        }
    }
}
