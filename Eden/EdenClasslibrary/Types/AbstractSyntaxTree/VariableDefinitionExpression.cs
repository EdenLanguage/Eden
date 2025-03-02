using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class VariableDefinitionExpression : Expression, IPrintable
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Name { get; set; }
        public VariableDefinitionExpression(Token token) : base(token) { }

        public override string ParenthesesPrint()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Var {Type.PrettyPrint()} {Name.PrettyPrint()}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
