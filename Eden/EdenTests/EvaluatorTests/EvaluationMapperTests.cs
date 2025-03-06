using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenTests.EvaluatorTests
{
    public class EvaluationMapperTests
    {
        [Fact]
        public void ValidOperationTest()
        {
            EvaluationMapper mapper = new EvaluationMapper();

            IObject left = IntObject.Create(10);
            IObject right = IntObject.Create(10);

            IObject[] testSet = new IObject[]
            {
                //  Int
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Plus}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(IntObject.Create(1), new Token() { Keyword = TokenType.Plus}, FloatObject.Create(3.14f))
                    .Invoke(IntObject.Create(1), FloatObject.Create(3.14f)),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Minus}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Star}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Slash}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Equal}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.Inequal}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.LeftArrow}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.RightArrow}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.LesserOrEqual}, right)
                    .Invoke(left, right),
                mapper.GetEvaluationFunc(left, new Token() { Keyword = TokenType.GreaterOrEqual}, right)
                    .Invoke(left, right),

                //  Float
                mapper.GetEvaluationFunc(FloatObject.Create(3.14f), new Token() { Keyword = TokenType.Plus}, FloatObject.Create(3.14f))
                    .Invoke(FloatObject.Create(3.14f), FloatObject.Create(3.14f)),
                mapper.GetEvaluationFunc(FloatObject.Create(3.14f), new Token() { Keyword = TokenType.Plus}, IntObject.Create(1))
                    .Invoke(FloatObject.Create(3.14f), FloatObject.Create(1)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Plus}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Minus}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Star}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Slash}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Equal}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.Inequal}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.LeftArrow}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.RightArrow}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.LesserOrEqual}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),
                mapper.GetEvaluationFunc(FloatObject.Create(1.1f), new Token() { Keyword = TokenType.GreaterOrEqual}, FloatObject.Create(1.1f))
                    .Invoke(FloatObject.Create(1.1f), FloatObject.Create(1.1f)),

                //  Float with Int
                mapper.GetEvaluationFunc(FloatObject.Create(3.14f), new Token() { Keyword = TokenType.Minus}, IntObject.Create(1))
                    .Invoke(FloatObject.Create(3.14f), FloatObject.Create(1)),
                mapper.GetEvaluationFunc(IntObject.Create(10), new Token() { Keyword = TokenType.Minus}, FloatObject.Create(2.5f))
                    .Invoke(IntObject.Create(10), FloatObject.Create(2.5f)),

                //  Bool
                mapper.GetEvaluationFunc(BoolObject.Create(true), new Token() { Keyword = TokenType.Equal}, BoolObject.Create(false))
                    .Invoke(BoolObject.Create(true), BoolObject.Create(false)),
                mapper.GetEvaluationFunc(BoolObject.Create(true), new Token() { Keyword = TokenType.Inequal}, BoolObject.Create(false))
                    .Invoke(BoolObject.Create(true), BoolObject.Create(false)),
                
                //  Null
                mapper.GetEvaluationFunc(NullObject.Create(), new Token() { Keyword = TokenType.Equal}, NullObject.Create())
                    .Invoke(NullObject.Create(), NullObject.Create()),
                mapper.GetEvaluationFunc(NullObject.Create(), new Token() { Keyword = TokenType.Inequal}, NullObject.Create())
                    .Invoke(NullObject.Create(), NullObject.Create()),
            };

            IObject[] result = new IObject[]
            {
                //  Int
                IntObject.Create(20),
                FloatObject.Create(4.14000034f),
                IntObject.Create(0),
                IntObject.Create(100),
                IntObject.Create(1),
                BoolObject.Create(true),
                BoolObject.Create(false),
                BoolObject.Create(false),
                BoolObject.Create(false),
                BoolObject.Create(true),
                BoolObject.Create(true),

                //  Float
                FloatObject.Create(6.28f),
                FloatObject.Create(4.14000034f),
                FloatObject.Create(2.2f),
                FloatObject.Create(0f),
                FloatObject.Create(1.21f),
                FloatObject.Create(1f),
                BoolObject.Create(true),
                BoolObject.Create(false),
                BoolObject.Create(false),
                BoolObject.Create(false),
                BoolObject.Create(true),
                BoolObject.Create(true),

                //  Float with Int
                FloatObject.Create(2.14f),
                FloatObject.Create(7.50f),

                //  Bool
                BoolObject.Create(false),
                BoolObject.Create(true),

                //  Null
                BoolObject.Create(true),
                BoolObject.Create(false),
            };

            for(int i = 0; i < testSet.Length; i++)
            {
                Assert.Equal(result[i].Type, testSet[i].Type);
                Assert.Equal(result[i].AsString(), testSet[i].AsString());
            }

        }

        [Fact]
        public void EdgeCaseOperationsTest()
        {
            EvaluationMapper mapper = new EvaluationMapper();

            IObject exampleInt = IntObject.Create(1);
            IObject exampleFloat = FloatObject.Create(1f);
            IObject exampleNull = NullObject.Create();
            IObject exampleBool = BoolObject.Create(true);

            IObject[] testSet = new IObject[]
            {
                //  Int(10) / Int(3) = Int(3)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Slash}, exampleInt)
                    .Invoke(IntObject.Create(10), IntObject.Create(3)),

                //  Int(10) / Float(3) = Float(3.33333325)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Slash}, exampleFloat)
                    .Invoke(IntObject.Create(10), FloatObject.Create(3)),

                //  Int(3) > Float(3) = Bool(False)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.RightArrow}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3)),

                //  Int(3) == Float(3) = Bool(True)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Equal}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3)),

                //  Int(3) <= Float(3) = Bool(True)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LesserOrEqual}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3)),

                //  Int(3) <= Float(3.1) = Bool(False)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LesserOrEqual}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3.1f)),

                //  Int(3) != Float(3) = Bool(False)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Inequal}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3)),

                //  Float(3) / Float(3) = Float(1)
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Slash}, exampleFloat)
                    .Invoke(FloatObject.Create(3), FloatObject.Create(3)),

                //  Int(3) != Float(3) = Bool(True)
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Inequal}, exampleFloat)
                    .Invoke(IntObject.Create(3), FloatObject.Create(3)),
            };

            IObject[] result = new IObject[]
            {
                IntObject.Create(3),
                FloatObject.Create(3.33333325f),
                BoolObject.Create(false),
                BoolObject.Create(true),
                BoolObject.Create(true),
                BoolObject.Create(false),
                BoolObject.Create(false),
                FloatObject.Create(1f),
                BoolObject.Create(false),
            };

            for (int i = 0; i < testSet.Length; i++)
            {
                Assert.Equal(result[i].Type, testSet[i].Type);
                Assert.Equal(result[i].AsString(), testSet[i].AsString());
            }

        }

        [Fact]
        public void InvalidOperationTest()
        {
            EvaluationMapper mapper = new EvaluationMapper();

            IObject exampleInt = IntObject.Create(1);
            IObject exampleFloat = FloatObject.Create(1f);
            IObject exampleNull = NullObject.Create();
            IObject exampleBool = BoolObject.Create(true);

            Func<IObject, IObject, IObject>[] testSet = new Func<IObject, IObject, IObject>[]
            {
                //  Int
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Plus}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Minus}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Star}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Slash}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Equal}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Inequal}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LeftArrow}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.RightArrow}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LesserOrEqual}, exampleBool),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleBool),

                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Plus}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Minus}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Star}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Slash}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Equal}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.Inequal}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LeftArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.RightArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.LesserOrEqual}, exampleNull),
                mapper.GetEvaluationFunc(exampleInt, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleNull),

                //  Float
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Plus}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Minus}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Star}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Slash}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Equal}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Inequal}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.LeftArrow}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.RightArrow}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.LesserOrEqual}, exampleBool),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleBool),

                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Plus}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Minus}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Star}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Slash}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Equal}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.Inequal}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.LeftArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.RightArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.LesserOrEqual}, exampleNull),
                mapper.GetEvaluationFunc(exampleFloat, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleNull),

                //  Null
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Plus}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Plus}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Plus}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Plus}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Minus}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Minus}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Minus}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Minus}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Star}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Star}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Star}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Star}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Slash}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Slash}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Slash}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Slash}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Equal}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Equal}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Equal}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Inequal}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Inequal}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.Inequal}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LeftArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LeftArrow}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LeftArrow}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LeftArrow}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.RightArrow}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.RightArrow}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.RightArrow}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.RightArrow}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LesserOrEqual}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LesserOrEqual}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LesserOrEqual}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.LesserOrEqual}, exampleBool),

                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleNull),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleInt),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleFloat),
                mapper.GetEvaluationFunc(exampleNull, new Token() { Keyword = TokenType.GreaterOrEqual}, exampleBool),
            };

            //  This soulf be always false because there should not be a func coresponding to testset input data.
            bool containsFunc = testSet.Any(x => x != null);

            Assert.False(containsFunc);
        }
    }
}