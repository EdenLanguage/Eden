using EdenClasslibrary.Types;
using System.Text;

namespace EdenClasslibrary.Errors
{
    public class ErrorsManager
    {
        private List<Error> _errors;
        public Error[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }
        public ErrorsManager()
        {
            _errors = new List<Error>();
        }

        public string PrintErrors()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Error error in Errors)
            {
                sb.AppendLine($"{error}");
            }

            string result = sb.ToString();
            return result;
        }

        public void AppendError(ErrorType errorType, Token token)
        {
            Error newError = ErrorsFactory.Create(errorType, token);
            _errors.Add(newError);
        }
    }
}
