using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types.EnvironmentTypes
{
    public class FunctionPayload
    {
        public Type Type { get; set; }
        public BlockStatement Body { get; set; }
        public VariablePayload[] Arguments { get; set; }
        private FunctionPayload(Type type, VariablePayload[] arguments, BlockStatement body)
        {
            Type = type;
            Arguments = arguments;
            Body = body;
        }
        public static FunctionPayload Create(Type type, VariablePayload[] arguments, BlockStatement body)
        {
            return new FunctionPayload(type, arguments, body);
        }

        public static VariablePayload[] GenerateArgumentsSignature(IObject[] rawSignatures)
        {
            VariablePayload[] args = new VariablePayload[rawSignatures.Length];

            for (int i = 0; i < rawSignatures.Length; i++)
            {
                VariableSignatureObject vso = rawSignatures[i] as VariableSignatureObject;

                args[i] = VariablePayload.Create(vso.Type, rawSignatures[i]);
            }

            return args;
        }

        public bool ArgumentsSignatureMatch(CallExpression callExpression)
        {
            if(Arguments.Length != callExpression.Arguments.Length)
            {
                return false;
            }

            //for(int i = 0; i < Arguments.Length; i++)
            //{
            //    VariablePayload signature = Arguments[i];
            //    VariableValueExpression callExpArg = callExpression.Arguments[i] as VariableValueExpression;

            //    if (callExpArg == null) throw new Exception("Not value type!");

            //    if (callExpArg.Type != signature.Variable.Type)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        public override string ToString()
        {
            return $"{(Body as IPrintable).PrettyPrint()}";
        }
    }
}
