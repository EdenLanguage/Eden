using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using System.ComponentModel.DataAnnotations;

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

        public IObject Evaluate(ASTreeNode root)
        {
            if(root is Expression)
            {
                return EvaluateExpression(root as Expression);
            }
            else if(root is Statement)
            {
                IObject evaluatedStmt = EvaluateStatement(root as Statement);
                if (evaluatedStmt is ReturnObject retObj) return retObj.WrappedObject;
                return evaluatedStmt;
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }
        #region Helper methods
        private IObject EvaluateExpression(ASTreeNode root)
        {
            if(root is IntExpression)
            {
                return EvaluateIntExpression(root as IntExpression);
            }
            else if(root is BinaryExpression)
            {
                return EvaluateBinaryExpression(root as BinaryExpression);
            }
            else if (root is UnaryExpression)
            {
                return EvaluateUnaryExpression(root as UnaryExpression);
            }
            else if (root is BoolExpresion)
            {
                return EvaluateBoolExpression(root as BoolExpresion);
            }
            else if(root is ExpressionStatement)
            {
                return EvaluateExpressionStatement(root as ExpressionStatement);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }

        private IObject EvaluateStatement(ASTreeNode root)
        {
            if (root is FileStatement)
            {
                return EvaluateFileStatement(root as FileStatement);
            }
            else if(root is BlockStatement)
            {
                return EvaluateBlockStatement(root as BlockStatement);
            }
            else if(root is ExpressionStatement)
            {
                return EvaluateExpressionStatement(root as ExpressionStatement);
            }
            else if (root is ReturnStatement)
            {
                return EvaluateReturnStatement(root as ReturnStatement);
            }
            else
            {
                return EvaluateNullExpression(root);
            }
        }
        #region Statements evaluators
        private IObject EvaluateFileStatement(ASTreeNode root)
        {
            BlockStatement block = (root as FileStatement).Block;

            return EvaluateBlockStatement(block);
        }

        private IObject EvaluateBlockStatement(ASTreeNode root)
        {
            BlockStatement block = root as BlockStatement;
            IObject result = null;

            foreach(Statement statement in block.Statements)
            {
                result = EvaluateStatement(statement);

                //  We don't need to evaluate further.
                if(result is ReturnObject)
                {
                    return (result as ReturnObject).WrappedObject;
                }
            }

            return result;
        }

        private IObject EvaluateReturnStatement(ASTreeNode root)
        {
            ReturnStatement retStmt = root as ReturnStatement;

            IObject returnExp = EvaluateExpression(retStmt.Expression);

            return new ReturnObject(returnExp);
        }

        private IObject EvaluateExpressionStatement(ASTreeNode root)
        {
            ExpressionStatement expStatement = root as ExpressionStatement;
            Expression exp = expStatement.Expression;

            if(exp is IntExpression)
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
                return EvaluateUnaryExpression(exp as UnaryExpression);
            }
            else if (exp is BinaryExpression)
            {
                return EvaluateBinaryExpression(exp as BinaryExpression);
            }
            else if (exp is IfExpression)
            {
                return EvaluateConditionalExpression(exp as IfExpression);
            }
            else
            {
                return EvaluateExpression(exp);
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

        private IObject EvaluateUnaryExpression(ASTreeNode root)
        {
            UnaryExpression unaryExp = root as UnaryExpression;
            if (unaryExp is null)
            {
                // TODO - handle error!
            }

            IObject insideEval = EvaluateExpression(unaryExp.Expression);

            Func<IObject, IObject> unaryFunc = _evalFuncsMapper.GetEvaluationFunc(unaryExp.Prefix, insideEval);

            if (unaryFunc == null)
            {
                _errorManager.AppendErrors(_evalFuncsMapper.PopErrors());
                return new ErrorObject(_errorManager.Errors.LastOrDefault());
            }

            return unaryFunc(insideEval);
        }

        private IObject EvaluateBinaryExpression(ASTreeNode root)
        {
            BinaryExpression binExp = root as BinaryExpression;
            if (binExp is null)
            {
            }

            IObject leftEval = EvaluateExpression(binExp.Left);
            IObject rightEval = EvaluateExpression(binExp.Right);

            Func<IObject, IObject, IObject> binaryFunc = _evalFuncsMapper.GetEvaluationFunc(leftEval, binExp.NodeToken, rightEval);

            if(binaryFunc == null)
            {
                _errorManager.AppendErrors(_evalFuncsMapper.PopErrors());
                return new ErrorObject(_errorManager.Errors.LastOrDefault());
            }

            return binaryFunc(leftEval, rightEval);
            //return IObjectFactory.ResolveBinaryObject(leftEval, binExp.NodeToken.Keyword, rightEval);
        }

        private IObject EvaluateConditionalExpression(ASTreeNode root)
        {
            IfExpression ifExp = root as IfExpression;
            if (ifExp is null)
            {
            }
            IObject condition = EvaluateExpression(ifExp.ConditionExpression);

            bool conditionMeet = ObjectHelpers.IsTruthy(condition);
            if(conditionMeet == true)
            {
                return EvaluateBlockStatement(ifExp.FulfielldBlock);
            }
            else if(ifExp.AlternativeBlock != null)
            {
                return EvaluateBlockStatement(ifExp.AlternativeBlock);
            }
            else
            {
                return NullObject.Create();
            }
        }
        #endregion
        #endregion
    }
}
