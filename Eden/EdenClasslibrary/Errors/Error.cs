using EdenClasslibrary.Types;
using System.Text;

namespace EdenClasslibrary.Errors
{
    public class Error
    {
        private string _errorMessage;
        private string _errorTip;
        private Token _token;
        public Error(string message, string tip, Token token)
        {
            _errorMessage = message;
            _errorTip = tip;
            _token = token;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (HasErrorMessage())
            {
                sb.AppendLine($"> {_errorMessage}");
            }

            string filename = (_token.Filename == string.Empty) ? "REPL" : Path.GetFileName(_token.Filename);
            sb.AppendLine($"> Error at: '{filename}'");
            sb.AppendLine($"> With token: '{_token.LiteralValue}', Line: '{_token.Line}', Column: '{_token.TokenStartingLinePosition}'");

            if (HasErrorTip())
            {
                sb.AppendLine($"> {_errorTip}");
            }

            sb.AppendLine();

            string result = sb.ToString();
            return result;
        }

        private bool HasErrorTip()
        {
            return _errorTip != null && _errorTip != string.Empty;
        }
        
        private bool HasErrorMessage()
        {
            return _errorMessage != null && _errorMessage != string.Empty;
        }
    }
}
