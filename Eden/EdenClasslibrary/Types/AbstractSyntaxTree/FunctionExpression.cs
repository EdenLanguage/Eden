﻿using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class FunctionExpression : Expression, IPrintable
    {
        private List<Expression> _functionArguments;
        public IdentifierExpression Name { get; set; }
        public VariableTypeExpression Type { get; set; }
        public Expression[] Arguments
        {
            get
            {
                return _functionArguments.ToArray();
            }
        }
        public BlockStatement Body { get; set; }
        public FunctionExpression(Token token) : base(token)
        {
            _functionArguments = new List<Expression>();
        }

        public void AddFuncArgument(Expression argument)
        {
            _functionArguments.Add(argument);
        }

        public override string ParenthesesPrint()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indents)}Function");
            sb.Append($"{Type.PrettyPrint()}");
            sb.Append($"{Name.PrettyPrint()}");
            sb.Append("(");

            //  Function args.
            for(int i = 0; i < Arguments.Length; i++)
            {
                sb.Append($"{(Arguments[i] as IPrintable).PrettyPrint()}");
                if (i > Arguments.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(")");
            sb.AppendLine("{");
            sb.Append($"{Body.PrettyPrint(indents + 1)}");
            sb.Append("}");

            string result = sb.ToString();
            return result;
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
