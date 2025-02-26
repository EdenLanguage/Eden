using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.Enums;
using System.Diagnostics;

namespace EdenClasslibrary.Parser
{
    public class Parser
    {
        #region Fields
        private readonly Lexer _lexer;
        private BlockStatement _ast;
        private List<string> _errors;
        private readonly Dictionary<TokenType, Func<Expression>> _unaryMapping;
        private readonly Dictionary<TokenType, Func<Expression, Expression>> _binaryMapping;
        private readonly Dictionary<TokenType, Precedence> _precedenceMapping;
        #endregion

        #region Properties
        public Token NextToken { get; set; }
        public Token CurrentToken { get; set; }
        public BlockStatement AbstractSyntaxTree { get { return _ast; } }
        public string[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }
        #endregion
        
        #region Constructor
        public Parser()
        {
            _lexer = new Lexer();
            _lexer.SetInput(string.Empty);
            _ast = new BlockStatement(Token.RootToken);
            _errors = new List<string>();

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

            _precedenceMapping[TokenType.Equal] = Precedence.Equals;
            _precedenceMapping[TokenType.Equal] = Precedence.Equals;
            _precedenceMapping[TokenType.Plus] = Precedence.Sum;
            _precedenceMapping[TokenType.Minus] = Precedence.Sum;
            _precedenceMapping[TokenType.Bool] = Precedence.Product;    //TODO: Find out what sould be precendese for logical type!
            _precedenceMapping[TokenType.Star] = Precedence.Product;
            _precedenceMapping[TokenType.Slash] = Precedence.Product;

            RegisterUnaryMapping(TokenType.Identifier, ParseIdentifierExpression);
            RegisterUnaryMapping(TokenType.Int, ParseIntExpression);
            RegisterUnaryMapping(TokenType.Float, ParseFloatExpression);
            RegisterUnaryMapping(TokenType.String, ParseStringExpression);
            RegisterUnaryMapping(TokenType.Bool, ParseBoolExpression);
            RegisterUnaryMapping(TokenType.Minus, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Plus, ParseUnaryExpression);

            RegisterBinaryMapping(TokenType.Plus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Minus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Star, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Slash, ParseBinaryExpression);
            #endregion
        }
        #endregion

        #region Public
        /// <summary>
        /// Parse input data token by token.
        /// </summary>
        /// <param name="code">Source code</param>
        /// <returns>Abstract syntax tree of input data</returns>
        public BlockStatement Parse(string code)
        {
            _lexer.SetInput(code);

            _ast = new BlockStatement(Token.RootToken);

            LoadNextToken();

            while (CurrentToken.Keyword != TokenType.Eof && CurrentToken.Keyword != TokenType.Illegal)
            {
                Statement statement = null;
                switch (CurrentToken.Keyword)
                {
                    case TokenType.VariableType:
                        statement = ParseInvalidStatement();
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
                                statement = ParseKeywordStatement();
                                break;
                        }
                        break;
                    default:
                        statement = ParseExpressionStatement();
                        if(CurrentToken.Keyword != TokenType.Semicolon && CurrentToken.Keyword != TokenType.Eof)
                        {
                            _errors.Add($"There was en error while parsing expression from line:'{statement.NodeToken.Line}' and column:'{statement.NodeToken.TokenStartingLinePosition}'");
                            statement = new InvalidStatement(statement.NodeToken);
                            EatStatement();
                        }
                        break;
                }

                if (statement != null)
                {
                    _ast.AddStatement(statement);
                }
            }

            return _ast;
        }
        #endregion

        #region Helper methods
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

        private Expression ParseIdentifierExpression()
        {
            IdentifierExpression id = new IdentifierExpression(CurrentToken);
            return id;
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
        #endregion

        #region Statements parsing methods
        private VariableDeclarationStatement ParseKeywordStatement()
        {
            return null;
        }

        private ExpressionStatement ParseExpressionStatement()
        {
            ExpressionStatement statement = new ExpressionStatement(CurrentToken);
            statement.Expression = ParseExpression(Precedence.Lowest);

            if (NextToken.Keyword == TokenType.Semicolon)
            {
                LoadNextToken();
            }

            LoadNextToken();

            return statement;
        }

        private InvalidStatement ParseInvalidStatement()
        {
            InvalidStatement statement = new InvalidStatement(CurrentToken);

            while (CurrentToken.Keyword != TokenType.Eof)
            {
                LoadNextToken();
            }

            return statement;
        }

        private Statement ParseReturnStatement()
        {
            ReturnStatement returnStatement = new ReturnStatement(CurrentToken);

            if (CurrentToken.Keyword != TokenType.Keyword && CurrentToken.LiteralValue != "return")
            {
                _errors.Add($"return token expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }
            LoadNextToken();

            if (!CurrentToken.CanEvaluateExpression())
            {
                _errors.Add($"There was an error while parsing expression. Cannot evaluate expression with '{CurrentToken.Keyword}' token, at line '{CurrentToken.Line}', column '{CurrentToken.TokenStartingLinePosition}'");
                EatStatement();
                return new InvalidStatement(returnStatement.NodeToken);
            }

            Expression expression = ParseExpression(Precedence.Lowest);
            LoadNextToken();

            if (!CurrentToken.IsSemicolon())
            {
                _errors.Add($"There was an erro while parsing expression. Expected '{TokenType.Semicolon}', actual '{CurrentToken.Keyword}', at line '{CurrentToken.Line}', column '{CurrentToken.TokenStartingLinePosition}'");
                EatStatement();
                return new InvalidStatement(returnStatement.NodeToken);
            }
            LoadNextToken();

            returnStatement.Expression = expression;

            return returnStatement;
        }

        private Statement ParseVariableStatement()
        {
            // Example: 'var int variable = 10'
            VariableDeclarationStatement variableStatement = new VariableDeclarationStatement(CurrentToken);

            if (CurrentToken.Keyword != TokenType.Keyword)
            {
                _errors.Add($"var keyword expected but got '{CurrentToken.Keyword}'");
                EatStatement();
                return new InvalidStatement(CurrentToken);
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
                _errors.Add($"Expected variable type token but got '{CurrentToken.Keyword}'");
                EatStatement();
                return new InvalidStatement(CurrentToken);
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
                _errors.Add($"Expected assignment token but got '{CurrentToken}'");
                EatStatement();
                return new InvalidStatement(CurrentToken);
            }

            LoadNextToken();

            Expression expression = ParseExpression(Precedence.Lowest);
            LoadNextToken();

            //  If next token is not Semicolon then expression was not parsed properly...
            if (!CurrentToken.IsSemicolon())
            {
                _errors.Add($"There was an error while parsing expression. Expected '{TokenType.Semicolon}', actual '{CurrentToken.Keyword}' at line '{CurrentToken.Line}' column '{CurrentToken.TokenStartingLinePosition}'");
                EatStatement();
                return new InvalidStatement(variableStatement.NodeToken);
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
            if(isDefined == true)
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
