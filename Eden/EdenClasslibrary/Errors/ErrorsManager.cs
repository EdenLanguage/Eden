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

        /// <summary>
        /// Returns all of the errors and clears storage.
        /// </summary>
        /// <returns></returns>
        public AError[] PopErrors()
        {
            AError[] errors = Errors;
            _errors.Clear();
            return errors;
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

        public void AppendErrors(AError[] error)
        {
            _errors.AddRange(error);
        }
    }
}
