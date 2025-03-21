using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeFailedToEvaluate : RuntimeError
    {
        public ErrorRuntimeFailedToEvaluate(Token token, string line) : base(token, line)
        {
        }

        public static AError Create(Token token, string line)
        {
            return new ErrorRuntimeFailedToEvaluate(token, line);
        }

        public static IObject CreateErrorObject(Token token, string line)
        {
            return ErrorObject.Create(token, Create(token, line));
        }

        public override string GetMessage()
        {
            return $"Failed to evaluate '{Token.LiteralValue}'";
        }
    }
}
