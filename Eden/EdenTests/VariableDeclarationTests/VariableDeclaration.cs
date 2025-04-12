using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.VariableDeclarationTests
{
    public class VariableDeclaration : FileTester
    {
        [Fact]
        public void IntDeclarationTypes()
        {
            string[] testset =
            [
                GetVariableDeclarationIntSourceFile("int1.eden"),
                GetVariableDeclarationIntSourceFile("int2.eden"),
                GetVariableDeclarationIntSourceFile("int3.eden"),
                GetVariableDeclarationIntSourceFile("int4.eden"),
            ];

            Type[] outputSet =
            [
                typeof(IntObject),
                typeof(IntObject),
                typeof(IntObject),
                typeof(IntObject),
            ];

            for(int i = 0; i < testset.Length; i++)
            {
                string input = testset[i];
                Type expected = outputSet[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (result.Type != expected)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{Path.GetFileName(input)}' failed!");
                    }
                }
            }
        }

        [Fact]
        public void FloatDeclarationTypes()
        {
            string[] testset =
            [
                GetVariableDeclarationFloatSourceFile("float1.eden"),
                GetVariableDeclarationFloatSourceFile("float2.eden"),
                GetVariableDeclarationFloatSourceFile("float3.eden"),
                GetVariableDeclarationFloatSourceFile("float4.eden"),
            ];

            Type[] outputSet =
            [
                typeof(FloatObject),
                typeof(FloatObject),
                typeof(FloatObject),
                typeof(FloatObject),
            ];

            for (int i = 0; i < testset.Length; i++)
            {
                string input = testset[i];
                Type expected = outputSet[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (result.Type != expected)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{Path.GetFileName(input)}' failed!");
                    }
                }
            }
        }

        [Fact]
        public void CharDeclarationTypes()
        {
            string[] testset =
            [
                GetVariableDeclarationCharSourceFile("char1.eden"),
                GetVariableDeclarationCharSourceFile("char2.eden"),
            ];

            Type[] outputSet =
            [
                typeof(CharObject),
                typeof(CharObject),
            ];

            for (int i = 0; i < testset.Length; i++)
            {
                string input = testset[i];
                Type expected = outputSet[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (result.Type != expected)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{Path.GetFileName(input)}' failed!");
                    }
                }
            }
        }
    }
}
