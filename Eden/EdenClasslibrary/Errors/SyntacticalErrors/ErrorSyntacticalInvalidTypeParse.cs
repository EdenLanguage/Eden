using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors.SyntacticalErrors
{
    public class ErrorSyntacticalInvalidTypeParse : SyntacticalError
    {
        private Token _actual;
        private string _type;

        public ErrorSyntacticalInvalidTypeParse(Token token, string typ, string line) : base(token, line)
        {
            _actual = token;
            _type = typ;
        }

        public static AError Create(Token token, string typ, string line)
        {
            return new ErrorSyntacticalInvalidTypeParse(token, typ, line);
        }

        public override string GetMessage()
        {
            return $"Couldn't convert literal '{_actual.LiteralValue}' to '{_type}' type.";
        }
    }
}
