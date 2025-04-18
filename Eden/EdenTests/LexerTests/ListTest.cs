﻿using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class List : FileTester
    {
        [Fact]
        public void List_Of_Int()
        {
            string filename = "main22.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();
        }

        [Fact]
        public void ListMethods()
        {
            string executionLocation = GetLexerSourceFile("list.eden");

            Lexer lexer = new Lexer();

            lexer.LoadFile(executionLocation);
            List<Token> tokens = lexer.Tokenize().ToList();

            Console.WriteLine(tokens.Count);
        }
    }
}
