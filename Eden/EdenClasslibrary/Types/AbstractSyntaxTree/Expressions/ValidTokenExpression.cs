namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class ValidTokenExpression : Expression
    {
        public ValidTokenExpression(Token token) : base(token) { }
        public static Expression Create(Token token)
        {
            return new ValidTokenExpression(token);
        }

        public override string Print(int indents = 0)
        {
            return "Valid expression!";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return "Valid expression!";
        }

        public override string ToString()
        {
            return "Valid expression!";
        }
    }
}
