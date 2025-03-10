namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public abstract class VariableValueExpression : Expression
    {
        private Type _type;
        public Type Type
        {
            get
            {
                return _type;
            }
        }
        protected VariableValueExpression(Token token, Type type) : base(token)
        {
            _type = type;
        }
        public string AsString()
        {
            return Type.Name;
        }
    }
}