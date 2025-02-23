using EdenClasslibrary.Types;

namespace EdenClasslibrary.Parser.AST
{
    public interface INode
    {
        Token NodeToken { get; set; }
        List<INode> Nodes { get; set; }
    }

    public class ASTRootNode : INode
    {
        public Token NodeToken { get; set; }
        public List<INode> Nodes { get; set; }
        public ASTRootNode()
        {
            Nodes = new List<INode>();
        }
    }

    public interface IExpressionNode : INode
    {
    }

    public interface IStatementNode : INode
    {
    }

    public class VariableStatement : IStatementNode
    {
        public Token VarToken { get; set; }
        public Token VarTypeToken { get; set; }
        public Token NameToken { get; set; }
        public IExpressionNode Expression { get; set; }
        public Token NodeToken { get; set; }
        public List<INode> Nodes { get; set; }
        public VariableStatement()
        {
            Nodes = new List<INode>();
        }
    }

    public class ReturnStatement : IStatementNode
    {
        public Token NodeToken { get; set; }
        public IExpressionNode ReturnExpression { get; set; }
        public List<INode> Nodes { get; set; }
        public ReturnStatement()
        {
            Nodes = new List<INode>();
        }
    }
}
