using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class QuitStatement : Statement
    {
        public QuitStatement(Token token) : base(token)
        {
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Quit;";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(QuitStatement)}";
        }

        public override string ToString()
        {
            return Print();
        }
    }
}
