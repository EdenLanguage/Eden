using EdenClasslibrary.Errors.RuntimeErrors;
using EdenClasslibrary.Types.EnvironmentTypes;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types
{
    public class ParsingEnvironment
    {
        private Dictionary<string, VariablePayload> _variables;
        private Dictionary<string, FunctionPayload> _functions;
        private BuildIn _buildIn;
        private Parser _parser;
        public ParsingEnvironment OutterEnvironment { get; set; }
        public ParsingEnvironment(Parser parser, BuildIn buildIn)
        {
            _parser = parser;
            _buildIn = buildIn;
            _variables = new Dictionary<string, VariablePayload>();
            _functions = new Dictionary<string, FunctionPayload>();
        }

        /// <summary>
        /// Handles case: Var Int a = 10;
        /// </summary>
        public IObject DefineVariable(string name, VariablePayload variable, bool allowRedefine = false)
        {
            if (!VariableExists(name) || allowRedefine == true)
            {
                _variables.Add(name, variable);
                return variable.Variable;
            }
            return ErrorRuntimeVarUndef.CreateErrorObject(name, variable.Variable.Token, _parser.Lexer.GetLine(variable.Variable.Token));
        }

        /// <summary>
        /// Clears stored variables
        /// </summary>
        public void Clear()
        {
            _variables.Clear();
        }

        public IObject UpdateVariableScope(string name, IObject value)
        {
            if (VariableExists(name))
            {
                _variables[name] = VariablePayload.Create(value.Type, value);
                return value;
            }
            else
            {
                return ErrorRuntimeVarUndef.CreateErrorObject(name, value.Token, _parser.Lexer.GetLine(value.Token));
            }
        }

        public IObject UpdateVariable(string name, IObject value)
        {
            IObject updated = UpdateVariableScope(name, value);

            if(updated is ErrorObject && OutterEnvironment != null)
            {
                return OutterEnvironment.UpdateVariable(name, value);
            }

            return updated;
        }

        public bool VariableExistsRoot(string name)
        {
            bool varEx = VariableExists(name);

            if (varEx == false && OutterEnvironment != null)
            {
                return OutterEnvironment.VariableExistsRoot(name);
            }

            return varEx;
        }

        public IObject CallBuildInFunc(string name, params IObject[] arguments)
        {
            return _buildIn.CallBuildInFunc(name, arguments);
        }

        public bool IsBuildInFunction(string name)
        {
            return _buildIn.FunctionExists(name);
        }

        public FunctionPayload GetBuildInFunctionSignature(string name)
        {
            return _buildIn.GetFunctionSignature(name);
        }

        public ParsingEnvironment ExtendEnvironment()
        {
            ParsingEnvironment newEnv = new ParsingEnvironment(_parser, _buildIn);
            newEnv.OutterEnvironment = this;
            return newEnv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public IObject GetVariableScope(Token token, string name)
        {
            if (VariableExists(name))
            {
                return _variables[name].Variable;
            }
            return ErrorRuntimeVarUndef.CreateErrorObject(name, token, _parser.Lexer.GetLine(token));
        }

        public IObject GetVariable(Token token, string name)
        {
            IObject variable = GetVariableScope(token, name);

            if (variable is ErrorObject && OutterEnvironment != null)
            {
                return OutterEnvironment.GetVariable(token, name);
            }

            return variable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public bool VariableExists(string name)
        {
            return _variables.ContainsKey(name);
        }

        #region Functions
        /// <summary>
        /// Handles case: Var Int a = 10;
        /// </summary>
        public bool DefineFunction(string name, FunctionPayload function)
        {
            if (!FunctionExists(name))
            {
                _functions.Add(name, function);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FunctionPayload GetFunction(string name)
        {
            if (FunctionExists(name))
            {
                return _functions[name];
            }
            return null;
        }

        public FunctionPayload GetFunctionRoot(string name)
        {
            FunctionPayload func = GetFunction(name);

            if(func == null && OutterEnvironment != null)
            {
                return OutterEnvironment.GetFunctionRoot(name);
            }

            return func;
        }

        /// <summary>
        /// Checks if function exists in this env.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool FunctionExists(string name)
        {
            return _functions.ContainsKey(name);
        }

        public bool FunctionExistsRoot(string name)
        {
            bool funcExists = FunctionExists(name);

            if(funcExists == false && OutterEnvironment != null)
            {
                return OutterEnvironment.FunctionExistsRoot(name);
            }

            return funcExists;
        }
        #endregion
    }
}
