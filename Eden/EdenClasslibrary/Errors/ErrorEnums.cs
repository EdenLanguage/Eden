 using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors
{
    public enum ParserErrorType
    {
        InvalidSyntax,
        InvalidToken,
        InvalidStatement,
        InvalidVariableDeclaration,
        InvalidVariableDeclaration_MissingSemicolon,
        EvaluationError,
    }
}
