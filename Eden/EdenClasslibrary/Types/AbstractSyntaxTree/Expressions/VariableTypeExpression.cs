using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class VariableTypeExpression : VariableValueExpression
    {
        public VariableTypeExpression(Token token) : base(token, TypeTokenMapper.TypeFromToken(token)) { }
        public override string ToString()
        {
            return Type.Name;
        }

        public string ToAbstractSyntaxTree()
        {
            return ToAbstractSyntaxTree();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Type.Name}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(VariableTypeExpression)} {{ {Type.Name } }}";
        }
    }
}
