using System.Text;

namespace EdenClasslibrary.Errors
{
    public abstract class AError
    {
        public abstract string GetMessage();
        public virtual string GetTip()
        {
            return string.Empty;
        }
        public abstract string GetDetails();
        public string PrintError()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"> {GetMessage()}");
            sb.AppendLine($"-> {GetDetails()}");

            string isTip = GetTip();
            string tip = isTip == "" ? "" : "-> " + isTip;
            sb.AppendLine($"{tip}");

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
