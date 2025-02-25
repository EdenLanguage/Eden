namespace EdenClasslibrary.Types
{
    public static class Variables
    {
        public static string[] Types
        {
            get
            {
                return new string[] { "Int", "Float", "String", "Bool" };
            }
        }

        public static bool IsVariableType(string literal)
        {
            return Types.Contains(literal);
        }
    }
}
