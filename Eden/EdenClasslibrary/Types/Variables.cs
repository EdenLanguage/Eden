using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types
{
    public static class Variables
    {
        public static string[] Types
        {
            get
            {
                return new string[] { "Int", "Float", "String", "Bool", "Char" };
            }
        }

        public static bool IsVariableType(string literal)
        {
            return Types.Contains(literal);
        }

        public static string NameFromType(Type type)
        {
            return type.Name.Replace("Object", "");
        }
    }
}
