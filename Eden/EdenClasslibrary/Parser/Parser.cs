using EdenClasslibrary.Parser.AST;
using EdenClasslibrary.Types;

namespace EdenClasslibrary.Parser
{
    public enum EvaluationOrder
    {
        Lowest,
        Equals,
        LesserGreater,
        Sum,
        Product,
        Prefix,
        Call,
    }

    public class Parser
    {
        public Lexer lexer;
        public Token CurrentToken;
        public Token NextToken;
        public BlockStatement _ast;
        private List<string> _errors;


        public BlockStatement AST { get { return _ast; } }
        public Dictionary<TokenType, Func<Expression>> PrefixParseFunctionsMappings;
        public Dictionary<TokenType, Func<Expression, Expression>> InfixParseFunctionsMappings;
        public Dictionary<TokenType, EvaluationOrder> Precedence;

        public string[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }
        public Parser()
        {
            lexer = new Lexer();
            // This one needs to be changed to root of AST tree node type.
            _ast = new BlockStatement();
            _errors = new List<string>();

            PrefixParseFunctionsMappings = new Dictionary<TokenType, Func<Expression>>();
            InfixParseFunctionsMappings = new Dictionary<TokenType, Func<Expression, Expression>>();
            Precedence = new Dictionary<TokenType, EvaluationOrder>();

            Precedence[TokenType.Equal] = EvaluationOrder.Equals;
            Precedence[TokenType.Equal] = EvaluationOrder.Equals;
            Precedence[TokenType.Plus] = EvaluationOrder.Sum;
            Precedence[TokenType.Minus] = EvaluationOrder.Sum;
            Precedence[TokenType.Star] = EvaluationOrder.Product;

            RegisterPrefix(TokenType.Indentifier, ParseIdentifier);
            RegisterPrefix(TokenType.Number, ParseInt);

            RegisterInfix(TokenType.Plus, ParseBinaryExpression);
            RegisterInfix(TokenType.Star, ParseBinaryExpression);
        }

        public EvaluationOrder CurrentTokenEvaluationOrder()
        {
            if (Precedence.ContainsKey(CurrentToken.Keyword))
            {
                return Precedence[CurrentToken.Keyword];
            }
            else return EvaluationOrder.Lowest;
        }

        public EvaluationOrder NextTokenEvaluationOrder()
        {
            if (Precedence.ContainsKey(NextToken.Keyword))
            {
                return Precedence[NextToken.Keyword];
            }
            else return EvaluationOrder.Lowest;
        }

        public Expression ParseBinaryExpression(Expression left)
        {
            Token token = CurrentToken;
            EvaluationOrder currentEvalOrder = CurrentTokenEvaluationOrder();

            LoadNextToken();

            Expression right = ParseExpression(currentEvalOrder);

            BinaryExpression binaryExpression = new BinaryExpression()
            {
                Left = left,
                Token = token,
                Right = right,
            };

            return binaryExpression;
        }

        public Expression ParseIdentifier()
        {
            Identifier id = new Identifier();
            id.Token = CurrentToken;
            id.Value = CurrentToken.Value;
            return id;
        }

        public Expression ParseInt()
        {
            IntLiteral id = new IntLiteral();
            id.Token = CurrentToken;

            int parsed = 0;
            bool couldParse = int.TryParse(CurrentToken.Value, out parsed);

            id.Value = parsed;
            return id;
        }

        public void RegisterPrefix(TokenType tokenType, Func<Expression> func)
        {
            PrefixParseFunctionsMappings[tokenType] = func;
        }

        public void RegisterInfix(TokenType tokenType, Func<Expression, Expression> func)
        {
            InfixParseFunctionsMappings[tokenType] = func;
        }

        public BlockStatement Parse(string code)
        {
            lexer = new Lexer();
            lexer.Input = code;

            _ast = new BlockStatement();

            LoadNextToken();

            while(CurrentToken.Keyword != TokenType.Eof && CurrentToken.Keyword != TokenType.Illegal)
            {
                Statement statement = null;
                // Now check what is the token and execute branch. For example 'VariableStatement' etc...
                switch (CurrentToken.Keyword)
                {
                    case TokenType.Var:
                    case TokenType.VarType:
                        statement = ParseVariableStatement();
                        break;
                    case TokenType.Keyword:
                        switch (CurrentToken.Value)
                        {
                            case "return":
                                statement = ParseReturnStatement();
                                break;
                            default:
                                statement = ParseKeywordStatement();
                                break;
                        }
                        break;
                    default:
                        statement = ParseExpressionStatement();
                        break;
                }

                if(statement != null)
                {
                    _ast.AddStatement(statement);
                }
            }

            return _ast;
        }

