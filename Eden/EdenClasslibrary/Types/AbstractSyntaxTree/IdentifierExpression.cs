using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IdentifierExpression : Expression, IPrintable
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
            return PrettyPrint();
        }

        public override string ParenthesesPrint()
        {
            return $"{Name}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{"Identifier".Pastel(Color.Green)}: {Name}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Name}";
        }
    }
}
