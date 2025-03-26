using EdenClasslibrary.Types.LanguageTypes;
using System.Diagnostics;
using System.Reflection;

namespace EdenClasslibrary.Types
{
    public class EdenRuntime
    {
        private Evaluator _evaluator;
        private Parser _parser;

        public string ExecutionLocation
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        public string Version
        {
            get
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(ExecutionLocation);
                return versionInfo.FileVersion;
            }
        }

        public EdenRuntime()
        {
            _parser = new Parser();
            _evaluator = new Evaluator(_parser);
        }

        #region Public
        public IObject Evaluate(string input)
        {
            return _evaluator.Evaluate(input);
        }

        public IObject EvaluateFile(string file)
        {
            return _evaluator.EvaluateFile(file);
        }
        #endregion
    }
}