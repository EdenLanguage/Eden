using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalIndexTypeInvalid : SemanticalError
    {
        private IObject _index;

        public ErrorSemanticalIndexTypeInvalid(IObject index, Token token, string line) : base(token, line)
        {
            _index = index;
        }

        public static AError Create(IObject index, Token token, string line)
        {
            return new ErrorSemanticalIndexTypeInvalid(index, token, line);
        }

        public static IObject CreateErrorObject(IObject index, Token token, string line)
        {
            return new ErrorObject(token, Create(index, token, line));
        }

        public override string GetMessage()
        {
            return $"Invalid indexer type. Allowed type is 'Int', actual '{_index.LanguageType}'";
        }
    }
}
