using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Loops : FileTester
    {
        [Fact]
        public void FileInput()
        {
            string filename = "main28.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Lexer lexer = new Lexer();

            lexer.LoadFile(executionLocation);

            List<Token> tokens = lexer.Tokenize().ToList();
        }
    }
}
