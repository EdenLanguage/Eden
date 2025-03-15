using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeArgOutOfRange : RuntimeError
    {
        private IIndexable _indexable;
        private IntObject _position;

        private ErrorRuntimeArgOutOfRange(IIndexable indexable, IntObject index)
        {
            _indexable = indexable;
            _position = index;
        }

        public static AError Create(IIndexable indexable, IntObject index)
        {
            return new ErrorRuntimeArgOutOfRange(indexable, index);
        }

        public static IObject CreateErrorObject(IIndexable indexable, IntObject index)
        {
            return new ErrorObject(Create(indexable, index));
        }

        public override string GetDetails()
        {
            return $"Objects length is '{_indexable.Length}' and called index was '{_position}'!";
        }

        public override string GetMessage()
        {
            return $"Index out of range error!";
        }
    }
}
