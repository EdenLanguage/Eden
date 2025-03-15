using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class SisyphusStatement : Statement
    {
        public Statement Body { get; set; }

        public SisyphusStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indents)}Sisyphus {{");
            sb.AppendLine($"{Body.Print(indents + 1)}");
            sb.Append($"{Common.IndentCreator(indents)}}};");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(SisyphusStatement)} {{");
            sb.AppendLine($"{Body.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
