namespace EdenClasslibrary.Utility
{
    public static class Common
    {
        public static bool ColorfulPrinting { get; set; } = false;
        public static string IndentCreator(int count)
        {
            string indents = string.Empty;
            for (int i = 0; i < count; i++)
            {
                indents += "   "; // \t before
            }
            return indents;
        }
    }
}