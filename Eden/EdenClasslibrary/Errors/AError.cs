using EdenClasslibrary.Types;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Errors
{
    public abstract class AError
    {
        public Token Token { get; }
        public virtual string ErrorType { get; }
        public string Line { get; }
     
        public AError(Token token, string line)
        {
            Token = token;
            Line = line;
        }

        public abstract string GetMessage();
        public string GetDetails()
        {
            return $"File: '{Token.Filename}', Line: '{Token.Line}', Column: '{Token.Start}'";
        }
        public string GetLineDetails()
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(Line))
            {
                return "";
            }

            int tabAmount = 0;
            foreach (char letter in Line)
            {
                if (letter == '\t')
                {
                    tabAmount++;
                }
            }

            string line = Line.Replace("\t", "").Replace("\r", "");

            string lineMark = string.Empty;
            int point = Token.Start - (tabAmount + 1);

            for (int i = 0; i < point; i++)
            {
                lineMark += " ";
            }
            lineMark += "^";

            while (lineMark.Length < line.Length)
            {
                lineMark += "-";
            }

            sb.AppendLine();
            sb.AppendLine(line);

            if(Common.ColorfulPrinting == true)
            {
                sb.Append($"{lineMark}".Pastel(Color.Red));
            }
            else
            {
                sb.Append($"{lineMark}");
            }
     
            string result = sb.ToString();

            return result;
        }
        public string PrintError()
        {
            StringBuilder sb = new StringBuilder();

            string message = GetMessage();
            string details = GetDetails();
            string lineDetails = GetLineDetails();

            if (Common.ColorfulPrinting == true)
            {
                sb.AppendLine($"[{ErrorType.Pastel(Color.Red)}]");
            }
            else
            {
                sb.AppendLine($"[{ErrorType}]");
            }

            sb.AppendLine($"{message}");
            sb.AppendLine($"{details}");
            sb.Append($"{lineDetails}");

            string result = sb.ToString();
            return result;
        }
    }
}
