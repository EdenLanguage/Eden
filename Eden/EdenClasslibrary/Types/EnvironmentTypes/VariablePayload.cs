using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types.EnvironmentTypes
{
    public class VariablePayload
    {
        public Type Type { get; set; }
        public IObject Variable { get; set; }

        private VariablePayload(Type type, IObject variable)
        {
            Type = type;
            Variable = variable;
        }

        public static VariablePayload Create(Type type, IObject variable)
        {
            return new VariablePayload(type, variable);
        }

        public override string ToString()
        {
            return $"{Variable.AsString()}";
        }
    }
}
