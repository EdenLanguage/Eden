using EdenClasslibrary.Errors;
using EdenClasslibrary.Errors.LexicalErrors;
using EdenClasslibrary.Errors.SyntacticalErrors;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.Enums;
using EdenClasslibrary.Types.Excpetions;
using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace EdenClasslibrary.Parser
{
    public class Parser
    {
        #region Fields
        private readonly Lexer _lexer;
        private Statement _program;
        private ErrorsManager _errorsManager;
        private readonly Dictionary<TokenType, Func<Expression>> _unaryMapping;
        private readonly Dictionary<TokenType, Func<Expression, Expression>> _binaryMapping;
        private readonly Dictionary<TokenType, Precedence> _precedenceMapping;
        #endregion

        #region Properties
        public Token NextToken { get; set; }
        public Token CurrentToken { get; set; }
        public Statement ProgramStatement { get { return _program; } }
        public FileStatement Program { get { return _program as FileStatement; } }
        public AError[] Errors
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

            _precedenceMapping[TokenType.LeftSquareBracket] = Precedence.Index;

            _precedenceMapping[TokenType.Assign] = Precedence.Equals;

            // == !=
            _precedenceMapping[TokenType.Inequal] = Precedence.Comparison;
            _precedenceMapping[TokenType.Equal] = Precedence.Comparison;
            _precedenceMapping[TokenType.LeftArrow] = Precedence.Comparison;
            _precedenceMapping[TokenType.RightArrow] = Precedence.Comparison;
            _precedenceMapping[TokenType.GreaterOrEqual] = Precedence.Comparison;
            _precedenceMapping[TokenType.LesserOrEqual] = Precedence.Comparison;

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
            RegisterUnaryMapping(TokenType.Null, ParseNullExpression);
            RegisterUnaryMapping(TokenType.Float, ParseFloatExpression);
            RegisterUnaryMapping(TokenType.Char, ParseCharExpression);
            RegisterUnaryMapping(TokenType.String, ParseStringExpression);
            RegisterUnaryMapping(TokenType.Bool, ParseBoolExpression);
            RegisterUnaryMapping(TokenType.Minus, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Tilde, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.QuenstionMark, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Plus, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.LeftParenthesis, ParseGroupedExpression);
            RegisterUnaryMapping(TokenType.ExclemationMark, ParseUnaryExpression);
            RegisterUnaryMapping(TokenType.Function, ParseFunctionExpression);

            RegisterBinaryMapping(TokenType.Assign, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Plus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Minus, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Star, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Slash, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Inequal, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Equal, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.LeftArrow, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.RightArrow, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.GreaterOrEqual, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.LesserOrEqual, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.LeftParenthesis, ParseCallExpression);
            RegisterBinaryMapping(TokenType.LeftSquareBracket, ParseIndexExpression);
            #endregion
        }
        #endregion

        #region Public
        /// <summary>
        /// Parse input data token by token.
        /// </summary>
        /// <param name="code">Source code</param>
        /// <returns>Abstract syntax tree of input data</returns>
        public Statement Parse(string code)
        {
            _lexer.SetInput(code);
            _program = ParseFileStatement();
            return _program;
        }

        public Statement ParseFile(string path)
        {
            //string sourceCode = GetSource(path);
            _lexer.LoadFile(path);
            _program = ParseFileStatement();
            return _program;
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
                if (CurrentToken.Keyword == TokenType.Illegal)
                {
                    throw new CurrentTokenIsIllegal();
                }

                NextToken = _lexer.NextToken();
            }
            else
            {
                // It was so just go oe position ahead.
                CurrentToken = NextToken;
                NextToken = _lexer.NextToken();
            }
            if (NextToken.Keyword == TokenType.Illegal)
            {
                throw new NextTokenIsIllegal();
            }
        }

        public string PrintErrors()
        {
            StringBuilder sb = new StringBuilder();

            foreach (AError error in Errors)
            {
                sb.Append(error.PrintError());
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

            Expression ish;

            ish = ValidateTokenIsNotTypeForExpression(TokenType.VariableType);
            if (ish is InvalidExpression) return ish;

            Func<Expression> unaryFunc = null;
            try
            {
                unaryFunc = GetUnaryFunc(CurrentToken.Keyword);
            }
            catch (Exception)
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken));
            }

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

        private Expression ParseListArguments(VariableTypeExpression variableTypeExp)
        {
            ListArgumentsExpression arguments = new ListArgumentsExpression(CurrentToken);
            arguments.Type = variableTypeExp;

            if (!CurrentToken.IsType(TokenType.LeftSquareBracket))
            {
                return null; //new InvalidExpression(CurrentToken);
            }

            if (NextToken.IsType(TokenType.RightSquareBracket))
            {
                //  Empty arguments list.
                LoadNextToken();
                return arguments;
            }

            LoadNextToken();
            while (!CurrentToken.IsType(TokenType.RightBracket))
            {
                Expression singleArgument = ParseExpression(Precedence.Lowest);
                arguments.AddArgument(singleArgument);

                //  Eat <argument> and <,>
                LoadNextToken();

                if (CurrentToken.IsType(TokenType.RightSquareBracket))
                {
                    break;
                }

                LoadNextToken();

            }

            return arguments;
        }

        private Expression ParseListSize(VariableTypeExpression variableTypeExp)
        {
            ListArgumentsExpression arguments = new ListArgumentsExpression(CurrentToken);
            arguments.Type = variableTypeExp;

            if (!CurrentToken.IsType(TokenType.LeftParenthesis))
            {
                return null; //new InvalidExpression(CurrentToken);
            }
            LoadNextToken();

            Expression expression = null;
            if (!CurrentToken.IsType(TokenType.Int))
            {
                return null; //new InvalidExpression(CurrentToken);
            }
            else
            {
                expression = ParseExpression(Precedence.Lowest);
                if (expression is not IntExpression)
                {
                    return null;// new InvalidExpression(CurrentToken);
                }
            }
            LoadNextToken();

            arguments.SetCapacity((expression as IntExpression).Value);

            return arguments;
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
                return null;// ParseInvalidExpression(TokenType.Semicolon, ParserErrorType.InvalidToken);
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

        private Expression ParseIndexExpression(Expression left)
        {
            IndexExpression indexExp = new IndexExpression(CurrentToken);
            LoadNextToken();

            Expression index = ParseExpression(Precedence.Lowest);
            if (index is InvalidExpression isInvalidIndexExp)
            {
                return isInvalidIndexExp;
            }
            LoadNextToken();

            indexExp.Object = left;
            indexExp.Index = index;

            return indexExp;
        }

        private Expression ParseIdentifierExpression()
        {
            IdentifierExpression id = new IdentifierExpression(CurrentToken);
            return id;
        }

        private Expression ParseFunctionExpression()
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.Function);
            if (ish is InvalidExpression) return ish;
            FunctionExpression functionExpression = new FunctionExpression(CurrentToken);
            LoadNextToken();

            //LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.VariableType);
            if (ish is InvalidExpression) return ish;
            functionExpression.Type = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            //if (!CurrentToken.IsType(TokenType.VariableType))
            //{
            //    return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            //}


            ish = ValidateTokenForExpression(TokenType.Identifier);
            if (ish is InvalidExpression) return ish;
            functionExpression.Name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            //LoadNextToken();

            //if (!CurrentToken.IsType(TokenType.Identifier))
            //{
            //    return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            //}

            //LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
            //functionExpression.Name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            //if (!CurrentToken.IsType(TokenType.LeftParenthesis))
            //{
            //    return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            //}
            //LoadNextToken();

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
                return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            }

            variableDefinition.Type = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.Identifier))
            {
                return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            }

            variableDefinition.Name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            return variableDefinition;
        }

        private Expression ParseFloatExpression()
        {
            FloatExpression floatExp = new FloatExpression(CurrentToken);

            string removedf = CurrentToken.LiteralValue.Replace("f", "");
            float parsed = 0;

            bool couldParse = float.TryParse(removedf, CultureInfo.InvariantCulture, out parsed);

            if (couldParse == false)
            {
                return null;
            }

            floatExp.Value = parsed;

            return floatExp;
        }

        private Expression ParseCharExpression()
        {
            CharExpression charExpression = new CharExpression(CurrentToken);

            bool isLiteral = CurrentToken.LiteralValue.Last() == 'c';
            char value = '\0';

            if(isLiteral == true)
            {
                string removedC = CurrentToken.LiteralValue.Replace("c", "");

                int asInt = 0;
                bool couldParse = int.TryParse(removedC, out asInt);

                if(couldParse == false)
                {
                    return null;
                }

                value = (char)(asInt % 256);
            }
            else
            {
                string removedInfixPrefix = CurrentToken.LiteralValue.Substring(1, CurrentToken.LiteralValue.Length - 2);
                string unescaped = Regex.Unescape(removedInfixPrefix);

                bool couldParse = char.TryParse(unescaped, out value);

                if (couldParse == false)
                {
                    return null;
                }
            }
            
            charExpression.Value = value;

            return charExpression;
        }

        private Expression ParseStringExpression()
        {
            StringExpression stringExp = new StringExpression(CurrentToken);

            string result = string.Empty;
            try
            {
                result = CurrentToken.LiteralValue.Substring(1, CurrentToken.LiteralValue.Length - 2);
            }
            catch (Exception)
            {
                return null;
            }

            stringExp.Value = result;

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

            //  If 'Int' token was valid it should look like smth like this: <value><i> Example: 10i
            string removedI = CurrentToken.LiteralValue.Replace("i","");
            int parsed = 0;

            bool couldParse = int.TryParse(removedI, out parsed);

            if(couldParse == false)
            {
                return null;
            }

            intExp.Value = parsed;

            return intExp;
        }

        private Expression ParseNullExpression()
        {
            NullExpression nullExp = new NullExpression(CurrentToken);
            return nullExp;
        }

        //private Expression ParseInvalidExpression(TokenType stopToken, ParserErrorType errorType)
        //{
        //    InvalidExpression invExp = new InvalidExpression(CurrentToken);

        //    _errorsManager.AppendError(ErrorDefaultParser.Create(errorType, CurrentToken));

        //    while (!CurrentToken.IsType(stopToken))
        //    {
        //        LoadNextToken();
        //    }

        //    return invExp;
        //}
        #endregion

        #region Statements parsing methods
        private Statement ParseStatement()
        {
            Statement statement = null;
            try
            {
                switch (CurrentToken.Keyword)
                {
                    case TokenType.VariableType:
                        statement = ParseInvalidStatement(ParserErrorType.InvalidStatement, CurrentToken);
                        break;
                    case TokenType.Loop:
                        statement = ParseLoopStatement();
                        break;
                    case TokenType.Sisyphus:
                        statement = ParseSisyphusStatement();
                        break;
                    case TokenType.Skip:
                        statement = ParseSkipStatement();
                        break;
                    case TokenType.Quit:
                        statement = ParseQuitStatement();
                        break;
                    case TokenType.Var:
                        statement = ParseVariableStatement();
                        break;
                    case TokenType.Return:
                        statement = ParseReturnStatement();
                        break;
                    case TokenType.Keyword:
                        switch (CurrentToken.LiteralValue)
                        {
                            case "Var":
                                statement = ParseVariableStatement();
                                break;
                            case "List":
                                statement = ParseListStatement();
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
                        //    _errors.Add($"There was en error while parsing expression from line:'{statement.NodeToken.Line}' and column:'{statement.NodeToken.Start}'");
                        //    statement = new InvalidStatement(statement.NodeToken);
                        //    EatStatement();
                        //}
                        break;
                }
            }
            catch (CurrentTokenIsIllegal)
            {
                statement = InvalidStatement.Create(CurrentToken, ErrorLexicalIllegalToken.Create(CurrentToken));
            }
            catch (NextTokenIsIllegal)
            {
                statement = InvalidStatement.Create(NextToken, ErrorLexicalIllegalToken.Create(NextToken));
            }
            return statement;
        }

        private Statement ParseInvalidStatement(ParserErrorType errorType, Token invalidToken)
        {
            AError error = ErrorDefaultParser.Create(errorType, invalidToken);

            Statement statement = new InvalidStatement(CurrentToken, error);
            _errorsManager.AppendError(error);
            //EatStatement();

            return statement;
        }

        private Statement ParseExpressionStatement()
        {
            ExpressionStatement statement = new ExpressionStatement(CurrentToken);
            
            Expression exp = ParseExpression(Precedence.Lowest);
            
            if(exp is InvalidExpression AsInvalidExp)
            {
                return new InvalidStatement(AsInvalidExp.NodeToken, ErrorLexicalIllegalToken.Create(CurrentToken));
            }
            
            statement.Expression = exp;

            if (NextToken.Keyword == TokenType.Semicolon)
            {
                //  Eat last token of expression
                LoadNextToken();
            }

            if (!CurrentToken.IsType(TokenType.Semicolon))
            {
                return ParseInvalidStatement(ParserErrorType.InvalidToken, CurrentToken);
            }

            //  Eat ';'
            LoadNextToken();

            return statement;
        }

        private Statement ValidateTokenForStatement(TokenType expected)
        {
            if(CurrentToken.Keyword != expected)
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalUnexpectedToken.Create(CurrentToken, expected));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Statement ValidateTokenIsNotTypeForStatement(TokenType expected)
        {
            if (CurrentToken.Keyword == expected)
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Statement ValidateTokenForStatementAsVarType()
        {
            if (!CurrentToken.IsVariableType())
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalTokenNotVarType.Create(CurrentToken));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Expression ValidateTokenForExpression(TokenType expected)
        {
            if (CurrentToken.Keyword != expected)
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedToken.Create(CurrentToken, expected));
            }
            return ValidTokenExpression.Create(CurrentToken);
        }

        private Expression ValidateTokenIsNotTypeForExpression(TokenType expected)
        {
            if (CurrentToken.Keyword == expected)
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken));
            }
            return ValidTokenExpression.Create(CurrentToken);
        }

        private Statement ParseReturnStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Return);
            if (ish is InvalidStatement) return ish;
            ReturnStatement returnStatement = new ReturnStatement(CurrentToken);
            LoadNextToken();

            Expression expression = ParseExpression(Precedence.Lowest);
            if(expression is InvalidExpression invalidExp)
            {
                return InvalidStatement.Create(invalidExp);
            }
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
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
                return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            }

            //  Eat '('
            LoadNextToken();
            ifExpression.ConditionExpression = ParseExpression(Precedence.Lowest);

            //  Eat ')'
            LoadNextToken();
            if (!CurrentToken.IsType(TokenType.RightParenthesis))
            {
                return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
            }

            LoadNextToken();
            if (!CurrentToken.IsType(TokenType.LeftBracket))
            {
                return null;//(TokenType.Semicolon, ParserErrorType.InvalidToken);
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

        private Statement ParseLoopBlockStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.LeftBracket);
            if (ish is InvalidStatement) return ish;
            LoopBlockStatement block = new LoopBlockStatement(CurrentToken);
            LoadNextToken();
            
            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseStatement();

                if(statement is InvalidStatement AsInvalidStatement)
                {
                    return block;
                }

                block.AddStatement(statement);
            }

            return block;
        }

        private Statement ParseBlockStatement()
        {
            BlockStatement block = new BlockStatement(CurrentToken);

            LoadNextToken();
            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseStatement();

                if(statement is InvalidStatement asInvalidStmt)
                {
                    return asInvalidStmt;
                }

                block.AddStatement(statement);
            }

            return block;
        }

        private Statement ParseFileStatement()
        {
            FileStatement file = new FileStatement(CurrentToken);
            BlockStatement programBlock = new BlockStatement(CurrentToken);
            file.Block = programBlock;

            try
            {
                LoadNextToken();
            }
            catch (CurrentTokenIsIllegal exception)
            {
                //AError error = exception.CreateError(CurrentToken);
                //programBlock.AddStatement(new InvalidStatement(CurrentToken, error));
                //_errorsManager.AppendError(error);
                return InvalidStatement.Create(CurrentToken, ErrorLexicalIllegalToken.Create(CurrentToken));
            }

            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseStatement();
                programBlock.AddStatement(statement);

                if (statement is InvalidStatement AsInvalidStatement)
                {
                    return AsInvalidStatement;
                }
            }

            return file;
        }

        private Statement ParseLoopStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Loop);
            if (ish is InvalidStatement) return ish;
            LoopStatement loop = new LoopStatement(CurrentToken);
            LoadNextToken();
            
            ish = ValidateTokenForStatement(TokenType.LeftParenthesis);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();
            
            Statement varStatement = ParseVariableStatement();
            if (varStatement is InvalidStatement asInvalidVarStatement)
            {
                return asInvalidVarStatement;
            }

            ish = ValidateTokenForStatement(TokenType.Identifier);
            if (ish is InvalidStatement) return ish;
            Expression condition = ParseExpression(Precedence.Lowest);
            if (condition is InvalidExpression asInvalidConditionExpression)
            {
                return InvalidStatement.Create(asInvalidConditionExpression);
            }

            LoadNextToken();
            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Identifier);
            if (ish is InvalidStatement) return ish;
            Expression indexerExp = ParseExpression(Precedence.Lowest);
            if (indexerExp is InvalidExpression asInvalidIndexerExpression)
            {
                return InvalidStatement.Create(asInvalidIndexerExpression);
            }
            LoadNextToken();

            //ish = ValidateTokenForStatement(TokenType.Semicolon);
            //if (ish is InvalidStatement) return ish;
            //LoadNextToken();
            
            ish = ValidateTokenForStatement(TokenType.RightParenthesis);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.LeftBracket);
            if (ish is InvalidStatement) return ish;
            Statement body = ParseLoopBlockStatement();

            ish = ValidateTokenForStatement(TokenType.RightBracket);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            loop.Condition = condition;
            loop.Body = body;
            loop.IndexerStatement = varStatement;
            loop.IndexerOperation = indexerExp;

            return loop;
        }

        private Statement ParseSkipStatement()
        {
            SkipStatement skip = new SkipStatement(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.Semicolon))
            {
                return null;
            }
            LoadNextToken();

            return skip;
        }

        private Statement ParseQuitStatement()
        {
            QuitStatement quit = new QuitStatement(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.Semicolon))
            {
                return null;
            }
            LoadNextToken();

            return quit;
        }

        private Statement ParseSisyphusStatement()
        {
            SisyphusStatement statement = new SisyphusStatement(CurrentToken);
            LoadNextToken();

            if (!CurrentToken.IsType(TokenType.LeftBracket))
            {
                return null;
            }
            //LoadNextToken();

            Statement body = ParseLoopBlockStatement();

            LoadNextToken();
            LoadNextToken();

            statement.Body = body;

            return statement;
        }

        /// <summary>
        /// Parse List declaration statements.
        /// Signature: '<List> <Type> <Identifier> <=> ( (<[> <Values> <]>) | (<(> <Size> <)>) ) <;>'
        /// Example: 'List Int primes = [2,3,5,7];'
        /// </summary>
        /// <returns></returns>
        private Statement ParseListStatement()
        {
            ListDeclarationStatement list = new ListDeclarationStatement(CurrentToken);
            if (CurrentToken.Keyword != TokenType.Keyword)
            {
                return ParseInvalidStatement(ParserErrorType.InvalidVariableDeclaration, CurrentToken);
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
                return ParseInvalidStatement(ParserErrorType.InvalidVariableDeclaration, NextToken);
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
                return ParseInvalidStatement(ParserErrorType.InvalidVariableDeclaration, NextToken);
            }

            LoadNextToken();

            Expression expression = null;
            if (CurrentToken.IsType(TokenType.LeftSquareBracket))
            {
                expression = ParseListArguments(variableTypeExp);
            }
            else if (CurrentToken.IsType(TokenType.LeftParenthesis))
            {
                expression = ParseListSize(variableTypeExp);
            }
            else
            {
                return ParseInvalidStatement(ParserErrorType.InvalidSyntax, NextToken);
            }

            //  Eat <]> or <)>
            LoadNextToken();

            //  If next token is not Semicolon then expression was not parsed properly...
            if (!CurrentToken.IsSemicolon())
            {
                return ParseInvalidStatement(ParserErrorType.InvalidVariableDeclaration_MissingSemicolon, CurrentToken);
            }

            LoadNextToken();

            list.Type = variableTypeExp;
            list.Identifier = identifierExp;
            list.Expression = expression;

            return list;
        }

            // Example: 'var int variable = 10'
        private Statement ParseVariableStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Var);
            if (ish is InvalidStatement) return ish;
            VariableDeclarationStatement variableStatement = new VariableDeclarationStatement(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForStatementAsVarType();
            if (ish is InvalidStatement) return ish;
            VariableTypeExpression variableTypeExp = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Identifier);
            if (ish is InvalidStatement) return ish;
            IdentifierExpression identifierExp = new IdentifierExpression(CurrentToken);
            LoadNextToken();
            

            ish = ValidateTokenForStatement(TokenType.Assign);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();
            
            Expression expression = ParseExpression(Precedence.Lowest);
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
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
