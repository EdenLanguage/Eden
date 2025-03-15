using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class ValidTokenStatement : Statement
    {
        public ValidTokenStatement(Token token) : base(token) { }

        public static Statement Create(Token token)
        {
            return new ValidTokenStatement(token);
        }

        public override string Print(int indents = 0)
        {
            return $"Valid statement";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"Valid statement";
        }

        public override string ToString()
        {
            return $"Valid statement";
        }
    }
}
