using EdenClasslibrary.Errors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.Enums;
using System.Text;

namespace EdenClasslibrary.Parser
{
    public class Parser
    {
        #region Fields
        private readonly Lexer _lexer;
        private FileStatement _program;
        private ErrorsManager _errorsManager;
        private readonly Dictionary<TokenType, Func<Expression>> _unaryMapping;
        private readonly Dictionary<TokenType, Func<Expression, Expression>> _binaryMapping;
        private readonly Dictionary<TokenType, Precedence> _precedenceMapping;
        #endregion

        #region Properties
        public Token NextToken { get; set; }
        public Token CurrentToken { get; set; }
        public FileStatement Program { get { return _program; } }
        public Error[] Errors
        {
            get
            {
                return _errorsManager.Errors;
            }
        }
        #endregion

        #region Constructor
        public Parser()
        {
            _lexer = new Lexer();
            _lexer.SetInput(string.Empty);
            _program = new FileStatement(Token.ProgramRootToken);
            _errorsManager = new ErrorsManager();

            #region Pratt Parser mappings
            /*  These are mappings required for Pratt's parsing method.
             *  '_unaryMapping' are mappings for functions that handle unary 
             *      tokens. Which means cases like '-1'. Mappings mean that 
             *      for current token type we use mapped function.
             *  '_binaryMapping' are mappings for operations like 5+5 and
             *      counter++. But increment operator was not yet implemented.
             *  '_precedenceMapping' is mapping for strength of current token.
             */
            _unaryMapping = new Dictionary<TokenType, Func<Expression>>();
            _binaryMapping = new Dictionary<TokenType, Func<Expression, Expression>>();
            _precedenceMapping = new Dictionary<TokenType, Precedence>();

            // TODO: Implement logical like &&, ||
            _precedenceMapping[TokenType.Bool] = Precedence.Logical;

            // == !=
            _precedenceMapping[TokenType.Inequal] = Precedence.Comparison;
            _precedenceMapping[TokenType.Equal] = Precedence.Comparison;

            // + -
            _precedenceMapping[TokenType.Plus] = Precedence.Sum;
            _precedenceMapping[TokenType.Minus] = Precedence.Sum;

            // * / %
            _precedenceMapping[TokenType.Star] = Precedence.Product;
            _precedenceMapping[TokenType.Slash] = Precedence.Product;

            _precedenceMapping[TokenType.LeftParenthesis] = Precedence.Call;

            RegisterUnaryMapping(TokenType.If, ParseIfExpression);
            RegisterUnaryMapping(TokenType.Identifier, ParseIdentifierExpression);
            RegisterUnaryMapping(TokenType.Int, ParseIntExpression);
            RegisterUnaryMapping(TokenType.Float, ParseFloatExpression);
            RegisterUnaryMapping(TokenType.String, ParseStringExpression);
            RegisterUnaryMapping(TokenType.Bool, ParseBoolExpression);
            RegisterUnaryMapping(TokenType.Minus, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Plus, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.LeftParenthesis, ParseGroupedExpression);
            RegisterUnaryMapping(TokenType.ExclemationMark, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Function, ParseFunctionExpression);

            RegisterBinaryMapping(TokenType.Plus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Minus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Star, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Slash, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Inequal, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Equal, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.LeftParenthesis, ParseCallExpression);
            #endregion
        }
        #endregion

        #region Public
        /// <summary>
        /// Parse input data token by token.
        /// </summary>
        /// <param name="code">Source code</param>
        /// <returns>Abstract syntax tree of input data</returns>
        public FileStatement Parse(string code)
        {
            _lexer.SetInput(code);
            _program = ParseFileStatement();
            return _program;
        }

        public FileStatement ParseFile(string path)
        {
            //string sourceCode = GetSource(path);
            _lexer.LoadFile(path);
            _program = ParseFileStatement();
            return _program;
        }
        #endregion

        #region Helper methods
        private string GetSource(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return "";
            }
        }

        private Precedence CurrentTokenEvaluationOrder()
        {
            if (_precedenceMapping.ContainsKey(CurrentToken.Keyword))
            {
                return _precedenceMapping[CurrentToken.Keyword];
            }
            else return Precedence.Lowest;
        }

