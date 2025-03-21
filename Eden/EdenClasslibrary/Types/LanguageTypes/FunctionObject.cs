using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenClasslibrary.Types.LanguageTypes
{
    public class FunctionObject : IObject
    {
        public Type Type
        {
            get
            {
                return typeof(FunctionObject);
            }
        }
        public Expression[] Arguments { get; set; }
        public BlockStatement Body { get; set; }

        public Token Token { get; }

        private FunctionObject(Token token, BlockStatement body, params Expression[] arguments)
        {
            Token = token;
            Arguments = arguments;
            Body = body;
        }

        public static IObject Create(Token token, BlockStatement body, params Expression[] arguments)
        {
            return new FunctionObject(token, body, arguments);
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
