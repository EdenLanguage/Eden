using EdenClasslibrary.Types;
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

            if(string.IsNullOrEmpty(Line))
            {
                return "";
            }

            string line = Line.Replace("\t","");

            int tabAmount = 0;
            foreach(char letter in Line)
            {
                if(letter == '\t')
                {
                    tabAmount++;
                }
            }

            string lineMark = string.Empty;
            int point = Token.Start - (tabAmount + 1);

            for(int i = 0; i < point; i++)
            {
                lineMark += " ";
            }
            lineMark += "^";

            while(lineMark.Length < Line.Length - 1)
            {
                lineMark += "-";
            }

            sb.AppendLine();
            sb.AppendLine(line);
#if DEBUG
            sb.Append($"{lineMark}");
#else
            sb.Append($"{lineMark}".Pastel(Color.Red));
#endif
            string result = sb.ToString();

            return result;
        }
        public string PrintError()
        {
            StringBuilder sb = new StringBuilder();

            string message = GetMessage();
            string details = GetDetails();
            string lineDetails = GetLineDetails();

#if DEBUG
            sb.AppendLine($"[{ErrorType}]");
#else
            sb.AppendLine($"[{ErrorType.Pastel(Color.Red)}]");
#endif

            sb.AppendLine($"{message}");
            sb.AppendLine($"{details}");
            sb.AppendLine($"{lineDetails}");

            string result = sb.ToString();
            return result;
        }
    }
}
