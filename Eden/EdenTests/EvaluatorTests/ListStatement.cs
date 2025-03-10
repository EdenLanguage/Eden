using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class ListStatement
    {
        [Fact]
        public void Declaration()
        {
            string[] code = new string[]
            {
                "List Int primes = [2, 3, 5, 7, 9];",
                "List Float primes = [2, 1.5, \"John\"];",
                "List String primes = [\"John\"];",
                "List Float pies = [1.1, 2.2, 3.3, 4.4, 5.5];",
                "List String names = [\"Mark\", \"Adam\", \"Jordan\", \"David\", \"Isaac\"];",
                "List Int temp = (0);",
                "List Int temp = (1);",
                "List Float temp = (10);",
                "List Int temp = [];",
                "List Int temp = [1];",
            };

            string[][] isOutcomeValid = new string[][]
            {
                ["true", "5", "IntObject", "true", "0"],
                ["false", "3", "FloatObject", "true", "0"],
                ["true", "1", "StringObject", "true", ""],
                ["true", "5", "FloatObject", "true", "0"],
                ["true", "5", "StringObject", "true", ""],
                ["true", "0", "IntObject", "false", "0"],
                ["true", "1", "IntObject", "false", "0"],
                ["true", "10", "FloatObject", "false", "0"],
                ["true", "0", "IntObject", "true", "0"],
                ["true", "1", "IntObject", "true", "0"],
            };

            string[] results = new string[] { };
            string statement = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                statement = code[i];
                results = isOutcomeValid[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                //  Parsing
                FileStatement ast = parser.Parse(statement);
                string AST = ast.ToASTFormat();
                string STR = ast.ToString();
                
                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);
                
                ListDeclarationStatement expressionStmnt = parser.Program.Block.Statements[0] as ListDeclarationStatement;
                Assert.NotNull(expressionStmnt);

                //  Evaluation
                IObject evalResult = evaluator.Evaluate(ast, env);
                if (results[0] == "false")
                {
                    Assert.True(evalResult is ErrorObject);
                }
                else
                {
                    Assert.True(evalResult is not ErrorObject);

                    //  Check type
                    Assert.Equal(results[2], evalResult.Type.Name);

                    //  Check count
                    Assert.Equal(results[1], (evalResult as IObjectCollection).Collection.Count.ToString());

                    //  Check collection's item's types
                    IObjectCollection collection = evalResult as IObjectCollection;

                    if (results[3] == "true")
                    {
                        for(int j = 0; j < collection.Collection.Count; j++)
                        {
                            IObject item = collection.Collection[j];

                            Assert.Equal(results[2], item.Type.Name);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < collection.Collection.Count; j++)
                        {
                            IObject item = collection.Collection[j];

                            if(collection is ObjectCollection asIntColl)
                            {
                                Type actualItemType = collection.Type;

                                if(actualItemType.Name == "IntObject")
                                {
                                    Assert.Equal(results[4], (item as IntObject).Value.ToString());
                                }
                                else if (actualItemType.Name == "StringObject")
                                {
                                    Assert.Equal(results[4], (item as StringObject).Value);
                                }
                                else if (actualItemType.Name == "FloatObject")
                                {
                                    Assert.Equal(results[4], (item as FloatObject).Value.ToString());
                                }
                                else
                                {
                                    Assert.Fail("TODO: Type not supported!");
                                }
                            }
                            else
                            {
                                Assert.Fail("Not defined collection!");
                            }
                        }
                    }
                }
            }
        }
    }
}