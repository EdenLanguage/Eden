using EdenClasslibrary.Types;
using System.Text;

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

        public static string GetTestFilesFile(string name)
        {
            return Path.Combine(GetTestFilesDirectory(), name);
        }

        public static string PrintTokenDiff(Token one, Token two)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Token  :    Actual | Expected    ");
            sb.AppendLine($"Keyword:  {one.Keyword} | {two.Keyword}");
            sb.AppendLine($"Literal:  {one.LiteralValue} | {two.LiteralValue}");
            sb.AppendLine($"File   :  {one.Filename} | {two.Filename}");
            sb.AppendLine($"Start  :  {one.Start} | {two.Start}");
            sb.AppendLine($"End    :  {one.End} | {two.End}");
            sb.AppendLine($"Line   :  {one.Line} | {two.Line}");

            return sb.ToString();
        }

        public static string GetInvalidLoopsSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Loops", "Invalid"]);
        }
        public static string GetLexerSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Lexer"]);
        }
        public static string GetValidLoopsSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Loops", "Valid"]);
        }

        public static string GetInvalidLoopsSourceFile(string name)
        {
            return Path.Combine(GetInvalidLoopsSourceDir(), name);
        }

        public static string GetLexerSourceFile(string name)
        {
            return Path.Combine(GetLexerSourceDir(), name);
        }

        public static string GetValidLoopsSourceFile(string name)
        {
            return Path.Combine(GetValidLoopsSourceDir(), name);
        }

        #region Parser
        public static string GetLiteralsSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Literals"]);
        }
        
        public static string GetLiteralsSourceFile(string name)
        {
            return Path.Combine(GetLiteralsSourceDir(), name);
        }

        public static string GetConsoleSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Console"]);
        }

        public static string GetConsoleSourceFile(string name)
        {
            return Path.Combine(GetConsoleSourceDir(), name);
        }

        public static string GetParserSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Parser"]);
        }

        public static string GetParserSourceFile(string name)
        {
            return Path.Combine(GetParserSourceDir(), name);
        }

        public static string GetParserInvalidSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Parser", "Invalid"]);
        }

        public static string GetParserInvalidSourceFile(string name)
        {
            return Path.Combine(GetParserInvalidSourceDir(), name);
        }
        #endregion

        #region Evaluator
        public static string GetEvaluatorSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Evaluator"]);
        }
        public static string GetEvaluatorSourceFile(string name)
        {
            return Path.Combine(GetEvaluatorSourceDir(), name);
        }
        #endregion

        #region Errors
        public static string GetErrorsSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "Errors"]);
        }
        public static string GetErrorsSourceFile(string name)
        {
            return Path.Combine(GetErrorsSourceDir(), name);
        }
        #endregion

        #region Example code
        public static string GetExampleCodeSourceDir()
        {
            return Path.Combine([GetTestFilesDirectory(), "ExampleCode"]);
        }
        public static string GetExampleCodeSourcePath(string name)
        {
            return Path.Combine(GetExampleCodeSourceDir(), name);
        }
        #endregion

        #endregion
    }
}
