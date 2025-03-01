using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class VariableTypeExpression : Expression
    {
        public string Type
        {
            get
            {
                return NodeToken.LiteralValue;
            }
        }
        public VariableTypeExpression(Token token) : base(token)
        {
        }
        public override string ToString()
        {
            return Type;
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tType: {Type.ToString()}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }

        public override string ParenthesesPrint()
        {
            return $"{Type}";
        }

        public override string ToAST(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Type: {Type}";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{"Type".Pastel(Color.Green)}: {Type}";
        }
    }
}
