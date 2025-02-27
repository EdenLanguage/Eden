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
    }
}
