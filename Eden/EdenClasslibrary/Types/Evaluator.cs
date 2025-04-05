using EdenClasslibrary.Errors;
using EdenClasslibrary.Errors.RuntimeErrors;
using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Errors.SyntacticalErrors;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.EnvironmentTypes;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.Net.Http.Headers;
using System.Reflection;

namespace EdenClasslibrary.Types
{
    public class Evaluator
    {
        private ParsingEnvironment _parsingEnv;
        private EvaluationMapper _evalFuncsMapper;
        private ErrorsManager _errorManager;
        private Parser _parser;
        private BuildIn _buildIn;

        public Evaluator(Parser parser)
        {
            _parser = parser;
            _buildIn = new BuildIn(parser);
            _parsingEnv = new ParsingEnvironment(parser, _buildIn);
            _evalFuncsMapper = new EvaluationMapper(parser);
            _errorManager = new ErrorsManager();
        }

        public IObject EvaluateFile(string path)
        {
            AbstractSyntaxTreeNode ast = _parser.ParseFile(path);

            return Evaluate(ast);
        }

        public IObject Evaluate(string code)
        {
            AbstractSyntaxTreeNode ast = _parser.Parse(code);

            return Evaluate(ast);
        }

        public IObject Evaluate(AbstractSyntaxTreeNode root)
        {
            if (root is InvalidExpression || root is InvalidStatement)
            {
                if(root is InvalidExpression invalidExp)
                {
                    return ErrorObject.Create(root.NodeToken, invalidExp.Error);
                }
                else if (root is InvalidStatement invalidStatement)
                {
                    return ErrorObject.Create(root.NodeToken, invalidStatement.Error);
                }
                else throw new Exception();
            }

            if (root is Expression)
            {
                return EvaluateExpression(root as Expression, _parsingEnv);
            }
            else if (root is Statement)
            {
                return EvaluateStatement(root as Statement, _parsingEnv);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }
        #region Helper methods
        private IObject EvaluateExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            if (root is IntExpression)
            {
                return EvaluateIntExpression(root as IntExpression);
            }
            else if (root is StringExpression)
            {
                return EvaluateStringExpression(root as StringExpression);
            }
            else if (root is CharExpression)
            {
                return EvaluateCharExpression(root as CharExpression);
            }
            else if (root is FloatExpression)
            {
                return EvaluateFloatExpression(root as FloatExpression);
            }
            else if (root is BinaryExpression)
            {
                return EvaluateBinaryExpression(root as BinaryExpression, env);
            }
            else if (root is UnaryExpression)
            {
                return EvaluateUnaryExpression(root as UnaryExpression, env);
            }
            else if (root is BoolExpresion)
            {
                return EvaluateBoolExpression(root as BoolExpresion);
            }
            else if (root is ExpressionStatement)
            {
                return EvaluateExpressionStatement(root as ExpressionStatement, env);
            }
            else if (root is IdentifierExpression)
            {
                return EvaluateIdentifierExpression(root as IdentifierExpression, env);
            }
            else if (root is VariableDefinitionExpression)
            {
                return EvaluateVariableDeclarationExpression(root as VariableDefinitionExpression, env);
            }
            else if (root is ListArgumentsExpression)
            {
                return EvaluateListDeclarationExpression(root as ListArgumentsExpression, env);
            }
            else if (root is FunctionCallExpression)
            {
                return EvaluateFunctionCallExpression(root as FunctionCallExpression, env);
            }
            else if (root is MethodCallExpression)
            {
                return EvaluateMethodCallExpression(root as MethodCallExpression, env);
            }
            else if (root is IndexExpression)
            {
                return EvaluateIndexExpression(root as IndexExpression, env);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }

        private IObject EvaluateStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            if (root is FileStatement)
            {
                return EvaluateFileStatement(root as FileStatement, env);
            }
            else if (root is BlockStatement)
            {
                return EvaluateBlockStatement(root as BlockStatement, env);
            }
            else if (root is ExpressionStatement)
            {
                return EvaluateExpressionStatement(root as ExpressionStatement, env);
            }
            else if (root is ReturnStatement)
            {
                return EvaluateReturnStatement(root as ReturnStatement, env);
            }
            else if (root is VariableDeclarationStatement)
            {
                return EvaluateVariableDeclarationStatement(root as VariableDeclarationStatement, env);
            }
            else if (root is ListDeclarationStatement)
            {
                return EvaluateListDeclarationStatement(root as ListDeclarationStatement, env);
            }
            else if (root is LoopStatement)
            {
                return EvaluateLoopStatement(root as LoopStatement, env);
            }
            else if (root is SisyphusStatement)
            {
                return EvaluateSisyphusStatement(root as SisyphusStatement, env);
            }
            else if (root is SkipStatement)
            {
                return EvaluateSkipStatement(root as SkipStatement, env);
            }
            else if (root is QuitStatement)
            {
                return EvaluateQuitStatement(root as QuitStatement, env);
            }
            else if (root is NullExpression)
            {
                return EvaluateQuitStatement(root as NullExpression, env);
            }
            else if (root is InvalidStatement)
            {
                return EvaluateInvalidStatement(root as InvalidStatement, env);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #region Statements evaluators
        private IObject EvaluateFileStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            BlockStatement block = (root as FileStatement).Block;

            IObject result = EvaluateBlockStatement(block, env);

            if (result is ReturnObject AsReturnObj)
            {
                return AsReturnObj.WrappedObject;
            }
            else if (result is ErrorObject AsErrorObject)
            {
                return AsErrorObject;
            }

            return result;
        }

        private IObject EvaluateBlockStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            BlockStatement block = root as BlockStatement;
            IObject result = NoneObject.Create(block.NodeToken);
            
            Statement[] blockStatements = block.Statements;
            
            foreach (Statement statement in blockStatements)
            {
                result = EvaluateStatement(statement, env);

                //  We don't need to evaluate further.
                if (result is ReturnObject AsReturnObj)
                {
                    return AsReturnObj;
                }
                else if (result is SkipObject AsSkipObj)
                {
                    return AsSkipObj;
                }
                else if (result is QuitObject AsQuitObj)
                {
                    return AsQuitObj;
                }
                else if (result is ErrorObject AsErrorObj)
                {
                    return AsErrorObj;
                }
            }

            return result;
        }

