using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using System.Text;

namespace EdenClasslibrary.Errors.SemanticalErrors
{
    public class ErrorSemanticalCantEvaluateAST : SemanticalError
    {
        private AbstractSyntaxTreeNode _astNode;
        private ErrorSemanticalCantEvaluateAST(AbstractSyntaxTreeNode node)
        {
            _astNode = node;
        }

        public static AError Create(AbstractSyntaxTreeNode node)
        {
            return new ErrorSemanticalCantEvaluateAST(node);
        }

        public static IObject CreateErrorObject(AbstractSyntaxTreeNode node)
        {
            return ErrorObject.Create(Create(node));
        }

        public override string GetDetails()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Details:");
            sb.AppendLine("");
            sb.Append(_astNode.ToString());

            string str = sb.ToString();
            return str;
        }

        public override string GetMessage()
        {
            return "Evaluation could not start because semantical analizer detected error!";
        }
    }
}
