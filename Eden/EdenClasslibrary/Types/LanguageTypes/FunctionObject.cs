using EdenClasslibrary.Types.AbstractSyntaxTree;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class FunctionObject : IObject
    {
        public Type Type => throw new NotImplementedException();
        public Expression[] Arguments { get; set; }
        public BlockStatement Body { get; set; }

        private FunctionObject(BlockStatement body, params Expression[] arguments)
        {
            Arguments = arguments;
            Body = body;
        }

        public static IObject Create(BlockStatement body, params Expression[] arguments)
        {
            return new FunctionObject(body, arguments);
        }

        public override string ToString()
        {
            return Body.ToString();
        }

        public IObject Evaluate(params IObject[] arguments)
        {
            return null;
        }

        public string AsString()
        {
            throw new NotImplementedException();
        }

        public bool IsSameType(IObject other)
        {
            throw new NotImplementedException();
        }
    }
}
