namespace EdenTests.Utility
{
    public class FileTester
    {
        #region Helper methods
        public static string GetTestFilesDirectory()
        {
            string executionLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string testsDir = executionLocation.Substring(0, executionLocation.IndexOf("EdenTests"));
            string sourceDir = Path.Combine(new string[] { testsDir, "EdenTests", "Source" });
            return sourceDir;
        }
        #endregion
    }
}
