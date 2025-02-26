using Pastel;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IdentifierExpression : Expression
    {
        public string Name
        {
            get
            {
                return NodeToken.LiteralValue;
            }
        }
        public IdentifierExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return Name;
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tName: {Name.ToString().Pastel(ConsoleColor.Blue)}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }

        public override string ParenthesesPrint()
        {
            return $"{Name}";
        }
    }
}
