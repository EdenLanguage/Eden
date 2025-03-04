using EdenClasslibrary.Types;
using System.Text;

namespace EdenClasslibrary.Errors
{
    public class ErrorsManager
    {
        private List<AError> _errors;
        public AError[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }
        public ErrorsManager()
        {
            _errors = new List<AError>();
        }

        public string PrintErrors()
        {
            StringBuilder sb = new StringBuilder();

            foreach(AError error in Errors)
            {
                sb.AppendLine($"{error.PrintError()}");
            }

            string result = sb.ToString();
            return result;
        }

        public void AppendError(AError error)
        {
            _errors.Add(error);
        }
    }
}
