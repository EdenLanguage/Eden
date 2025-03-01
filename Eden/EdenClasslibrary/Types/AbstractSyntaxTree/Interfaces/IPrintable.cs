namespace EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces
{
    interface IPrintable
    {
        abstract string PrettyPrint(int indents = 0);
        abstract string ToASTFormat();
        abstract string PrettyPrintAST(int indent = 0);
    }
}
