using EdenClasslibrary.Types.ConfigTypes;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types
{
    public class EdenScriptRunner
    {
        private EdenRuntime _runtime;
        private ExecutionConfig _config;
        public EdenScriptRunner(EdenRuntime runtime, ExecutionConfig config)
        {
            _runtime = runtime;
            _config = config;
        }

        public void Run()
        {
            IObject result = _runtime.EvaluateFile(_config.Path);
            if(result is not NoneObject)
            {
                Console.WriteLine(result);
            }
        }
    }
}
