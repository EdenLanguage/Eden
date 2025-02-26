using EdenClasslibrary.Parser;

namespace EdenTests.ParserTests
{
    public class VariableStatementTest
    {
        [Fact]
        public void Statement_1()
        {
            //string code = "var int zmienna = 10;";

            //Parser parser = new Parser();
            //parser.Parse(code);

            //Assert.Equal(parser.AST.Statements.Length, 1);
            //Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void Statement_2()
        {
            //string code =
            //    "var int zmienna = 10;" +
            //    "var int counter = 0;" +
            //    "var int minuser = 20;";

            //Parser parser = new Parser();
            //parser.Parse(code);

            //Assert.Equal(parser.AST.Statements.Length, 3);
            //Assert.Equal(parser.Errors.Length, 0);
        }


        [Fact]
        public void Statement_3()
        {
            //string code =
            //    "var bool flag = false;";
            //Parser parser = new Parser();
            //parser.Parse(code);

            //string json = parser.Statements.Serialize();

            //Assert.Equal(parser.AST.Statements.Length, 1);
            //Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void Statement_4()
        {
            //string code =
            //    "var zmienna = 10;" +
            //    "int counter = 0;" +
            //    "var int minuser = ;" +
            //    "var int counter = 120;";

            //Parser parser = new Parser();
            //parser.Parse(code);

            //Assert.Equal(parser.AST.Statements.Length, 1);
            //Assert.Equal(parser.Errors.Length, 3);
        }

        [Fact]
        public void Statement_5()
        {
            //string code =
            //    "var int zmienna = 10;" +
            //    "int counter = 0";

            //Parser parser = new Parser();
            //parser.Parse(code);

            //Assert.Equal(parser.AST.Statements.Length, 1);
            //Assert.Equal(parser.Errors.Length, 1);
        }
    }
}
