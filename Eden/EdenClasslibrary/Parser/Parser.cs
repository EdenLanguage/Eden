using EdenClasslibrary.Parser.AST;
using EdenClasslibrary.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EdenClasslibrary.Parser
{
    public class Parser
    {
        public Lexer lexer;
        public Token CurrentToken;
        public Token NextToken;
        public ASTRootNode _astRoot;
        private List<string> _errors;
        public string[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }
        public ASTRootNode ASTRoot
        {
            get
            {
                return _astRoot;
            }
        }
        public Parser()
        {
            lexer = new Lexer();
            // This one needs to be changed to root of AST tree node type.
            _astRoot = new ASTRootNode();
            _errors = new List<string>();
        }

        public void Parse(string code)
        {
            lexer = new Lexer();
            lexer.Input = code;

            _astRoot = new ASTRootNode();

            LoadNextToken();

            while(CurrentToken.Keyword != TokenType.Eof && CurrentToken.Keyword != TokenType.Illegal)
            {
                INode node = null;
                // Now check what is the token and execute branch. For example 'VariableStatement' etc...
                switch (CurrentToken.Keyword)
                {
                    case TokenType.Var:
                    case TokenType.VarType:
                        node = ParseVariableStatement();
                        break;
                    case TokenType.Keyword:
                        switch (CurrentToken.Value)
                        {
                            case "return":
                                node = ParseReturnStatement();
                                break;
                            default:
                                node = ParseKeywordStatement();
                                break;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if(node != null)
                {
                    _astRoot.Nodes.Add(node);
                }
            }
        }

        public void EatStatement()
        {
            // Go throught tokens till you find ';' and then eat it as well.
            while(CurrentToken.Keyword != TokenType.Semicolon && NextToken.Keyword != TokenType.Eof)
            {
                LoadNextToken();
            }
            LoadNextToken();
        }

        public VariableStatement ParseKeywordStatement()
        {
            return null;
        }

        public ReturnStatement ParseReturnStatement()
        {
            ReturnStatement returnStatement = new ReturnStatement();

            if(CurrentToken.Keyword != TokenType.Keyword && CurrentToken.Value != "return")
            {
                _errors.Add($"return token expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            returnStatement.NodeToken = CurrentToken;

            LoadNextToken();

            int counter = 0;
            while(CurrentToken.Keyword != TokenType.Semicolon)
            {
                LoadNextToken();
                counter++;
            }
            // This needs to be implemented.
            returnStatement.ReturnExpression = null;

            if(counter == 0)
            {
                // No expresion
                _errors.Add($"Expression tokens expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            LoadNextToken();

            return returnStatement;
        }

        public VariableStatement ParseVariableStatement()
        {
            // Example: 'var int variable = 10'
            VariableStatement variableStatement = new VariableStatement();
            
            if(CurrentToken.Keyword != TokenType.Var)
            {
                _errors.Add($"var keyword expected but got '{CurrentToken.Keyword}'");
                EatStatement();
                return null;
            }

            Token variableToken = CurrentToken;
            Token variableTypeToken = null;

            // Check whether it is a type
            if (NextToken.IsVariableType())
            {
                LoadNextToken();
                variableTypeToken = CurrentToken;
            }
            else
            {
                _errors.Add($"Expected variable type token but got '{CurrentToken.Keyword}'");
                EatStatement();
                return null;
            }
            
            LoadNextToken();
            Token variabeNameToken = CurrentToken;

            Token nodeToken = null;
            if (NextToken.IsAssignType())
            {
                // Consume '='
                LoadNextToken();
                nodeToken = CurrentToken;
            }
            else
            {
                _errors.Add($"Expected assignment token but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            LoadNextToken();
            
            // Calculate expression
            IExpressionNode expression = null;

            // Eat everyting till ';' is encountered. Later on there should be expressnio evaluation.
            int counter = 0;
            while(CurrentToken.Keyword != TokenType.Semicolon)
            {
                LoadNextToken();
                counter++;  
            }

            if(counter == 0)
            {
                // No expression tokens encountered.
                _errors.Add($"Expression token expected but got '{CurrentToken}'");
                EatStatement();
                return null;
            }

            variableStatement.VarToken = variabeNameToken;
            variableStatement.VarTypeToken = variableTypeToken;
            variableStatement.NameToken = variabeNameToken;
            variableStatement.NodeToken = nodeToken;
            variableStatement.Expression = expression;

            LoadNextToken();

            return variableStatement;
        }

        public void LoadNextToken()
        {
            if(CurrentToken == null)
            {
                // Lexer was not run yet so load 2 tokens
                CurrentToken = lexer.NextToken();
                NextToken = lexer.NextToken();
            }
            else
            {
                // It was so just go oe position ahead.
                CurrentToken = NextToken;
                NextToken = lexer.NextToken();
            }
        }
    }
}
