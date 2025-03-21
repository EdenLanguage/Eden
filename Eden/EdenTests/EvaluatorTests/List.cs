using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;

namespace EdenTests.EvaluatorTests
{
    public class List
    {
        [Fact]
        public void Declaration()
        {
            string[] code = new string[]
            {
                "List Int primes = [2i, 3i, 5i, 7i, 9i];",
                "List Float primes = [2i, 1.5f, \"John\"];",
                "List String primes = [\"John\"];",
                "List Float pies = [1.1f, 2.2f, 3.3f, 4.4f, 5.5f];",
                "List String names = [\"Mark\", \"Adam\", \"Jordan\", \"David\", \"Isaac\"];",
                "List Int temp = (0i);",
                "List Int temp = (1i);",
                "List Float temp = (10i);",
                "List Int temp = [];",
                "List Int temp = [1i];",
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
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(statement);
                if (results[0] == "false")
                {
                    Assert.True(result is ErrorObject);
                }
                else
                {
                    Assert.True(result is not ErrorObject);

                    //  Check type
                    Assert.Equal(results[2], result.Type.Name);

                    //  Check count
                    Assert.Equal(results[1], (result as IObjectCollection).Collection.Count.ToString());

                    //  Check collection's item's types
                    IObjectCollection collection = result as IObjectCollection;

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