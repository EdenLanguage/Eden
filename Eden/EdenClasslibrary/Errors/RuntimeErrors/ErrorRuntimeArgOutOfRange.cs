using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeArgOutOfRange : RuntimeError
    {
        private IIndexable _indexable;
        private int _position;
        private Token _token;

        public ErrorRuntimeArgOutOfRange(IIndexable indexable, int position, Token token, string line) : base(token, line)
        {
            _indexable = indexable;
            _position = position;
            _token = token;
        }

        public static AError Create(IIndexable indexable, int position, Token token, string line)
        {
            return new ErrorRuntimeArgOutOfRange(indexable, position, token, line);
        }

        public static IObject CreateErrorObject(IIndexable indexable, int position, Token token, string line)
        {
            return new ErrorObject(token, Create(indexable, position, token, line));
        }

        public override string GetMessage()
        {
            return $"Argument out of range! Index '{_position}'";
        }
    }
}
