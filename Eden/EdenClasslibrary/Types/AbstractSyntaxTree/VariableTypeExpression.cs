using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class VariableTypeExpression : Expression, IPrintable
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
            return PrettyPrint();
        }

        public override string ParenthesesPrint()
        {
            return $"{Type}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{"Type".Pastel(Color.Green)}: {Type}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Type}";
        }
    }
}
