using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types.EnvironmentTypes
{
    public class FunctionPayload
    {
        public Type Type { get; set; }
        public BlockStatement Body { get; set; }
        public ObjectSignature[] Arguments { get; set; }
        private FunctionPayload(Type type, ObjectSignature[] arguments, BlockStatement body)
        {
            Type = type;
            Arguments = arguments;
            Body = body;
        }
        public static FunctionPayload Create(Type type, ObjectSignature[] arguments, BlockStatement body)
        {
            return new FunctionPayload(type, arguments, body);
        }

        public static ObjectSignature[] GenerateArgumentsSignature(IObject[] rawSignatures)
        {
            ObjectSignature[] args = new ObjectSignature[rawSignatures.Length];

            for (int i = 0; i < rawSignatures.Length; i++)
            {
                VariableSignatureObject vso = rawSignatures[i] as VariableSignatureObject;

                args[i] = ObjectSignature.Create(vso.Name, vso.Type);
            }

            return args;
        }

        public bool ArgumentsSignatureMatch(IObject[] arguments)
        {
            if(Arguments.Length != arguments.Length)
            {
                return false;
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if ((arguments[i].GetType() != Arguments[i].Type) && !arguments[i].GetType().GetInterfaces().Contains(Arguments[i].Type))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Body.Print()}";
        }
    }
}
