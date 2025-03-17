using EdenClasslibrary.Types.AbstractSyntaxTree;

namespace EdenClasslibrary.Types
{
    public static class EvaluationUtility
    {
        public static AbstractSyntaxTreeNode CheckValidity<T>(AbstractSyntaxTreeNode instace) where T : AbstractSyntaxTreeNode
        {
            if((instace as T) is not null)
            {
                return instace as T;
            }
            return null;//  TODO: Handle error
        }
    }
}
