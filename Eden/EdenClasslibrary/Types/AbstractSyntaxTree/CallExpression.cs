using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class CallExpression : Expression, IPrintable
    {
        /*  Add(5, 10)
         *  Expression ( Expression[] )
         */
        private List<Expression> _arguments;
        public Expression Function { get; set; }
        public Expression[] Arguments
        {
            get
            {
                return _arguments.ToArray();
            }
        }
        public CallExpression(Token token) : base(token)
        {
            _arguments = new List<Expression>();
        }

        public void AddArgumentExpression(Expression argument)
        {
            _arguments.Add(argument);
        }

        public override string ParenthesesPrint()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(Function as IPrintable).PrettyPrint()}");
            sb.Append("(");
            for(int i = 0; i < Arguments.Length; i++)
            {
                sb.Append($"{(Arguments[i] as IPrintable).PrettyPrint()}");
                if(i < Arguments.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");
            string result = sb.ToString();
            return result;
        }

        public string PrettyPrintAST(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