        private Precedence NextTokenEvaluationOrder()
        {
            if (_precedenceMapping.ContainsKey(NextToken.Keyword))
            {
                return _precedenceMapping[NextToken.Keyword];
            }
            else return Precedence.Lowest;
        }
        private void EatStatement()
        {
            // Go throught tokens till you find ';' and then eat it as well.
            while (CurrentToken.Keyword != TokenType.Semicolon && NextToken.Keyword != TokenType.Eof)
            {
                LoadNextToken();
            }
            LoadNextToken();
        }
        private void LoadNextToken()
        {
            if (CurrentToken == null)
            {
                // Lexer was not run yet so load 2 tokens
                CurrentToken = _lexer.NextToken();
                NextToken = _lexer.NextToken();
            }
            else
            {
                // It was so just go oe position ahead.
                CurrentToken = NextToken;
                NextToken = _lexer.NextToken();
            }
        }

        public string PrintErrors()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Error error in Errors)
            {
                sb.Append(error);
            }

            string result = sb.ToString();
            return result;
        }
        #endregion

        #region Expressions parsing methods
        /// <summary>
        /// Pratt parsing method
        /// </summary>
        /// <param name="precedence"></param>
        /// <returns>Parsed Expression</returns>
        private Expression ParseExpression(Precedence precedence)
        {
            /*  Pratt Parser - how the fuck does it work.
             *  Every first call 'order' argument is the lowest possible value.
             *  If we call this for the first time we know that the first token has to be parsed via Prefix func. (Because it is first).
             *  If we evaluate it successfully. We can go throught all of the tokens that proceed next token, only if their precendence is lower then ours
             */

            Func<Expression> unaryFunc = GetUnaryFunc(CurrentToken.Keyword);

            Expression leftNodeExpression = unaryFunc();
            Precedence leftNodePrecedence = CurrentTokenEvaluationOrder();

            while (NextToken.Keyword != TokenType.Semicolon && precedence < NextTokenEvaluationOrder())
            {
                Func<Expression, Expression> binaryFunc = GetBinaryFunc(NextToken.Keyword);

                LoadNextToken();

                leftNodeExpression = binaryFunc(leftNodeExpression);
            }

            return leftNodeExpression;
        }

        private Expression ParseUnaryExpression()
        {
            Token token = CurrentToken;
            Precedence currentEvalOrder = CurrentTokenEvaluationOrder();

            LoadNextToken();

            Expression exp = ParseExpression(currentEvalOrder);

            UnaryExpression unaryExpression = new UnaryExpression(token)
            {
                Expression = exp,
            };

            return unaryExpression;
        }

        private Expression ParseGroupedExpression()
        {
            Token token = CurrentToken;
            Precedence currentEvalOrder = CurrentTokenEvaluationOrder();

            LoadNextToken();
            Expression exp = ParseExpression(Precedence.Lowest);


            // This one 'eats' last ')' to close expression.
            LoadNextToken();

            return exp;
        }

        private Expression ParseBinaryExpression(Expression left)
        {
            Token token = CurrentToken;
            Precedence currentEvalOrder = CurrentTokenEvaluationOrder();

            LoadNextToken();

            Expression right = ParseExpression(currentEvalOrder);

            BinaryExpression binaryExpression = new BinaryExpression(token)
            {
                Left = left,
                Right = right,
            };

            return binaryExpression;
        }

        private Expression ParseCallExpression(Expression function)
        {
            if (!CurrentToken.IsType(TokenType.LeftParenthesis))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }
            CallExpression callExp = new CallExpression(CurrentToken);
            callExp.Function = function;
            LoadNextToken();

            while (!CurrentToken.IsType(TokenType.RightParenthesis) && !CurrentToken.IsType(TokenType.Eof))
            {
                Expression argumentExp = ParseExpression(Precedence.Lowest);
                callExp.AddArgumentExpression(argumentExp);

                //  Eat parsed token and ',' token
                LoadNextToken();

                if (!CurrentToken.IsType(TokenType.RightParenthesis))
                {
                    LoadNextToken();
                }
            }

            return callExp;
        }

        private Expression ParseIdentifierExpression()
        {
            IdentifierExpression id = new IdentifierExpression(CurrentToken);
            return id;
        }

        private Expression ParseFunctionExpression()
        {
            FunctionExpression functionExpression = new FunctionExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.VariableType))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            functionExpression.Type = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.Identifier))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            functionExpression.Name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.LeftParenthesis))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }
            LoadNextToken();

            while (!CurrentToken.IsType(TokenType.RightParenthesis))
            {
                Expression funcArgument = ParseVariableDefinitionExpression();
                functionExpression.AddFuncArgument(funcArgument);

                if (CurrentToken.IsType(TokenType.Comma) && !NextToken.IsType(TokenType.RightParenthesis))
                {
                    LoadNextToken();
                }
            }
            //  Eat ')'
            LoadNextToken();

            functionExpression.Body = ParseBlockStatement();
            LoadNextToken();

            return functionExpression;
        }

        private Expression ParseVariableDefinitionExpression()
        {
            VariableDefinitionExpression variableDefinition = new VariableDefinitionExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.VariableType))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            variableDefinition.Type = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.Identifier))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            variableDefinition.Name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            return variableDefinition;
        }

        private Expression ParseFloatExpression()
        {
            FloatExpression floatExp = new FloatExpression(CurrentToken);
            return floatExp;
        }

        private Expression ParseStringExpression()
        {
            StringExpression stringExp = new StringExpression(CurrentToken);
            return stringExp;
        }

        private Expression ParseBoolExpression()
        {
            BoolExpresion boolExp = new BoolExpresion(CurrentToken);
            return boolExp;
        }

        private Expression ParseIntExpression()
        {
            IntExpression intExp = new IntExpression(CurrentToken);
            return intExp;
        }

        private Expression ParseInvalidExpression(TokenType stopToken, ErrorType errorType)
        {
            InvalidExpression invExp = new InvalidExpression(CurrentToken);

            _errorsManager.AppendError(errorType, CurrentToken);

            while (!CurrentToken.IsType(stopToken))
            {
                LoadNextToken();
            }

            return invExp;
        }
        #endregion

        #region Statements parsing methods
        private Statement ParseStatement()
        {
            Statement statement = null;
            switch (CurrentToken.Keyword)
            {
                case TokenType.VariableType:
                    statement = ParseInvalidStatement(ErrorType.InvalidStatement, CurrentToken);
                    break;
                case TokenType.Keyword:
                    switch (CurrentToken.LiteralValue)
                    {
                        case "Return":
                            statement = ParseReturnStatement();
                            break;
                        case "Var":
                            statement = ParseVariableStatement();
                            break;
                        default:
                            statement = ParseExpressionStatement();
                            break;
                    }
                    break;
                default:
                    statement = ParseExpressionStatement();
                    ////if (CurrentToken.Keyword != TokenType.Semicolon && CurrentToken.Keyword != TokenType.Eof)
                    //if (CurrentToken.Keyword != TokenType.Semicolon && CurrentToken.Keyword != TokenType.Eof)
                    //{
                    //    _errors.Add($"There was en error while parsing expression from line:'{statement.NodeToken.Line}' and column:'{statement.NodeToken.TokenStartingLinePosition}'");
                    //    statement = new InvalidStatement(statement.NodeToken);
                    //    EatStatement();
                    //}
                    break;
            }
            return statement;
        }

        private Statement ParseInvalidStatement(ErrorType errorType, Token invalidToken)
        {
            Statement statement = new InvalidStatement(invalidToken);

            _errorsManager.AppendError(errorType, invalidToken);
            EatStatement();

            return statement;
        }

        private Statement ParseExpressionStatement()
        {
            ExpressionStatement statement = new ExpressionStatement(CurrentToken);
            statement.Expression = ParseExpression(Precedence.Lowest);

            if (NextToken.Keyword == TokenType.Semicolon)
            {
                //  Eat last token of expression
                LoadNextToken();
            }

            if (!CurrentToken.IsType(TokenType.Semicolon))
            {
                return ParseInvalidStatement(ErrorType.InvalidToken, CurrentToken);
            }

            //  Eat ';'
            LoadNextToken();

            return statement;
        }

        private Statement ParseReturnStatement()
        {
            ReturnStatement returnStatement = new ReturnStatement(CurrentToken);

            if (CurrentToken.Keyword != TokenType.Keyword && CurrentToken.LiteralValue != "return")
            {
                return ParseInvalidStatement(ErrorType.InvalidToken, CurrentToken);
            }
            LoadNextToken();

            if (!CurrentToken.CanEvaluateExpression())
            {
                return ParseInvalidStatement(ErrorType.EvaluationError, CurrentToken);
            }

            Expression expression = ParseExpression(Precedence.Lowest);
            LoadNextToken();

            if (!CurrentToken.IsSemicolon())
            {
                return ParseInvalidStatement(ErrorType.InvalidToken, CurrentToken);
            }
            LoadNextToken();

            returnStatement.Expression = expression;

            return returnStatement;
        }

        private Expression ParseIfExpression()
        {
            IfExpression ifExpression = new IfExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.LeftParenthesis))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            //  Eat '('
            LoadNextToken();
            ifExpression.ConditionExpression = ParseExpression(Precedence.Lowest);

            //  Eat ')'
            LoadNextToken();
            if (!CurrentToken.IsType(TokenType.RightParenthesis))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            LoadNextToken();
            if (!CurrentToken.IsType(TokenType.LeftBracket))
            {
                return ParseInvalidExpression(TokenType.Semicolon, ErrorType.InvalidToken);
            }

            ifExpression.FulfielldBlock = ParseBlockStatement();
            LoadNextToken();

            if (CurrentToken.IsType(TokenType.Else))
            {
                //  Eat 'Else' token
                LoadNextToken();

                ifExpression.AlternativeBlock = ParseBlockStatement();

                //  Eat '}'
                LoadNextToken();
            }

            return ifExpression;
        }

        private BlockStatement ParseBlockStatement()
        {
            BlockStatement block = new BlockStatement(CurrentToken);

            LoadNextToken();
            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseStatement();
                block.AddStatement(statement);
            }

            return block;
        }

        private FileStatement ParseFileStatement()
        {
            FileStatement file = new FileStatement(CurrentToken);
            BlockStatement programBlock = new BlockStatement(CurrentToken);
            file.Block = programBlock;

            LoadNextToken();
            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseStatement();
                programBlock.AddStatement(statement);
            }

            return file;
        }

        private Statement ParseVariableStatement()
        {
            // Example: 'var int variable = 10'
            VariableDeclarationStatement variableStatement = new VariableDeclarationStatement(CurrentToken);

            if (CurrentToken.Keyword != TokenType.Keyword)
            {
                return ParseInvalidStatement(ErrorType.InvalidVariableDeclaration, CurrentToken);
            }

            VariableTypeExpression variableTypeExp = null;

            // Check whether it is a type
            if (NextToken.IsVariableType())
            {
                LoadNextToken();
                variableTypeExp = new VariableTypeExpression(CurrentToken);
            }
            else
            {
                return ParseInvalidStatement(ErrorType.InvalidVariableDeclaration, NextToken);
            }

            LoadNextToken();
            IdentifierExpression identifierExp = new IdentifierExpression(CurrentToken);

            if (NextToken.IsAssignType())
            {
                // Consume '='
                LoadNextToken();
            }
            else
            {
                return ParseInvalidStatement(ErrorType.InvalidVariableDeclaration, NextToken);
            }

            LoadNextToken();

            Expression expression = ParseExpression(Precedence.Lowest);
            LoadNextToken();

            //  If next token is not Semicolon then expression was not parsed properly...
            if (!CurrentToken.IsSemicolon())
            {
                return ParseInvalidStatement(ErrorType.InvalidVariableDeclaration_MissingSemicolon, CurrentToken);
            }

            LoadNextToken();

            variableStatement.Type = variableTypeExp;
            variableStatement.Identifier = identifierExp;
            variableStatement.Expression = expression;

            return variableStatement;
        }
        #endregion

        #region Pratt parser helper methods
        private void RegisterUnaryMapping(TokenType tokenType, Func<Expression> func)
        {
            _unaryMapping[tokenType] = func;
        }

        private void RegisterBinaryMapping(TokenType tokenType, Func<Expression, Expression> func)
        {
            _binaryMapping[tokenType] = func;
        }

        private Func<Expression, Expression> GetBinaryFunc(TokenType type)
        {
            bool isDefined = _binaryMapping.ContainsKey(type);
            if (isDefined == true)
            {
                return _binaryMapping[type];
            }
            else
            {
                throw new Exception($"Binary handling function for '{type}' TokenType is not present in mapping!");
            }
        }

        private Func<Expression> GetUnaryFunc(TokenType type)
        {
            bool isDefined = _unaryMapping.ContainsKey(type);
            if (isDefined == true)
            {
                return _unaryMapping[type];
            }
            else
            {
                throw new Exception($"Unary handling function for '{type}' TokenType is not present in mapping!");
            }
        }
        #endregion
    }
}
