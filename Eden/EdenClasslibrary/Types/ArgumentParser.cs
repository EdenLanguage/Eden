using EdenClasslibrary.Types.ConfigTypes;

namespace EdenClasslibrary.Types
{
    public class ArgumentParser
    {
        private string[] _parameters;
        private string _runtimeLocation;

        public ArgumentParser(string runtimeLocation, params string[] parameters)
        {
            _parameters = parameters;
            _runtimeLocation = runtimeLocation;
        }

        public EdenRuntimeConfig ParseParameters()
        {
            int paramsCount = _parameters.Length;
            bool useRepl = false;
            bool hasError = false;
            List<string> parameters = new List<string>();

            if (_parameters.Length == 0 || _parameters.Contains("--version"))
            {
                // Eden .... go for basic repl
                useRepl = true;
            }

            for (int i = 0; i < _parameters.Length; i++)
            {
                parameters.Add(_parameters[i]);
            }

            return new EdenRuntimeConfig(repl: useRepl, runtimeLocation: _runtimeLocation, parameters: parameters.ToArray());
        }
    }
}
