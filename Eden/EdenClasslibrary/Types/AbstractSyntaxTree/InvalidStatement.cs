namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class InvalidStatement : Statement
    {
        public InvalidStatement(Token token) : base(token)
        {
        }

        public override string Print()
        {
            return "Parser encountered invalid statement!";
        }

        public override string ToAST(int indents = 0)
        {
            return "Parser encountered invalid statement!";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return "Parser encountered invalid statement!";
        }

        public override string ToString()
        {
            return "Parser encountered invalid statement!";
        }
    }
}
