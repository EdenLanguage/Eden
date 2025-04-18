﻿namespace EdenClasslibrary.Types.LanguageTypes
{
    public class QuitObject : IObject
    {
        public Type Type => throw new NotImplementedException();

        public Token Token { get; }

        private QuitObject(Token token)
        {
            Token = token;
        }
        public string LanguageType
        {
            get
            {
                return "Quit";
            }
        }
        public static IObject Create(Token token)
        {
            return new QuitObject(token);
        }

        public override string ToString()
        {
            return AsString();
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
