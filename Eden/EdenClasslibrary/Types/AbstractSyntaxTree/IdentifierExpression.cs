using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
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

        public override string ToAST(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{nameof(IdentifierExpression)}: {Name}";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{"Identifier".Pastel(Color.Green)}: {Name}";
        }
    }
}
