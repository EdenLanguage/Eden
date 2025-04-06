using EdenClasslibrary.Errors;
using EdenClasslibrary.Errors.LexicalErrors;
using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Errors.SyntacticalErrors;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.Enums;
using EdenClasslibrary.Types.Excpetions;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EdenClasslibrary.Types
{
    public class Parser
    {
        #region Fields
        private readonly Lexer _lexer;
        private readonly LiteralsTable _literalsTable;
        private Statement _program;
        private ErrorsManager _errorsManager;
        private readonly Dictionary<TokenType, Func<Expression>> _unaryMapping;
        private readonly Dictionary<TokenType, Func<Expression, Expression>> _binaryMapping;
        private readonly Dictionary<TokenType, Precedence> _precedenceMapping;
        #endregion

        #region Properties
        public Token NextToken { get; set; }
        public Token CurrentToken { get; set; }
        public Lexer Lexer
        {
            get
            {
                return _lexer;
            }
        }
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
            _literalsTable = new LiteralsTable();

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

            _precedenceMapping[TokenType.And] = Precedence.Logical;
            _precedenceMapping[TokenType.Or] = Precedence.Logical;
            _precedenceMapping[TokenType.Bool] = Precedence.Logical;

            _precedenceMapping[TokenType.LeftSquareBracket] = Precedence.Index;

            _precedenceMapping[TokenType.Assign] = Precedence.Equals;

            _precedenceMapping[TokenType.Inequal] = Precedence.Comparison;
            _precedenceMapping[TokenType.Equal] = Precedence.Comparison;
            _precedenceMapping[TokenType.LeftArrow] = Precedence.Comparison;
            _precedenceMapping[TokenType.RightArrow] = Precedence.Comparison;
            _precedenceMapping[TokenType.GreaterOrEqual] = Precedence.Comparison;
            _precedenceMapping[TokenType.LesserOrEqual] = Precedence.Comparison;

            _precedenceMapping[TokenType.Plus] = Precedence.Sum;
            _precedenceMapping[TokenType.Minus] = Precedence.Sum;

            _precedenceMapping[TokenType.Modulo] = Precedence.Product;
            _precedenceMapping[TokenType.Star] = Precedence.Product;
            _precedenceMapping[TokenType.Slash] = Precedence.Product;

            _precedenceMapping[TokenType.LeftParenthesis] = Precedence.Call;
            _precedenceMapping[TokenType.Dot] = Precedence.Call;

            RegisterUnaryMapping(TokenType.If, ParseConditionalExpression);
            RegisterUnaryMapping(TokenType.Identifier, ParseIdentifierExpression);
            RegisterUnaryMapping(TokenType.LiteralIdentifier, ParseLiteralIdentifierExpression);
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

            RegisterBinaryMapping(TokenType.Modulo, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.And, ParseBinaryExpression);
            RegisterBinaryMapping(TokenType.Or, ParseBinaryExpression);
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
            RegisterBinaryMapping(TokenType.LeftParenthesis, ParseFunctionCallExpression);
            RegisterBinaryMapping(TokenType.Dot, ParseMethodCallExpression);
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
            ClearParser();
            _lexer.SetInput(code);
            _program = ParseTopLevelStatements();
            return _program;
        }

        public Statement ParseFile(string path)
        {
            ClearParser();
            _lexer.LoadFile(path);
            _program = ParseTopLevelStatements();
            return _program;
        }
        #endregion

        #region Helper methods
        private void ClearParser()
        {
            CurrentToken = null;
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
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }

            Expression leftNodeExpression = unaryFunc();
            if(leftNodeExpression is InvalidExpression asInvalidNodeExp)
            {
                return asInvalidNodeExp;
            }

            Precedence leftNodePrecedence = CurrentTokenEvaluationOrder();

            while (NextToken.Keyword != TokenType.Semicolon && precedence < NextTokenEvaluationOrder())
            {
                Func<Expression, Expression> binaryFunc = null;

                try
                {
                    binaryFunc = GetBinaryFunc(NextToken.Keyword);
                }
                catch (Exception)
                {
                    return InvalidExpression.Create(NextToken, ErrorSemanticalUndefBinaryOpExp.Create(NextToken, _lexer.GetLine(NextToken)));
                }


                LoadNextToken();

                leftNodeExpression = binaryFunc(leftNodeExpression);
            }

            //LoadNextToken();

            return leftNodeExpression;
        }

        /// <summary>
        /// Parse arguments of List type. Example <[> <args> <]>
        /// </summary>
        /// <param name="variableTypeExp"></param>
        /// <returns></returns>
        private Expression ParseListArguments(VariableTypeExpression variableTypeExp)
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.LeftSquareBracket);
            if (ish is InvalidExpression) return ish;
            ListArgumentsExpression arguments = new ListArgumentsExpression(CurrentToken);
            arguments.Type = variableTypeExp;
            LoadNextToken();

            //  If this token is ']' this means that defined collection is empty.
            if (CurrentToken.IsType(TokenType.RightSquareBracket))
            {
                return arguments;
            }

            //  In this case it is either not empty or invalid.
            while (!CurrentToken.IsType(TokenType.RightBracket))
            {
                Expression argument = ParseExpression(Precedence.Lowest);
                if(argument is InvalidExpression invalidArgument)
                {
                    return invalidArgument;
                }
                //  Eat last token of parsed expression.
                LoadNextToken();
                arguments.AddArgument(argument);

                if (CurrentToken.IsType(TokenType.RightSquareBracket))
                {
                    break;
                }

                ish = ValidateTokenForExpression(TokenType.Comma);
                if (ish is InvalidExpression) return ish;
                LoadNextToken();
            }

            return arguments;
        }

        /// <summary>
        /// Parse List constructor. Example <(> <Exp> <)>  
        /// </summary>
        /// <param name="variableTypeExp"></param>
        /// <returns></returns>
        private Expression ParseListSize(VariableTypeExpression variableTypeExp)
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
            ListArgumentsExpression arguments = new ListArgumentsExpression(CurrentToken);
            arguments.Type = variableTypeExp;
            LoadNextToken();

            ish = ValidateTokenForExpression([TokenType.Int, TokenType.LiteralIdentifier]);
            if (ish is InvalidExpression) return ish;
            Expression expression = ParseExpression(Precedence.Lowest);
            if(expression is InvalidExpression AsInvalidSizeExp)
            {
                return AsInvalidSizeExp;
            }
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.RightParenthesis);
            if (ish is InvalidExpression) return ish;

            arguments.SizeExpression = expression;

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

        /// <summary>
        /// Parse binary expression like '5 + 5'
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        private Expression ParseBinaryExpression(Expression left)
        {
            BinaryExpression binaryExpression = new BinaryExpression(CurrentToken);
            
            Precedence currentEvalOrder = CurrentTokenEvaluationOrder();
            LoadNextToken();

            Expression right = ParseExpression(currentEvalOrder);
            if(right is InvalidExpression invalidRight)
            {
                return invalidRight;
            }

            binaryExpression.Left = left;
            binaryExpression.Right = right;

            return binaryExpression;
        }

        /// <summary>
        /// Parse function call expression. Example 'Length("abc")'
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private Expression ParseFunctionCallExpression(Expression function)
        {
            Expression ish;

            FunctionCallExpression callExp = new FunctionCallExpression(CurrentToken);
            callExp.Function = function;
            
            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            while (!CurrentToken.IsType(TokenType.RightParenthesis) && !CurrentToken.IsType(TokenType.Eof))
            {
                Expression argument = ParseExpression(Precedence.Lowest);
                if(argument is InvalidExpression invalidArgument)
                {
                    return invalidArgument;
                }
                callExp.AddArgumentExpression(argument);
                LoadNextToken(); //  Eat parsed token

                //  Eat ',' token that is separating arguments
                if (!CurrentToken.IsType(TokenType.RightParenthesis))
                {
                    ish = ValidateTokenForExpression(TokenType.Comma);
                    if (ish is InvalidExpression) return ish;
                    LoadNextToken();
                }
            }

            ish = ValidateTokenForExpression(TokenType.RightParenthesis);
            if (ish is InvalidExpression) return ish;

            return callExp;
        }

        private Expression ParseMethodCallExpression(Expression classObj)
        {
            //  <object> <.> <method-name> <(> <argument> <)>
            //  <method-name> <.> <(> <object>, <argument> <)>
            Expression ish;

            MethodCallExpression methodCallExp = new MethodCallExpression(classObj.NodeToken);

            ish = ValidateTokenForExpression(TokenType.Dot);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            Expression methodName = ParseIdentifierExpression();
            if(methodName is InvalidExpression asInvalidMethodName)
            {
                return asInvalidMethodName;
            }
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            while (!CurrentToken.IsType(TokenType.RightParenthesis) && !CurrentToken.IsType(TokenType.Eof))
            {
                Expression argument = ParseExpression(Precedence.Lowest);
                if (argument is InvalidExpression invalidArgument)
                {
                    return invalidArgument;
                }
                methodCallExp.AddArgumentExpression(argument);
                LoadNextToken(); //  Eat parsed token

                //  Eat ',' token that is separating arguments
                if (!CurrentToken.IsType(TokenType.RightParenthesis))
                {
                    ish = ValidateTokenForExpression(TokenType.Comma);
                    if (ish is InvalidExpression) return ish;
                    LoadNextToken();
                }
            }

            ish = ValidateTokenForExpression(TokenType.RightParenthesis);
            if (ish is InvalidExpression) return ish;

            methodCallExp.ClassObject = classObj;
            methodCallExp.Method = methodName;

            return methodCallExp;
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
            Expression idExp= new IdentifierExpression(CurrentToken);
            return idExp;
        }

        /// <summary>
        /// Parse `Identifier` expression. If `Identifier` exists in `Literals Table` it is replaced with it's value.
        /// </summary>
        /// <returns></returns>
        private Expression ParseLiteralIdentifierExpression()
        {
            Expression idExp = new LiteralIdentifier(CurrentToken);

            string literalName = CurrentToken.LiteralValue;
            bool literalExists = _literalsTable.LiteralExists(literalName);
            if (literalExists == true)
            {
                idExp = _literalsTable.GetLiteral(literalName);
            }

            return idExp;
        }

        private Expression ParseFunctionExpression()
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.Function);
            if (ish is InvalidExpression) return ish;
            FunctionExpression functionExpression = new FunctionExpression(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.VariableType);
            if (ish is InvalidExpression) return ish;
            Expression type = new VariableTypeExpression(CurrentToken);
            if (type is InvalidExpression asInvalidType)
            {
                return asInvalidType;
            }
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.Identifier);
            if (ish is InvalidExpression) return ish;
            Expression name = new IdentifierExpression(CurrentToken);
            if (type is InvalidExpression asInvalidName)
            {
                return asInvalidName;
            }
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
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
            ish = ValidateTokenForExpression(TokenType.RightParenthesis);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            Statement body = ParseBlockStatement();
            if(body is InvalidStatement asInvalid)
            {
                return InvalidExpression.Create(asInvalid);
            }

            functionExpression.Type = type;
            functionExpression.Name = name;
            functionExpression.Body = body;

            return functionExpression;
        }

        /// <summary>
        /// Parse variable definition exp. Used to parse function's signature. 
        /// </summary>
        /// <returns></returns>
        private Expression ParseVariableDefinitionExpression()
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.Var);
            if (ish is InvalidExpression) return ish;
            VariableDefinitionExpression variableDefinition = new VariableDefinitionExpression(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.VariableType);
            if (ish is InvalidExpression) return ish;
            VariableTypeExpression type = new VariableTypeExpression(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.Identifier);
            if (ish is InvalidExpression) return ish;
            IdentifierExpression name = new IdentifierExpression(CurrentToken);
            LoadNextToken();

            variableDefinition.Name = name;
            variableDefinition.Type = type;

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
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalInvalidTypeParse.Create(CurrentToken, "Float", _lexer.GetLine(CurrentToken)));
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
                    return InvalidExpression.Create(CurrentToken, ErrorSyntacticalInvalidTypeParse.Create(CurrentToken, "Char", _lexer.GetLine(CurrentToken)));
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
                    return InvalidExpression.Create(CurrentToken, ErrorSyntacticalInvalidTypeParse.Create(CurrentToken, "Char", _lexer.GetLine(CurrentToken)));
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
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalInvalidTypeParse.Create(CurrentToken, "String", _lexer.GetLine(CurrentToken)));
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
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalInvalidTypeParse.Create(CurrentToken, "Int", _lexer.GetLine(CurrentToken)));
            }

            intExp.Value = parsed;

            return intExp;
        }

        private Expression ParseNullExpression()
        {
            NullExpression nullExp = new NullExpression(CurrentToken);
            return nullExp;
        }
        #endregion

        #region Statements parsing methods
        private Statement ParseTopLevelStatement()
        {
            Statement statement = null;
            try
            {
                switch (CurrentToken.Keyword)
                {
                    case TokenType.Loop:
                        statement = ParseLoopStatement();
                        break;
                    case TokenType.Sisyphus:
                        statement = ParseSisyphusStatement();
                        break;
                    case TokenType.Var:
                        statement = ParseVariableStatement();
                        break;
                    case TokenType.Return:
                        statement = ParseReturnStatement();
                        break;
                    case TokenType.List:
                        statement = ParseListStatement();
                        break;
                    case TokenType.Literal:
                        statement = ParseLiteralStatement();
                        break;
                    case TokenType.Skip:
                    case TokenType.Quit:
                        //  This is illegal at top level
                        statement = InvalidStatement.Create(CurrentToken, ErrorSemanticalStatementNotDefinedInTopLvl.Create(CurrentToken, _lexer.GetLine(CurrentToken)));   
                        break;
                    default:
                        statement = ParseExpressionStatement();
                        break;
                }
            }
            catch (CurrentTokenIsIllegal)
            {
                statement = InvalidStatement.Create(CurrentToken, ErrorLexicalIllegalToken.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            catch (NextTokenIsIllegal)
            {
                statement = InvalidStatement.Create(NextToken, ErrorLexicalIllegalToken.Create(NextToken, _lexer.GetLine(NextToken)));
            }
            return statement;
        }

        private Statement ParseStatement()
        {
            Statement statement = null;
            try
            {
                switch (CurrentToken.Keyword)
                {
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
                    case TokenType.List:
                        statement = ParseListStatement();
                        break;
                    case TokenType.Literal:
                        //  Literal statements are only possible at top level.
                        statement = InvalidStatement.Create(CurrentToken, ErrorSemanticalStatementOnlyAvailableAtTopLvl.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
                        break;
                    default:
                        statement = ParseExpressionStatement();
                        break;
                }
            }
            catch (CurrentTokenIsIllegal)
            {
                statement = InvalidStatement.Create(CurrentToken, ErrorLexicalIllegalToken.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            catch (NextTokenIsIllegal)
            {
                statement = InvalidStatement.Create(NextToken, ErrorLexicalIllegalToken.Create(NextToken, _lexer.GetLine(NextToken)));
            }
            return statement;
        }

        private Statement ParseExpressionStatement()
        {
            Statement ish;
            ExpressionStatement statement = new ExpressionStatement(CurrentToken);

            Expression exp = ParseExpression(Precedence.Lowest);

            if (exp is InvalidExpression AsInvalidExp)
            {
                return new InvalidStatement(AsInvalidExp.NodeToken, AsInvalidExp.Error);
            }

            statement.Expression = exp;

            if (NextToken.Keyword == TokenType.Semicolon)
            {
                //  Eat last token of expression
                LoadNextToken();
            }

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            return statement;
        }

        private Statement ValidateExpressionForLiteralStatement(Expression expression)
        {
            Token token = _literalsTable.Check(expression);
            if (token is not null)
            {
                //  Return error saying that literal can only be made of literal values of type Int, Char, Float and String or already defined Literals.
                return InvalidStatement.Create(token, ErrorSemanticalLiteralEvaluation.Create(token, _lexer.GetLine(token)));
            }
            return ValidTokenStatement.Create(token);
        }

        private Statement ValidateTokenForStatement(TokenType expected)
        {
            if(CurrentToken.Keyword != expected)
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalUnexpectedToken.Create(expected, CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Statement ValidateTokenForStatement(params TokenType[] expected)
        {
            if (!expected.Contains(CurrentToken.Keyword))
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalUnexpectedTokens.Create(expected, CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Statement ValidateTokenIsNotTypeForStatement(TokenType expected)
        {
            if (CurrentToken.Keyword == expected)
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Statement ValidateTokenForStatementAsVarType()
        {
            if (!CurrentToken.IsVariableType())
            {
                return InvalidStatement.Create(CurrentToken, ErrorSyntacticalTokenNotVarType.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenStatement.Create(CurrentToken);
        }

        private Expression ValidateTokenForExpression(TokenType expected)
        {
            if (CurrentToken.Keyword != expected)
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedToken.Create(expected, CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenExpression.Create(CurrentToken);
        }

        private Expression ValidateTokenForExpression(params TokenType[] expected)
        {
            if (!expected.Contains(CurrentToken.Keyword))
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedTokens.Create(expected, CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            return ValidTokenExpression.Create(CurrentToken);
        }

        private Expression ValidateTokenIsNotTypeForExpression(TokenType expected)
        {
            if (CurrentToken.Keyword == expected)
            {
                return InvalidExpression.Create(CurrentToken, ErrorSyntacticalUnexpectedExpression.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
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

        /// <summary>
        /// Parse conditional expressions. All of the conditionals start with 'If', they may contain 'Else'
        /// </summary>
        /// <returns></returns>
        private Expression ParseConditionalExpression()
        {
            Expression ish;

            ish = ValidateTokenForExpression(TokenType.If);
            if (ish is InvalidExpression) return ish;
            IfExpression ifExpression = new IfExpression(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.LeftParenthesis);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            Expression condition = ParseExpression(Precedence.Lowest);
            if(condition is InvalidExpression invalidCondition)
            {
                return invalidCondition;
            }
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.RightParenthesis);
            if (ish is InvalidExpression) return ish;
            LoadNextToken();

            ish = ValidateTokenForExpression(TokenType.LeftBracket);
            if (ish is InvalidExpression) return ish;

            Statement block = ParseBlockStatement();
            if(block is InvalidStatement invalidBlock)
            {
                return InvalidExpression.Create(invalidBlock);
            }
            LoadNextToken();

            //  TODO: Implement 'ElseIf' logic
            Statement alternative = null;
            if (CurrentToken.IsType(TokenType.Else))
            {
                ish = ValidateTokenForExpression(TokenType.Else);
                if (ish is InvalidExpression) return ish;
                LoadNextToken();

                alternative = ParseBlockStatement();
            }

            ifExpression.ConditionExpression = condition;
            ifExpression.FulfielldBlock = block;
            ifExpression.AlternativeBlock = alternative;

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
                    return AsInvalidStatement;
                }

                block.AddStatement(statement);
            }

            return block;
        }

        private Statement ParseBlockStatement()
        {
            Statement ish;

            BlockStatement block = new BlockStatement(CurrentToken);

            //  Teraz z jednej strony jak sie coluje te funckje to nie jest sprawdzane wstępowanie '{'.
            //  No i w przypadku programu nie jest to potrzebne ale dla funkcji to tak.
            ish = ValidateTokenForStatement(TokenType.LeftBracket);
            if (ish is InvalidStatement) return ish;
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

        private Statement ParseTopLevelStatements()
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
                return InvalidStatement.Create(CurrentToken, ErrorLexicalIllegalToken.Create(CurrentToken, _lexer.GetLine(CurrentToken)));
            }
            catch (NextTokenIsIllegal exception)
            {
                return InvalidStatement.Create(NextToken, ErrorLexicalIllegalToken.Create(NextToken, _lexer.GetLine(CurrentToken)));
            }

            while (!CurrentToken.IsType(TokenType.RightBracket) && !CurrentToken.IsType(TokenType.Eof))
            {
                Statement statement = ParseTopLevelStatement();
                
                if (statement is InvalidStatement AsInvalidStatement)
                {
                    return AsInvalidStatement;
                
                }
                else if(statement is LiteralStatement AsLiteralStatement)
                {
                    // If the parser managed to reach this point, it means that the given `LiteralStatement` was successfully parsed
                    // and added to the `LiteralsTable`, meaning its value is already stored.
                    // `LiteralStatement`s are not meant to be evaluated by the `Evaluator`, e.g., in a form like `Literal 10i As Size;`.
                    // In fact, everywhere a literal is used, its name should be replaced by its value.
                    // Therefore, we should skip adding the `LiteralStatement` to the program block's statements list.
                    continue;
                }
                else
                {
                    programBlock.AddStatement(statement);
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

            ish = ValidateTokenForStatement(TokenType.RightParenthesis);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.LeftBracket);
            if (ish is InvalidStatement) return ish;
            Statement body = ParseLoopBlockStatement();
            if(body is InvalidStatement asInvalidBlock)
            {
                return asInvalidBlock;
            }

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
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Skip);
            if (ish is InvalidStatement) return ish;
            SkipStatement skip = new SkipStatement(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            return skip;
        }

        private Statement ParseQuitStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Quit);
            if (ish is InvalidStatement) return ish;
            QuitStatement quit = new QuitStatement(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            return quit;
        }

        private Statement ParseSisyphusStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Sisyphus);
            if (ish is InvalidStatement) return ish;
            SisyphusStatement statement = new SisyphusStatement(CurrentToken);
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.LeftBracket);
            if (ish is InvalidStatement) return ish;

            Statement body = ParseLoopBlockStatement();
            if(body is InvalidStatement invalidBody)
            {
                return invalidBody;
            }

            ish = ValidateTokenForStatement(TokenType.RightBracket);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
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
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.List);
            if (ish is InvalidStatement) return ish;
            ListDeclarationStatement list = new ListDeclarationStatement(CurrentToken);
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

            ish = ValidateTokenForStatement([TokenType.LeftSquareBracket, TokenType.LeftParenthesis]);
            if (ish is InvalidStatement) return ish;

            Expression expression = new EmptyExpression(CurrentToken);
            if (CurrentToken.IsType(TokenType.LeftSquareBracket))
            {
                expression = ParseListArguments(variableTypeExp);
            }
            else
            {
                expression = ParseListSize(variableTypeExp);
            }

            if(expression is InvalidExpression invalidArgsExp)
            {
                return InvalidStatement.Create(invalidArgsExp);
            }
            LoadNextToken();
            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            list.Type = variableTypeExp;
            list.Identifier = identifierExp;
            list.Expression = expression;

            return list;
        }

        private Statement ParseLiteralStatement()
        {
            Statement ish;

            ish = ValidateTokenForStatement(TokenType.Literal);
            if (ish is InvalidStatement) return ish;
            LiteralStatement literalStatement = new LiteralStatement(CurrentToken);
            LoadNextToken();

            //  Expression is parsed but we have to make sure that it has only literal values and `Identifier` tokens were swapped with their value.
            //  Otherwise we should return invalid statement error.
            Expression expression = ParseExpression(Precedence.Lowest);

            ish = ValidateExpressionForLiteralStatement(expression);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.As);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.LiteralIdentifier);
            if (ish is InvalidStatement) return ish;
            Expression name = ParseExpression(Precedence.Lowest);
            if(name is InvalidExpression AsInvalidName)
            {
                return InvalidStatement.Create(AsInvalidName);
            }
            LoadNextToken();

            ish = ValidateTokenForStatement(TokenType.Semicolon);
            if (ish is InvalidStatement) return ish;
            LoadNextToken();

            literalStatement.Expression = expression;
            literalStatement.Name = name;

            string literalName = name.NodeToken.LiteralValue;
            bool isLiteralDefined = _literalsTable.LiteralExists(literalName);
            if(isLiteralDefined == true)
            {
                Token litNameToken = literalStatement.Name.NodeToken;
                return InvalidStatement.Create(litNameToken, ErrorSemanticalLiteralAlreadyDefined.Create(litNameToken, _lexer.GetLine(litNameToken)));
            }
            else
            {
                _literalsTable.AddLiteral(literalStatement);
            }

            return literalStatement;
        }

        /// <summary>
        /// Parse variable statement.
        /// </summary>
        /// <returns></returns>
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
