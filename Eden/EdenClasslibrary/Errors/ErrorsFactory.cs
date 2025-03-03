using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors
{
    public enum ErrorType
    {
        InvalidSyntax,
        InvalidToken,
        InvalidStatement,
        InvalidVariableDeclaration,
        InvalidVariableDeclaration_MissingSemicolon,
        EvaluationError,
    }

    public static class ErrorsFactory
    {
        public static Error Create(ErrorType errorType, Token token)
        {
            string message = GetMessage(errorType, token);
            string tip = GetTip(errorType, token);
            return new Error(message, tip, token);
        }

        private static string GetMessage(ErrorType type, Token token)
        {
            string message = string.Empty;
            switch (type)
            {
                case ErrorType.InvalidSyntax:
                    message = "Invalid syntax error.";
                    break;
                case ErrorType.InvalidToken:
                    message = $"Invalid token! Token '{token.Keyword}' with value '{token.LiteralValue}' was not expected!";
                    break;
                case ErrorType.InvalidStatement:
                    message = $"Invalid statement! Token '{token.Keyword}' with value '{token.LiteralValue}' was not expected at this position in statement!";
                    break;
                case ErrorType.InvalidVariableDeclaration:
                    message = $"Could not parse variable declaration! Token '{token.Keyword}' with value '{token.LiteralValue}' was first token in statement that could not be parsed!";
                    break;
                case ErrorType.InvalidVariableDeclaration_MissingSemicolon:
                    message = $"Could not parse variable declaration! Token ';' was expected but encountered token is '{token.Keyword}' with value '{token.LiteralValue}'!";
                    break;
                case ErrorType.EvaluationError:
                    message = $"Couldn't evaluate statement! Token '{token.Keyword}' with value '{token.LiteralValue}' was first token in statement that could not be evaluated!";
                    break;
                default:
                    message = "Undefined error!";
                    break;
            }
            return message;
        }

        private static string GetTip(ErrorType type, Token token)
        {
            string tip = string.Empty;
            switch (type)
            {
                case ErrorType.InvalidSyntax:
                    tip = "Take a look at language whitepaper to find more details about syntax.";
                    break;
                case ErrorType.InvalidVariableDeclaration:
                    tip = $"To declare variable use syntax: <Var> <Type> <'='> <Expression>. Example: Var Int counter = 0;";
                    break;
                default:
                    tip = "";
                    break;
            }
            return tip;
        }
    }
}
