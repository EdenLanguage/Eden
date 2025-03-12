using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.EnvironmentTypes;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace EdenClasslibrary.Types
{
    public class Evaluator
    {
        private EvaluationMapper _evalFuncsMapper;
        private ErrorsManager _errorManager;
        public Evaluator()
        {
            _evalFuncsMapper = new EvaluationMapper();
            _errorManager = new ErrorsManager();
        }

        public IObject Evaluate(ASTreeNode root, Environment env)
        {
            if (root is Expression)
            {
                return EvaluateExpression(root as Expression, env);
            }
            else if (root is Statement)
            {
                return EvaluateStatement(root as Statement, env);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }
        #region Helper methods
        private IObject EvaluateExpression(ASTreeNode root, Environment env)
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

        private IObject EvaluateStatement(ASTreeNode root, Environment env)
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
            else if (root is InvalidStatement)
            {
                return EvaluateInvalidStatement(root as InvalidStatement, env);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }
        #region Statements evaluators
        private IObject EvaluateFileStatement(ASTreeNode root, Environment env)
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

        private IObject EvaluateBlockStatement(ASTreeNode root, Environment env)
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
                else if (result is ErrorObject AsErrorObj)
                {
                    return AsErrorObj;
                }
            }

            return result;
        }

        private IObject EvaluateReturnStatement(ASTreeNode root, Environment env)
        {
            ReturnStatement retStmt = root as ReturnStatement;

            IObject returnExp = EvaluateExpression(retStmt.Expression, env);

            return new ReturnObject(returnExp);
        }

        private IObject EvaluateInvalidStatement(ASTreeNode root, Environment env)
        {
            return ErrorInvalidStatement.CreateErrorObject();
        }

        /// <summary>
        /// Handles evaluating variable declaration. Example: 'Var Int counter = 10';
        /// It has to check whether variable with such name was previously created.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private IObject EvaluateVariableDeclarationStatement(ASTreeNode root, Environment env)
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

            return env.DefineVariable(identifier.Name, VariablePayload.Create(type.Type, rightSide));
        }

        private IObject EvaluateListDeclarationStatement(ASTreeNode root, Environment env)
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

        private IObject EvaluateExpressionStatement(ASTreeNode root, Environment env)
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
        private IObject EvaluateIntExpression(ASTreeNode root)
        {
            IntExpression intExp = root as IntExpression;
            if (intExp is null)
            {
                // TODO - handle error!
            }
            return IntObject.Create(intExp.Value);
        }

        private IObject EvaluateFloatExpression(ASTreeNode root)
        {
            FloatExpression floatExp = root as FloatExpression;
            if (floatExp is null)
            {
                // TODO - handle error!
            }
            return FloatObject.Create(floatExp.Value);
        }

        private IObject EvaluateStringExpression(ASTreeNode root)
        {
            StringExpression stringExp = root as StringExpression;
            if (stringExp is null)
            {
                // TODO - handle error!
            }
            return StringObject.Create(stringExp.Value);
        }

        private IObject EvaluateCharExpression(ASTreeNode root)
        {
            CharExpression charExp = root as CharExpression;
            if (charExp is null)
            {
                // TODO - handle error!
            }
            return CharObject.Create(charExp.Value);
        }

        private IObject EvaluateBoolExpression(ASTreeNode root)
        {
            BoolExpresion boolExp = root as BoolExpresion;
            if (boolExp is null)
            {
                // TODO - handle error!
            }
            return BoolObject.Create(boolExp.Value);
        }

        private IObject EvaluateNullExpression(ASTreeNode root)
        {
            NullExpression nullExp = root as NullExpression;
            if (nullExp is null)
            {
                // TODO - handle error!
            }
            return NullObject.Create();
        }

        private IObject EvaluateUnaryExpression(ASTreeNode root, Environment env)
        {
            UnaryExpression unaryExp = root as UnaryExpression;
            if (unaryExp is null)
            {
                // TODO - handle error!
            }

            IObject insideEval = EvaluateExpression(unaryExp.Expression, env);

            Func<IObject, IObject> unaryFunc = _evalFuncsMapper.GetEvaluationFunc(unaryExp.Prefix, insideEval);

            if (unaryFunc == null)
            {
                _errorManager.AppendErrors(_evalFuncsMapper.PopErrors());
                return new ErrorObject(_errorManager.Errors.LastOrDefault());
            }

            return unaryFunc(insideEval);
        }

        private IObject[] EvaluateExpressions(ASTreeNode root, Environment env, Expression[] expressions)
        {
            IObject[] expressionResults = new IObject[expressions.Length];

            for (int i = 0; i < expressions.Length; i++)
            {
                expressionResults[i] = EvaluateExpression(expressions[i], env);
            }

            return expressionResults;
        }

        private IObject EvaluateBinaryExpression(ASTreeNode root, Environment env)
        {
            BinaryExpression binExp = root as BinaryExpression;
            if (binExp is null)
            {
            }

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
                _errorManager.AppendErrors(_evalFuncsMapper.PopErrors());
                return new ErrorObject(_errorManager.Errors.LastOrDefault());
            }

            return binaryFunc(leftEval, rightEval);
        }

        private IObject EvaluateIdentifierExpression(ASTreeNode root, Environment env)
        {
            IdentifierExpression identifier = root as IdentifierExpression;
            return env.GetVariableValue(identifier.Name);
        }

        private IObject EvaluateVariableDeclarationExpression(ASTreeNode root, Environment env)
        {
            VariableDefinitionExpression varDef = root as VariableDefinitionExpression;
            return VariableSignatureObject.Create(varDef.Name.Name, varDef.Type.Type);
        }

        private IObject EvaluateListDeclarationExpression(ASTreeNode root, Environment env)
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
                        return ErrorCollectionArgumentTypeMismatch.CreateErrorObject(listArgs.Type.Type, value.Type);
                    }

                    argsValues.Add(value);
                }
            }
            else if (listArgs.Arguments.Capacity > 0)
            {
                for (int i = 0; i < listArgs.Capacity; i++)
                {
                    IObject value = ObjectFactory.Create(listArgs.Type.Type);

                    if (value.Type != listArgs.Type.Type)
                    {
                        return ErrorCollectionArgumentTypeMismatch.CreateErrorObject(listArgs.Type.Type, value.Type);
                    }

                    argsValues.Add(value);
                }
            }

            return ObjectCollection.Create(listArgs.Type.Type, argsValues.ToArray());
        }

        private IObject EvaluateIndexExpression(ASTreeNode root, Environment env)
        {
            IndexExpression indexExp = root as IndexExpression;
            IObject obj = EvaluateExpression(indexExp.Object, env);
            IObject idx = EvaluateExpression(indexExp.Index, env);
            IObject result = null;

            if (obj is not IIndexable notIndexableError)
            {
                return ErrorObjectTypeIsNotIndexable.CreateErrorObject(obj);
            }

            if (idx is not IntObject notIntIndexer || (idx is IntObject asIntObj && asIntObj.Value < 0))
            {
                return ErrorIndexNotValid.CreateErrorObject(idx);
            }

            try
            {
                result = (obj as IIndexable)[(idx as IntObject).Value];
            }
            catch (ArgumentOutOfRangeException exception)
            {
                return ErrorArgumentOutOfRange.CreateErrorObject(obj as IIndexable, idx as IntObject);
            }

            return result;
        }

        private IObject EvaluateCallExpression(ASTreeNode root, Environment env)
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
                    return ErrorFunctionNotDefined.CreateErrorObject();
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
                return ErrorFunctionInvalidArguments.CreateErrorObject();
            }

            //  Call evaluate function body with given arguments.
            Environment extendedEnv = env.ExtendEnvironment();

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

            return result;
        }

        private IObject EvaluateBuildInFunction(Environment env, string name, params IObject[] arguments)
        {
            return env.CallBuildInFunc(name, arguments);
        }

        private IObject EvaluateFunctionExpression(ASTreeNode root, Environment env)
        {
            FunctionExpression funcExp = root as FunctionExpression;

            IObject funcBody = FunctionObject.Create(funcExp.Body, funcExp.Arguments);

            IObject[] argumentSignatures = EvaluateExpressions(root, env, funcExp.Arguments);
            ObjectSignature[] variablePayload = FunctionPayload.GenerateArgumentsSignature(argumentSignatures);
            env.DefineFunction(funcExp.Name.Name, FunctionPayload.Create(funcExp.Type.Type, variablePayload, funcExp.Body));

            return funcBody;
        }

        private IObject EvaluateConditionalExpression(ASTreeNode root, Environment env)
        {
            IfExpression ifExp = root as IfExpression;
            if (ifExp is null)
            {
            }
            IObject condition = EvaluateExpression(ifExp.ConditionExpression, env);

            bool conditionMeet = ObjectHelpers.IsTruthy(condition);
            if (conditionMeet == true)
            {
                return EvaluateBlockStatement(ifExp.FulfielldBlock, env);
            }
            else if (ifExp.AlternativeBlock != null)
            {
                return EvaluateBlockStatement(ifExp.AlternativeBlock, env);
            }
            else
            {
                //  TODO: In case main21.eden -> Fibonacci example, after condition is not meet. Code should execute recirsive call of n-2 + n-1.
                return NullObject.Create();
            }
        }
        #endregion
        #endregion
    }
}
