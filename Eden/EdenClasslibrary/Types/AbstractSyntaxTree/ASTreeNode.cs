﻿using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using System.Reflection.Emit;
using System.Security.Principal;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Basic node of AbstractSyntaxTree
    /// </summary>
    public abstract class ASTreeNode
    {
        public Token NodeToken { get; set; }
        public ASTreeNode(Token token)
        {
            NodeToken = token;
        }
        public abstract override string ToString();
    }
}
