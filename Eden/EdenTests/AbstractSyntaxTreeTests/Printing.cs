using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.AbstractSyntaxTreeTests
{
    public class Printing : FileTester
    {
        [Fact]
        public void TestingFiles()
        {
            string dir = GetTestFilesDirectory();
            string[] testFiles = Directory.GetFiles(dir);

            foreach (string file in testFiles)
            {
                try
                {
                    Parser parser = new Parser();
                    FileStatement block = parser.ParseFile(file);
                    string STR = block.ToString();
                    string AST = block.ToASTFormat();
                    Assert.True(AST != null && AST.Length != 0);
                }
                catch (Exception exception)
                {
                    Assert.Fail($"There was an error at: '{file}'. Message: '{exception.Message}'");
                }
            }
        }

        [Fact]
        public void TestingFile_2()
        {
            string dir = GetTestFilesDirectory();
            string file = Path.Combine(dir, "main2.eden");

            try
            {
                Parser parser = new Parser();
                FileStatement block = parser.ParseFile(file);
                string STR = block.ToString();
                string AST = block.ToASTFormat();
                Assert.True(AST != null && AST.Length != 0);
            }
            catch (Exception exception)
            {
                Assert.Fail($"There was an error at: '{file}'. Message: '{exception.Message}'");
            }
        }

        [Fact]
        public void TestingFile_21()
        {
            string dir = GetTestFilesDirectory();
            string file = Path.Combine(dir, "main21.eden");

            try
            {
                Parser parser = new Parser();
                FileStatement block = parser.ParseFile(file);
                string STR = block.ToString();
                string AST = block.ToASTFormat();
                Assert.True(AST != null && AST.Length != 0);
            }
            catch (Exception exception)
            {
                Assert.Fail($"There was an error at: '{file}'. Message: '{exception.Message}'");
            }
        }

        [Fact]
        public void TestingFile_8()
        {
            string dir = GetTestFilesDirectory();
            string file = Path.Combine(dir, "main8.eden");

            try
            {
                Parser parser = new Parser();
                FileStatement block = parser.ParseFile(file);
                string STR = block.ToString();
                string AST = block.ToASTFormat();
                Assert.True(AST != null && AST.Length != 0);
            }
            catch (Exception exception)
            {
                Assert.Fail($"There was an error at: '{file}'. Message: '{exception.Message}'");
            }
        }
    }
}