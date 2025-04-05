using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Errors.RuntimeErrors
{
    public class ErrorRuntimeExpressionEvaluationFail : RuntimeError
    {
        private Type _expected;
        private Type _actual;

        public ErrorRuntimeExpressionEvaluationFail(Type expected, Type actual, Token token, string line) : base(token, line)
        {
            _expected = expected;
            _actual = actual;
        }

        public static AError Create(Type expected, Type actual, Token token, string line)
        {
            return new ErrorRuntimeExpressionEvaluationFail(expected, actual, token, line);
        }

        public static IObject CreateErrorObject(Type expected, Type actual, Token token, string line)
        {
            return new ErrorObject(token, Create(expected, actual, token, line));
        }

        public override string GetMessage()
        {
            return $"Expression should evaluate to type '{Variables.NameFromType(_expected)}' but it evaluates to '{Variables.NameFromType(_actual)}'";
        }
    }
}
