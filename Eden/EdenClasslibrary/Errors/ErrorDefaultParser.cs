using EdenClasslibrary.Types;

namespace EdenClasslibrary.Errors
{
    public class ErrorDefaultParser : AError
    {
        private ParserErrorType _parserErrorType;
        private Token _parserToken;

        public ErrorDefaultParser(ParserErrorType type, Token token)
        {
            _parserErrorType = type;
            _parserToken = token;
        }

        public static AError Create(ParserErrorType type, Token token)
        {
            return new ErrorDefaultParser(type, token);
        }

        public override string GetDetails()
        {
            return $"File: '{Path.GetFileName(_parserToken.Filename)}'. Line: '{_parserToken.Line}'. Column:'{_parserToken.Start}'.";
        }

        public override string GetMessage()
        {
            string message = string.Empty;
            switch (_parserErrorType)
            {
                case ParserErrorType.InvalidSyntax:
                    message = $"Invalid syntax error.";
                    break;
                case ParserErrorType.InvalidToken:
                    message = $"Invalid token! Token '{_parserToken.Keyword}' with value '{_parserToken.LiteralValue}' was not expected!";
                    break;
                case ParserErrorType.InvalidStatement:
                    message = $"Invalid statement! Token '{_parserToken.Keyword}' with value '{_parserToken.LiteralValue}' was not expected at this position in statement!";
                    break;
                case ParserErrorType.InvalidVariableDeclaration:
                    message = $"Could not parse variable declaration! Token '{_parserToken.Keyword}' with value '{_parserToken.LiteralValue}' was first token in statement that could not be parsed!";
                    break;
                case ParserErrorType.InvalidVariableDeclaration_MissingSemicolon:
                    message = $"Could not parse variable declaration! Token ';' was expected but encountered token is '{_parserToken.Keyword}' with value '{_parserToken.LiteralValue}'!";
                    break;
                case ParserErrorType.EvaluationError:
                    message = $"Couldn't evaluate statement! Token '{_parserToken.Keyword}' with value '{_parserToken.LiteralValue}' was first token in statement that could not be evaluated!";
                    break;
                default:
                    message = $"Undefined error!";
                    break;
            }
            return message;
        }

        public override string GetTip()
        {
            string tip = string.Empty;
            switch (_parserErrorType)
            {
                case ParserErrorType.InvalidSyntax:
                    tip = $"Take a look at language whitepaper to find more details about syntax.";
                    break;
                case ParserErrorType.InvalidVariableDeclaration:
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