        private IObject EvaluateReturnStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            ReturnStatement retStmt = root as ReturnStatement;

            IObject returnExp = EvaluateExpression(retStmt.Expression, env);

            return new ReturnObject(retStmt.NodeToken, returnExp);
        }

        private IObject EvaluateInvalidStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            return ErrorSyntacticalInvalidStatement.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
        }

        /// <summary>
        /// Handles evaluating variable declaration. Example: 'Var Int counter = 10';
        /// It has to check whether variable with such name was previously created.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private IObject EvaluateVariableDeclarationStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            /*  In future it is important to create function on eval mapper that allows to assign value to variable. This will allow to check whether there was a type missmatch.
             */

            VariableDeclarationStatement varStatement = root as VariableDeclarationStatement;

            //  Will be used to check whether this assingment is possible.
            VariableTypeExpression type = varStatement.Type;

            //  Variable with this name should not exist before this declaration!
            IdentifierExpression identifier = varStatement.Identifier;

            IObject rightSide = EvaluateExpression(varStatement.Expression, env);

            if (rightSide is ErrorObject asError)
            {
                return asError;
            }

            bool varAlreadyDefined = env.VariableExistsRoot(identifier.Name);
            if (varAlreadyDefined == true)
            {
                return ErrorSemanticalVarRefined.CreateErrorObject(identifier.Name, root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            else
            {
                IObject definedVariable = env.DefineVariable(identifier.Name, VariablePayload.Create(type.Type, rightSide));
                if(definedVariable is ErrorObject varAsError)
                {
                    return varAsError;
                }
            }
            return NoneObject.Create(varStatement.NodeToken);
        }

        private IObject EvaluateListDeclarationStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            ListDeclarationStatement list = root as ListDeclarationStatement;

            //  Will be used to check whether this assingment is possible.
            VariableTypeExpression type = list.Type;

            //  Variable with this name should not exist before this declaration!
            IdentifierExpression identifier = list.Identifier;

            IObject rightSide = EvaluateExpression(list.Expression, env);

            if (rightSide is ErrorObject asError)
            {
                return asError;
            }

            IObject definedVariable = env.DefineVariable(identifier.Name, VariablePayload.Create(type.Type, rightSide));
            if(definedVariable is ErrorObject varDefError)
            {
                return varDefError;
            }

            return NoneObject.Create(list.NodeToken);
        }

        private IObject EvaluateQuitStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            return QuitObject.Create(root.NodeToken);
        }

        private IObject EvaluateSkipStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            return SkipObject.Create(root.NodeToken);
        }

        private IObject EvaluateLoopStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            /*  Handling Loops:
             *  The basic approach is to loop indefinitely until the condition is no longer met.
             *  
             *  Detailed Explanation:
             *  1) Each iteration of the loop has its own environment.
             *  2) After each iteration, the current environment is discarded.
             *  3) Before discarding the environment, we extract the loop index
             *     and transfer it to the new environment. This ensures that the loop index 
             *     is preserved across iterations.
             *  4) Loop evaluation consists of four steps:
             *      a) Evaluate the loop condition.
             *      b) Execute the loop body (code block).
             *      c) Update the loop index.
             *      d) Transfer the updated index to the new environment.
             */

            LoopStatement loop = root as LoopStatement;

            //  Prepare loop env and define indexer.
            ParsingEnvironment loopEnv = env.ExtendEnvironment();
            IObject indexer = EvaluateStatement(loop.IndexerStatement, loopEnv);
            string indexerName = (loop.IndexerStatement as VariableDeclarationStatement).Identifier.Name;

            while (true)
            {
                //  a)  Handling 'Loop' condition evaluation!
                IObject condition = EvaluateExpression(loop.Condition, loopEnv);

                if(condition is ErrorObject isConditionError)
                {
                    return isConditionError;
                }

                //  If evaluation is truthfull then we can execute code!
                if(condition is not BoolObject)
                {
                    //  TODO:
                    return null;
                }
                else
                {
                    if((condition as BoolObject).Value == false)
                    {
                        break;
                    }
                }

                //  b) Evaluate loop code
                IObject blockResult = EvaluateBlockStatement(loop.Body, loopEnv);
                if (blockResult is ErrorObject AsErrorObj)
                {
                    return AsErrorObj;
                }
                else if (blockResult is ReturnObject AsReturnObj)
                {
                    return AsReturnObj;
                }
                else if (blockResult is SkipObject AsSkipObj)
                {
                    /*  If 'Skip' statement is met we should clear current env block,
                     *  evaluate indexer and navigate to the next iteration.
                     */
                }
                else if (blockResult is QuitObject AsQuitObj)
                {
                    /*  If 'Quit' statement is met, just finish evaluating loop.
                     */
                    break;
                }

                //  c)  Evaluate indexer
                IObject indexerOpResult = EvaluateExpression(loop.IndexerOperation, loopEnv);
                if (indexerOpResult is ErrorObject indexerOpAsError)
                {
                    return indexerOpAsError;
                }
                else if (indexerOpResult is NoneObject indexerNone)
                {
                    //  Good
                }
                else
                {
                    return ErrorRuntimeFailedToEvaluate.CreateErrorObject(indexerOpResult.Token, _parser.Lexer.GetLine(indexerOpResult.Token));
                }

                //  d)  Clear previous block data and copy indexer
                indexer = loopEnv.GetVariable(loop.IndexerOperation.NodeToken, indexerName);
                loopEnv.Clear();
                loopEnv.DefineVariable(indexerName, VariablePayload.Create(indexerOpResult.Type, indexer));
            }

            return NoneObject.Create(root.NodeToken);
        }

        private IObject EvaluateSisyphusStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            SisyphusStatement loop = root as SisyphusStatement;

            //  Prepare loop env and define indexer.
            ParsingEnvironment loopEnv = env.ExtendEnvironment();

            while (true)
            {
                IObject blockResult = EvaluateBlockStatement(loop.Body, loopEnv);

                if (blockResult is ErrorObject AsErrorObj)
                {
                    return AsErrorObj;
                }
                else if (blockResult is SkipObject AsSkipObj)
                {
                    /*  If 'Skip' statement is met we should clear current env block,
                     *  evaluate indexer and navigate to the next iteration.
                     */
                }
                else if (blockResult is QuitObject AsQuitObj)
                {
                    return NoneObject.Create(blockResult.Token);
                }

                loopEnv.Clear();
            }
        }

        private IObject EvaluateExpressionStatement(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            ExpressionStatement expStatement = root as ExpressionStatement;
            Expression exp = expStatement.Expression;

            if (exp is IntExpression)
            {
                return EvaluateIntExpression(exp as IntExpression);
            }
            else if (exp is BoolExpresion)
            {
                return EvaluateBoolExpression(exp as BoolExpresion);
            }
            else if (exp is NullExpression)
            {
                return EvaluateNullExpression(exp as NullExpression);
            }
            else if (exp is UnaryExpression)
            {
                return EvaluateUnaryExpression(exp as UnaryExpression, env);
            }
            else if (exp is BinaryExpression)
            {
                return EvaluateBinaryExpression(exp as BinaryExpression, env);
            }
            else if (exp is IfExpression)
            {
                return EvaluateConditionalExpression(exp as IfExpression, env);
            }
            else if (exp is FunctionExpression)
            {
                return EvaluateFunctionExpression(exp as FunctionExpression, env);
            }
            else if (exp is FunctionCallExpression)
            {
                return EvaluateFunctionCallExpression(exp as FunctionCallExpression, env);
            }
            else if (exp is MethodCallExpression)
            {
                return EvaluateMethodCallExpression(exp as MethodCallExpression, env);
            }
            else
            {
                return EvaluateExpression(exp, env);
            }
        }
        #endregion

        #region Expression evaluators
        private IObject EvaluateIntExpression(AbstractSyntaxTreeNode root)
        {
            IntExpression intExp = root as IntExpression;
            if (intExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return IntObject.Create(intExp.NodeToken, intExp.Value);
        }

        private IObject EvaluateFloatExpression(AbstractSyntaxTreeNode root)
        {
            FloatExpression floatExp = root as FloatExpression;
            if (floatExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return FloatObject.Create(floatExp.NodeToken, floatExp.Value);
        }

        private IObject EvaluateStringExpression(AbstractSyntaxTreeNode root)
        {
            StringExpression stringExp = root as StringExpression;
            if (stringExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return StringObject.Create(stringExp.NodeToken, stringExp.Value);
        }

        private IObject EvaluateCharExpression(AbstractSyntaxTreeNode root)
        {
            CharExpression charExp = root as CharExpression;
            if (charExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return CharObject.Create(charExp.NodeToken, charExp.Value);
        }

        private IObject EvaluateBoolExpression(AbstractSyntaxTreeNode root)
        {
            BoolExpresion boolExp = root as BoolExpresion;
            if (boolExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return BoolObject.Create(boolExp.NodeToken, boolExp.Value);
        }

        private IObject EvaluateNullExpression(AbstractSyntaxTreeNode root)
        {
            NullExpression nullExp = root as NullExpression;
            if (nullExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }
            return NullObject.Create(nullExp.NodeToken);
        }

        private IObject EvaluateUnaryExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            UnaryExpression unaryExp = root as UnaryExpression;
            if (unaryExp is null)
            {
                return ErrorRuntimeFailedToEvaluate.CreateErrorObject(root.NodeToken, _parser.Lexer.GetLine(root.NodeToken));
            }

            IObject insideEval = EvaluateExpression(unaryExp.Expression, env);

            Func<IObject, IObject> unaryFunc = _evalFuncsMapper.GetEvaluationFunc(unaryExp.Prefix, insideEval);

            if (unaryFunc == null)
            {
                _errorManager.AppendErrors(_evalFuncsMapper.PopErrors());
                return new ErrorObject(unaryExp.NodeToken, _errorManager.Errors.LastOrDefault());
            }

            return unaryFunc(insideEval);
        }

        private IObject[] EvaluateExpressions(AbstractSyntaxTreeNode root, ParsingEnvironment env, Expression[] expressions)
        {
            IObject[] expressionResults = new IObject[expressions.Length];

            for (int i = 0; i < expressions.Length; i++)
            {
                expressionResults[i] = EvaluateExpression(expressions[i], env);
            }

            return expressionResults;
        }

        private IObject EvaluateBinaryExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            BinaryExpression binExp = root as BinaryExpression;

            IObject leftEval = EvaluateExpression(binExp.Left, env);
            if(leftEval is ErrorObject leftError)
            {
                return leftError;
            }

            IObject rightEval = EvaluateExpression(binExp.Right, env);
            if (rightEval is ErrorObject rightError)
            {
                return rightError;
            }

            //  If expression are 'Return' expression type. We need to unwrap them.
            if (leftEval is ReturnObject lar)
            {
                leftEval = lar.WrappedObject;
            }

            if (rightEval is ReturnObject rar)
            {
                rightEval = rar.WrappedObject;
            }

            Func<IObject, IObject, IObject> binaryFunc = _evalFuncsMapper.GetEvaluationFunc(leftEval, binExp.NodeToken, rightEval);

            if (binaryFunc == null)
            {
                if(binExp.NodeToken.Keyword == TokenType.Assign)
                {
                    return ErrorSemanticalIndexAssignError.CreateErrorObject(leftEval, rightEval, _parser.Lexer.GetLine(leftEval.Token));
                }
                else return ErrorSemanticalUndefBinaryOp.CreateErrorObject(leftEval, binExp.NodeToken, rightEval, _parser.Lexer.GetLine(leftEval.Token));
            }



            IObject result = binaryFunc(leftEval, rightEval);
            if (result is ErrorObject resultError)
            {
                return resultError;
            }

            if (binExp.NodeToken.Keyword == TokenType.Assign)
            {
                if (binExp.Left is IdentifierExpression AsId)
                {
                    env.UpdateVariable((binExp.Left as IdentifierExpression).Name, result);
                }
                else if (binExp.Left is IndexExpression AsIndex)
                {
                    if (leftEval.Type != rightEval.Type)
                    {
                        return ErrorSemanticalCollectionArgTypeMismatch.CreateErrorObject(leftEval.Type, rightEval.Type, rightEval.Token, _parser.Lexer.GetLine(rightEval.Token));
                    }

                    IndexExpression ind = binExp.Left as IndexExpression;

                    string varName = (ind.Object as IdentifierExpression).Name;

                    int index = 0;
                    if(ind.Index is IntExpression asInt)
                    {
                        index = asInt.Value;
                    }
                    else
                    {
                        IObject iterator = env.GetVariable(ind.Index.NodeToken, ind.Index.NodeToken.LiteralValue);

                        if(iterator is not IntObject)
                        {
                            throw new Exception($"Indexing for '{ind.Index}' is not implemented!");
                        }
                    }

                    env.UpdateVariable(varName, index, result);
                }
                else
                {
                    return ErrorSemanticalIndexAssignError.CreateErrorObject(leftEval, rightEval, _parser.Lexer.GetLine(leftEval.Token));
                }
                return NoneObject.Create(binExp.Right.NodeToken);
            }

            return result;
        }

        private IObject EvaluateIdentifierExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            IdentifierExpression identifier = root as IdentifierExpression;
            return env.GetVariable(identifier.NodeToken, identifier.Name);
        }

        private IObject EvaluateVariableDeclarationExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            VariableDefinitionExpression varDef = root as VariableDefinitionExpression;
            return VariableSignatureObject.Create(varDef.NodeToken, varDef.Name.Name, varDef.Type.Type);
        }

        private IObject EvaluateListDeclarationExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            ListArgumentsExpression listArgs = root as ListArgumentsExpression;

            List<IObject> argsValues = new List<IObject>();
            if (listArgs.Arguments.Count > 0)
            {
                foreach (Expression exp in listArgs.Arguments)
                {
                    IObject value = EvaluateExpression(exp, env);

                    if (value.Type != listArgs.Type.Type)
                    {
                        return ErrorSemanticalCollectionArgTypeMismatch.CreateErrorObject(listArgs.Type.Type, value.Type, listArgs.NodeToken, _parser.Lexer.GetLine(listArgs.NodeToken));
                    }

                    argsValues.Add(value);
                }
            }
            else if (listArgs.Arguments.Capacity > 0)
            {
                for (int i = 0; i < listArgs.Capacity; i++)
                {
                    IObject value = ObjectFactory.Create(listArgs.NodeToken, listArgs.Type.Type);

                    argsValues.Add(value);
                }
            }

            return ObjectCollection.Create(listArgs.Type.Type, argsValues.ToArray());
        }

        private IObject EvaluateIndexExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            IndexExpression indexExp = root as IndexExpression;
            IObject obj = EvaluateExpression(indexExp.Object, env);
            if(obj is ErrorObject objAsError)
            {
                return objAsError;
            }

            IObject idx = EvaluateExpression(indexExp.Index, env);
            if(idx is ErrorObject idxAsError)
            {
                return idxAsError;
            }
            
            if (obj is not IIndexable notIndexableError)
            {
                return ErrorSemanticalTypeNotIndexable.CreateErrorObject(obj, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }

            if (idx is not IntObject notIntIndexer)
            {
                return ErrorSemanticalIndexTypeInvalid.CreateErrorObject(idx, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }

            try
            {
                IIndexable collection = obj as IIndexable;
                int index = (idx as IntObject).Value;
                IObject item = collection[index];

                IObject result = null;
                switch (item.LanguageType)
                {
                    case "Int":
                        result = IntObject.Create(indexExp.Object.NodeToken, (item as IntObject).Value);
                        break;
                    case "Char":
                        result = CharObject.Create(indexExp.Object.NodeToken, (item as CharObject).Value);
                        break;
                    case "Bool":
                        result = BoolObject.Create(indexExp.Object.NodeToken, (item as BoolObject).Value);
                        break;
                    case "Float":
                        result = FloatObject.Create(indexExp.Object.NodeToken, (item as FloatObject).Value);
                        break;
                    case "String":
                        result = StringObject.Create(indexExp.Object.NodeToken, (item as StringObject).Value);
                        break;
                    case "None":
                        result = NoneObject.Create(indexExp.Object.NodeToken);
                        break;
                    case "Null":
                        result = NullObject.Create(indexExp.Object.NodeToken);
                        break;
                    default:
                        result = ErrorObject.Create(indexExp.Object.NodeToken, ErrorSyntacticalInvalidTypeParse.Create(indexExp.Object.NodeToken, item.LanguageType, _parser.Lexer.GetLine(indexExp.Object.NodeToken)));
                        break;
                }

                return result;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                return ErrorRuntimeArgOutOfRange.CreateErrorObject(obj as IIndexable, (idx as IntObject).Value, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }
        }

        private IObject EvaluateMethodCallExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            MethodCallExpression callExp = root as MethodCallExpression;

            string methodName = (callExp.Method as IdentifierExpression).Name;

            IObject objectClassObj = EvaluateExpression(callExp.ClassObject, env);
            if(objectClassObj is ErrorObject objClassAsError)
            {
                return objClassAsError;
            }

            IObject[] arguments = EvaluateExpressions(root, env, callExp.Arguments);
            foreach (IObject arg in arguments)
            {
                if (arg is ErrorObject AsError)
                {
                    return AsError;
                }
            }

            IObject[] parameters = new IObject[callExp.Arguments.Length + 1];
            parameters[0] = objectClassObj;
            for(int i = 0; i < arguments.Length; i++)
            {
                parameters[i + 1] = arguments[i];
            }

            FunctionPayload funcSigPayload = env.GetBuildInFunctionSignature(methodName, parameters);
            if(funcSigPayload == null)
            {
                return ErrorSemanticalFunInvalidArgs.CreateErrorObject(methodName, arguments, callExp.Method.NodeToken, _parser.Lexer.GetLine(callExp.Method.NodeToken));
            }

            //  Check whether signature are ok.
            bool signatureOK = funcSigPayload.ArgumentsSignatureMatch(parameters);
            if (signatureOK == false)
            {
                return ErrorSemanticalFunInvalidArgs.CreateErrorObject(methodName, parameters, callExp.NodeToken, _parser.Lexer.GetLine(callExp.NodeToken));
            }

            //  Call evaluate function body with given arguments.
            ParsingEnvironment extendedEnv = env.ExtendEnvironment();

            for (int i = 0; i < parameters.Length; i++)
            {
                ObjectSignature varDef = funcSigPayload.Arguments[i];
                extendedEnv.DefineVariable(varDef.Name, VariablePayload.Create(varDef.Type, parameters[i]));
            }

            IObject result = EvaluateBuildInMethod(extendedEnv, parameters[0], methodName, parameters.Skip(1).ToArray());

            if (result is ReturnObject isReturnExp)
            {
                return isReturnExp.WrappedObject;
            }

            return result;
        }

        private IObject EvaluateFunctionCallExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            FunctionCallExpression callExp = root as FunctionCallExpression;

            string funcName = (callExp.Function as IdentifierExpression).Name;

            //  Evaluate arguments so for example input is '5+1' -> '6'.
            IObject[] arguments = EvaluateExpressions(root, env, callExp.Arguments);

            foreach (IObject arg in arguments)
            {
                if (arg is ErrorObject AsError)
                {
                    return AsError;
                }
            }

            //  Is this build-in function
            bool isBuildInFunc = env.IsBuildInFunction(funcName, arguments);

            //  Maybe its ordinary func
            if (isBuildInFunc == false)
            {
                //  First check whether there is such function defined.
                bool funcExists = env.FunctionExistsRoot(funcName);

                if (funcExists == false)
                {
                    return ErrorRuntimeFuncNotDefined.CreateErrorObject(callExp.Function.NodeToken, _parser.Lexer.GetLine(callExp.Function.NodeToken));
                }
            }

            FunctionPayload funcSigPayload = null;
            if (isBuildInFunc == true)
            {
                funcSigPayload = env.GetBuildInFunctionSignature(funcName, arguments);
            }
            else
            {
                funcSigPayload = env.GetFunctionRoot(funcName);
            }

            if (funcSigPayload == null)
            {
                return ErrorSemanticalFunInvalidArgs.CreateErrorObject(funcName, arguments, callExp.Function.NodeToken, _parser.Lexer.GetLine(callExp.Function.NodeToken));
            }

            //  Check whether signature are ok.
            bool signatureOK = funcSigPayload.ArgumentsSignatureMatch(arguments);
            if (signatureOK == false)
            {
                return ErrorSemanticalFunInvalidArgs.CreateErrorObject(funcName, arguments, callExp.NodeToken, _parser.Lexer.GetLine(callExp.NodeToken));
            }

            //  Call evaluate function body with given arguments.
            ParsingEnvironment extendedEnv = env.ExtendEnvironment();

            for (int i = 0; i < arguments.Length; i++)
            {
                ObjectSignature varDef = funcSigPayload.Arguments[i];
                extendedEnv.DefineVariable(varDef.Name, VariablePayload.Create(varDef.Type, arguments[i]));
            }

            IObject result = null;
            if (isBuildInFunc == false)
            {
                //  Evalute block of function with new environment that has details about passed variables...
                result = EvaluateBlockStatement(funcSigPayload.Body, extendedEnv);
            }
            else
            {
                //  Call build in func
                result = EvaluateBuildInFunction(extendedEnv, funcName, arguments);
            }

            if(result is ReturnObject isReturnExp)
            {
                return isReturnExp.WrappedObject;
            }

            return result;
        }

        private IObject EvaluateBuildInFunction(ParsingEnvironment env, string name, params IObject[] arguments)
        {
            return env.CallBuildInFunc(name, arguments);
        }

        private IObject EvaluateBuildInMethod(ParsingEnvironment env, IObject classObject, string name, params IObject[] arguments)
        {
            return env.CallBuildInMethod(classObject, name, arguments);
        }

        private IObject EvaluateFunctionExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            FunctionExpression funcExp = root as FunctionExpression;

            IObject funcBody = FunctionObject.Create(funcExp.NodeToken, funcExp.Body as BlockStatement, funcExp.Arguments);

            IObject[] argumentSignatures = EvaluateExpressions(root, env, funcExp.Arguments);
            ObjectSignature[] variablePayload = FunctionPayload.GenerateArgumentsSignature(argumentSignatures);
            env.DefineFunction((funcExp.Name as IdentifierExpression).Name, FunctionPayload.Create((funcExp.Type as VariableTypeExpression).Type, variablePayload, funcExp.Body as BlockStatement));

            return NoneObject.Create(funcExp.NodeToken);
        }

        private IObject EvaluateConditionalExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            IfExpression ifExp = root as IfExpression;

            IObject condition = EvaluateExpression(ifExp.ConditionExpression, env);
            if(condition is ErrorObject conditionAsError)
            {
                return conditionAsError;
            }

            bool conditionMeet = ObjectHelpers.IsTruthy(condition);
            if (conditionMeet == true)
            {
                ParsingEnvironment fulfieldBlockEnv = env.ExtendEnvironment();
                return EvaluateBlockStatement(ifExp.FulfielldBlock, fulfieldBlockEnv);
            }
            else if (ifExp.AlternativeBlock != null)
            {
                ParsingEnvironment alternativeBlockEnv = env.ExtendEnvironment();
                return EvaluateBlockStatement(ifExp.AlternativeBlock, alternativeBlockEnv);
            }
            else
            {
                return NoneObject.Create(root.NodeToken);
            }
        }
        #endregion
        #endregion
    }
}
