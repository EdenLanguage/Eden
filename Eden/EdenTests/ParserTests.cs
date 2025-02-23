using EdenClasslibrary.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenTests
{
    public class ParserTests
    {
        [Fact]
        public void ParserTest_InitialTest()
        {
            string code = "var int zmienna = 10;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_ThreeStatements()
        {
            string code = 
                "var int zmienna = 10;" +
                "var int counter = 0;" +
                "var int minuser = 20;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 3);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_InvalidStatement_1()
        {
            string code =
                "var zmienna = 10;" +
                "int counter = 0;" +
                "var int minuser = ;" +
                "var int counter = 120;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 1);
            Assert.Equal(parser.Errors.Length, 3);
        }

        [Fact]
        public void ParserTest_InvalidStatement_2()
        {
            string code =
                "var int zmienna = 10;" +
                "int counter = 0";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 1);
            Assert.Equal(parser.Errors.Length, 1);
        }

        [Fact]
        public void ParserTest_BoolStatement_1()
        {
            string code =
                "var bool flag = false;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_Return_1()
        {
            string code =
                "return 5;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.ASTRoot.Nodes.Count, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }
    }
}
