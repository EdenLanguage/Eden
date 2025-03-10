using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors
{
    public class ErrorIndexNotValid : AError
    {
        private IObject _index;

        private ErrorIndexNotValid(IObject index)
        {
            _index = index;
        }

        public static AError Create(IObject index)
        {
            return new ErrorIndexNotValid(index);
        }

        public static IObject CreateErrorObject(IObject index)
        {
            return new ErrorObject(Create(index));
        }

        public override string GetDetails()
        {
            return $"Indexer should be 'Int' type with value greater than 0. Actual value type: '{_index.Type}'." + ((_index is IntObject asInt) ? $" With value: '{asInt.Value}'" : "");
        }

        public override string GetMessage()
        {
            return $"Used indexer is not valid!";
        }
    }
}
