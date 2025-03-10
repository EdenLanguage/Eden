using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class StringExpression : Expression, IPrintable
    {
        public string Value
        {
            get
            {
                // This this one requires refactoring. I don't realy know whether responsibility of
                // literal value of string should be delegated to lexer or done here...
                return NodeToken.LiteralValue.Replace("\"","");
            }
        }
        public StringExpression(Token token) : base(token) { }
        
        public override string ToString()
        {
            return PrettyPrint();
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}\"{Value}\"";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent)}{nameof(StringExpression)} {{ \"{Value}\" }}";
        }
    }
}