        public void EatStatement()
        {
            // Go throught tokens till you find ';' and then eat it as well.
            while(CurrentToken.Keyword != TokenType.Semicolon && NextToken.Keyword != TokenType.Eof)
            {
                LoadNextToken();
            }
            LoadNextToken();
        }

        public VariableStatement ParseKeywordStatement()
        {
            return null;
        }

        public Expression ParseExpression(EvaluationOrder order)
        {
            /*  Pratt Parser - how the fuck does it work.
             *  Every first call 'order' argument is the lowest possible value.
             *  If we call this for the first time we know that the first token has to be parsed via Prefix func. (Because it is first).
             *  If we evaluate it successfully. We can go throught all of the tokens that proceed next token, only if their precendence is lower then ours
             */

            Func<Expression> func = PrefixParseFunctionsMappings[CurrentToken.Keyword];
            if(func == null)
            {
                return null;
            }

            Expression leftNodeExpression = func();
            EvaluationOrder leftNodeEvalOrder = CurrentTokenEvaluationOrder();

            while(NextToken.Keyword != TokenType.Semicolon && order < NextTokenEvaluationOrder())
            {
                Func<Expression, Expression> binaryFunc = InfixParseFunctionsMappings[NextToken.Keyword];

                LoadNextToken();

                if(binaryFunc == null)
                {
                    throw new Exception("Func should be defined!");
                }

                leftNodeExpression = binaryFunc(leftNodeExpression);
            }

            return leftNodeExpression;
        }

        public ExpressionStatement ParseExpressionStatement()
        {
            ExpressionStatement statement = new ExpressionStatement();
            statement.Expression = ParseExpression(EvaluationOrder.Lowest);

            if(NextToken.Keyword == TokenType.Semicolon)
            {
                LoadNextToken();
            }

            LoadNextToken();

            return statement;
        }

        public ReturnStatement ParseReturnStatement()
        {
            ReturnStatement returnStatement = new ReturnStatement();

            if(CurrentToken.Keyword != TokenType.Keyword && CurrentToken.Value != "return")
            {
                _errors.Add($"return token expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            returnStatement.NodeToken = CurrentToken;

            LoadNextToken();

            int counter = 0;
            while(CurrentToken.Keyword != TokenType.Semicolon)
            {
                LoadNextToken();
                counter++;
            }
            // This needs to be implemented.
            returnStatement.ReturnExpression = null;

            if(counter == 0)
            {
                // No expresion
                _errors.Add($"Expression tokens expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            LoadNextToken();

            return returnStatement;
        }

        public VariableStatement ParseVariableStatement()
        {
            // Example: 'var int variable = 10'
            VariableStatement variableStatement = new VariableStatement();
            
            if(CurrentToken.Keyword != TokenType.Var)
            {
                _errors.Add($"var keyword expected but got '{CurrentToken.Keyword}'");
                EatStatement();
                return null;
            }

            Token variableToken = CurrentToken;
            Token variableTypeToken = null;

            // Check whether it is a type
            if (NextToken.IsVariableType())
            {
                LoadNextToken();
                variableTypeToken = CurrentToken;
            }
            else
            {
                _errors.Add($"Expected variable type token but got '{CurrentToken.Keyword}'");
                EatStatement();
                return null;
            }
            
            LoadNextToken();
            Token variabeNameToken = CurrentToken;

            Token nodeToken = null;
            if (NextToken.IsAssignType())
            {
                // Consume '='
                LoadNextToken();
                nodeToken = CurrentToken;
            }
            else
            {
                _errors.Add($"Expected assignment token but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            LoadNextToken();

            // Calculate expression
            Expression expression = null;

            // Eat everyting till ';' is encountered. Later on there should be expressnio evaluation.
            int counter = 0;
            while(CurrentToken.Keyword != TokenType.Semicolon)
            {
                LoadNextToken();
                counter++;  
            }

            if(counter == 0)
            {
                // No expression tokens encountered.
                _errors.Add($"Expression token expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            Identifier nameIdentifier = new Identifier();

            variableStatement.Token = variabeNameToken;
            variableStatement.Type = variableTypeToken;
            variableStatement.Name = nameIdentifier;
            variableStatement.Value = expression;

            LoadNextToken();

            return variableStatement;
        }

        public void LoadNextToken()
        {
            if(CurrentToken == null)
            {
                // Lexer was not run yet so load 2 tokens
                CurrentToken = lexer.NextToken();
                NextToken = lexer.NextToken();
            }
            else
            {
                // It was so just go oe position ahead.
                CurrentToken = NextToken;
                NextToken = lexer.NextToken();
            }
        }
    }
}
