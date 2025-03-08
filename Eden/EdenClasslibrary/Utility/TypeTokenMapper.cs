using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Utility
{
    public static class TypeTokenMapper
    {
        public static Type TypeFromToken(Token token)
        {
            Type result = typeof(NullObject);
            switch (token.Keyword)
            {
                case TokenType.Int:
                    result = typeof(IntObject);
                    break;
                case TokenType.Float:
                    result = typeof(FloatObject);
                    break;
                case TokenType.Bool:
                    result = typeof(BoolObject);
                    break;
                case TokenType.Null:
                    result = typeof(NullObject);
                    break;
                case TokenType.VariableType:
                    result = TypeFromString(token.LiteralValue);
                    break;
                default:
                    throw new Exception("Unhandled type!");
            }
            return result;
        }

        public static Type TypeFromString(string type)
        {
            Type result = typeof(NullObject);
            switch (type)
            {
                case "Int":
                    result = typeof(IntObject);
                    break;
                case "Float":
                    result = typeof(FloatObject);
                    break;
                case "Bool":
                    result = typeof(BoolObject);
                    break;
                case "Null":
                    result = typeof(NullObject);
                    break;
                case "String":
                    result = typeof(StringObject);
                    break;
                default:
                    throw new Exception("Unhandled type!");
            }
            return result;
        }
    }
}
