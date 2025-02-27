namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class UnaryExpression : Expression
    {
        public Expression Expression { get; set; }
        public UnaryExpression(Token token) : base(token)
        {
        }

        private string EvaluatePrint()
        {
            if(NodeToken.Keyword == TokenType.Minus)
            {
                return $"({NodeToken.LiteralValue}{Expression.ParenthesesPrint()})";
            }
            else
            {
                return $"{Expression.ParenthesesPrint()}";
            }
        }

        public override string ParenthesesPrint()
        {
            return EvaluatePrint();
        }

        public override string ToString()
        {
            return EvaluatePrint();
        }
    }
}
