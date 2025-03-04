namespace EdenClasslibrary.Types.LanguageTypes
{
    public static class IObjectFactory
    {
        public static IObject ResolveUnaryObject(TokenType prefix, IObject evaluatedObject)
        {
            if (evaluatedObject is IntObject)
            {
                IntObject intObj = evaluatedObject as IntObject;
                switch (prefix)
                {
                    case TokenType.QuenstionMark:
                        return new BoolObject(intObj.Value == 0);
                    case TokenType.Minus:
                    case TokenType.ExclemationMark:
                        return new IntObject(-1 * intObj.Value);
                    case TokenType.Tilde:
                        return new IntObject(~intObj.Value);
                    default:
                        return new NullObject(null);
                }
            }
            else if (evaluatedObject is BoolObject)
            {
                BoolObject boolObj = evaluatedObject as BoolObject;
                switch (prefix)
                {
                    case TokenType.QuenstionMark:
                        return new BoolObject(boolObj.Value == false);
                    case TokenType.ExclemationMark:
                    case TokenType.Tilde:
                        return new BoolObject(!boolObj.Value);
                    default:
                        return new NullObject(null);
                }
            }
            else
            {
                return new NullObject(null);
            }
        }
        public static IObject ResolveBinaryObject(IObject left, TokenType operation, IObject right)
        {
            bool areSameType = left.IsSameType(right);
            if(areSameType == false)
            {
                //  TODO: Handle that properly!!!
                return new NullObject(null);
            }

            if (left is IntObject)
            {
                IntObject leftObj = left as IntObject;
                IntObject rightObj = right as IntObject;
                switch (operation)
                {
                    case TokenType.Minus:
                        return new IntObject(leftObj.Value - rightObj.Value);
                    case TokenType.Plus:
                        return new IntObject(leftObj.Value + rightObj.Value);
                    case TokenType.Star:
                        return new IntObject(leftObj.Value * rightObj.Value);
                    case TokenType.Slash:
                        return new IntObject(leftObj.Value / rightObj.Value);
                    case TokenType.Equal:
                        return new BoolObject(leftObj.Value == rightObj.Value);
                    case TokenType.Inequal:
                        return new BoolObject(leftObj.Value != rightObj.Value);
                    case TokenType.LeftArrow:
                        return new BoolObject(leftObj.Value < rightObj.Value);
                    case TokenType.RightArrow:
                        return new BoolObject(leftObj.Value > rightObj.Value);
                    case TokenType.LesserOrEqual:
                        return new BoolObject(leftObj.Value <= rightObj.Value);
                    case TokenType.GreaterOrEqual:
                        return new BoolObject(leftObj.Value >= rightObj.Value);
                    default:
                        return new NullObject(null);
                }
            }
            else if (left is BoolObject)
            {
                BoolObject leftObj = left as BoolObject;
                BoolObject rightObj = right as BoolObject;
                switch (operation)
                {
                    case TokenType.Equal:
                        return new BoolObject(leftObj.Value == rightObj.Value);
                    case TokenType.Inequal:
                        return new BoolObject(leftObj.Value != rightObj.Value);
                    default:
                        return new NullObject(null);
                }
            }
            else
            {
                return new NullObject(null);
            }
        }
    }
}
