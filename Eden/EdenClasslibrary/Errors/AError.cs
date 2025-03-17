using System.Text;

namespace EdenClasslibrary.Errors
{
    public abstract class AError
    {
        public virtual string ErrorType { get; }
        public abstract string GetMessage();
        public virtual string GetTip()
        {
            return string.Empty;
        }
        public abstract string GetDetails();
        public string PrintError()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"-> [TYPE]");
            sb.AppendLine($"-> {GetMessage()}");

            string isDetails = GetDetails();
            string details = isDetails == "" ? "" : "-> " + isDetails;
            if(details != "")
            {
                sb.AppendLine($"{details}");
            }

            string isTip = GetTip();
            string tip = isTip == "" ? "" : "-> " + isTip;
            if(tip != "")
            {
                sb.AppendLine($"{tip}");
            }

            string result = sb.ToString();
            return result;
        }

        public bool HasTip()
        {
            return GetTip() != string.Empty;
        }

        public bool HasErrorMessage()
        {
            return (GetMessage() != null) && (GetMessage() != string.Empty);
        }
    }
}
