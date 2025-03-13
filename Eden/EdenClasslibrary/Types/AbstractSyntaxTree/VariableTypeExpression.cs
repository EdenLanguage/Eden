using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class VariableTypeExpression : VariableValueExpression, IPrintable
    {
        public VariableTypeExpression(Token token) : base(token, TypeTokenMapper.TypeFromToken(token)) { }
        public override string ToString()
        {
            return Type.Name;
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(VariableTypeExpression)} {{ {Type.Name } }}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Type.Name}";
        }
    }
}
