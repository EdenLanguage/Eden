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
            else if (root is CallExpression)
            {
                return EvaluateCallExpression(root as CallExpression, env);
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

            IObject result = null;

            foreach (Statement statement in block.Statements)
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
                return env.DefineVariable(identifier.Name, VariablePayload.Create(type.Type, rightSide));
            }
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

            return env.DefineVariable(identifier.Name, VariablePayload.Create(type.Type, rightSide));
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
            LoopStatement loop = root as LoopStatement;

            //  Prepare loop env and define indexer.
            ParsingEnvironment loopEnv = env.ExtendEnvironment();
            IObject indexer = EvaluateStatement(loop.IndexerStatement, loopEnv);
            string indexerName = (loop.IndexerStatement as VariableDeclarationStatement).Identifier.Name;

            while ((EvaluateExpression(loop.Condition, loopEnv) as BoolObject).Value)
            {
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
                    return NullObject.Create(AsQuitObj.Token);
                }

                //  Evaluate indexer
                IObject indexerOpResult = EvaluateExpression(loop.IndexerOperation, loopEnv);
                //  Clear previous block data
                loopEnv.Clear();
                loopEnv.DefineVariable(indexerName, VariablePayload.Create(indexerOpResult.Type, indexerOpResult));
            }

            return NullObject.Create(root.NodeToken);
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
            else if (exp is CallExpression)
            {
                return EvaluateCallExpression(exp as CallExpression, env);
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
            IObject rightEval = EvaluateExpression(binExp.Right, env);

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
                return ErrorSemanticalUndefBinaryOp.CreateErrorObject(leftEval, binExp.NodeToken.Keyword, rightEval, _parser.Lexer.GetLine(leftEval.Token));
            }

            IObject result = binaryFunc(leftEval, rightEval);

            if (binExp.NodeToken.Keyword == TokenType.Assign)
            {
                if (binExp.Left is IdentifierExpression AsId && AsId != null)
                {
                    result = env.UpdateVariable((binExp.Left as IdentifierExpression).Name, result);
                }
                else
                {
                    return ErrorSemanticalIllegalAssing.CreateErrorObject(binExp.NodeToken, _parser.Lexer.GetLine(leftEval.Token));
                }
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

                    if (value.Type != listArgs.Type.Type)
                    {
                        return ErrorSemanticalCollectionArgTypeMismatch.CreateErrorObject(listArgs.Type.Type, value.Type, listArgs.NodeToken, _parser.Lexer.GetLine(listArgs.NodeToken));
                    }

                    argsValues.Add(value);
                }
            }

            return ObjectCollection.Create(listArgs.Type.Type, argsValues.ToArray());
        }

        private IObject EvaluateIndexExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            IndexExpression indexExp = root as IndexExpression;
            IObject obj = EvaluateExpression(indexExp.Object, env);
            IObject idx = EvaluateExpression(indexExp.Index, env);
            IObject result = null;

            if (obj is not IIndexable notIndexableError)
            {
                return ErrorSemanticalTypeNotIndexable.CreateErrorObject(obj, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }

            if (idx is not IntObject notIntIndexer || (idx is IntObject asIntObj && asIntObj.Value < 0))
            {
                return ErrorSemanticalIndexTypeInvalid.CreateErrorObject(idx, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }

            try
            {
                result = (obj as IIndexable)[(idx as IntObject).Value];
            }
            catch (ArgumentOutOfRangeException exception)
            {
                return ErrorRuntimeArgOutOfRange.CreateErrorObject(obj as IIndexable, (idx as IntObject).Value, indexExp.NodeToken, _parser.Lexer.GetLine(indexExp.NodeToken));
            }

            return result;
        }

        private IObject EvaluateCallExpression(AbstractSyntaxTreeNode root, ParsingEnvironment env)
        {
            CallExpression callExp = root as CallExpression;

            string funcName = (callExp.Function as IdentifierExpression).Name;

            //  Is this build-in function
            bool isBuildInFunc = env.IsBuildInFunction(funcName);

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
                funcSigPayload = env.GetBuildInFunctionSignature(funcName);
            }
            else
            {
                funcSigPayload = env.GetFunctionRoot(funcName);
            }


            //  Evaluate arguments so for example input is '5+1' -> '6'.
            IObject[] arguments = EvaluateExpressions(root, env, callExp.Arguments);

            foreach (IObject arg in arguments)
            {
                if (arg is ErrorObject AsError)
                {
                    return AsError;
                }
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
