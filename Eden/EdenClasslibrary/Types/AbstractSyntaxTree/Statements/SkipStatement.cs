using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class SkipStatement : Statement
    {
        public SkipStatement(Token token) : base(token) { }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Skip;";
        }

        public string ToAbstractSyntaxTree()
        {
            throw new NotImplementedException();
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(SkipStatement)}";
        }

        public override string ToString()
        {
            return Print();
        }
    }
}