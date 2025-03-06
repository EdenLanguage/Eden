using EdenClasslibrary.Types;
using System.Runtime.CompilerServices;

namespace EdenClasslibrary.Errors
{
    public class ErrorTokenParsingError : AError
    {
        private Token _invalidToken;

        private ErrorTokenParsingError(Token invalidToken)
        {
            //_invalidToken;
        }

        public override string GetDetails()
        {
            throw new NotImplementedException();
        }

        public override string GetMessage()
        {
            throw new NotImplementedException();
        }
    }
}
