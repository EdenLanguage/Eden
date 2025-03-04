using EdenClasslibrary.Types.AbstractSyntaxTree;

namespace EdenClasslibrary.Types
{
    public static class EvaluationUtility
    {
        public static ASTreeNode CheckValidity<T>(ASTreeNode instace) where T : ASTreeNode
        {
            if((instace as T) is not null)
            {
                return instace as T;
            }
            return null;//  TODO: Handle error
        }
    }
}
