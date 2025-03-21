using EdenClasslibrary.Types;
using System.Text;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalUnexpectedTokens : SyntacticalError
    {
        private Token _actual;
        private TokenType[] _expected;

        public ErrorSyntacticalUnexpectedTokens(TokenType[] expected, Token token, string line) : base(token, line)
        {
            _actual = token;
            _expected = expected;
        }

        public static AError Create(TokenType[] expected, Token token, string line)
        {
            return new ErrorSyntacticalUnexpectedTokens(expected, token, line);
        }

        private string GetExpected()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < _expected.Length; i++)
            {
                sb.Append($"'{_expected[i]}'");
                if(i < _expected.Length - 1)
                {
                    sb.Append(" or ");
                }
            }

            return sb.ToString();
        }

        public override string GetMessage()
        {
            return $"Parser expected {GetExpected()} token but acutal token was '{_actual.Keyword}'.";
        }
    }
}
