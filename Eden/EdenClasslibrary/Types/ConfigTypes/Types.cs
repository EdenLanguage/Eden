namespace EdenClasslibrary.Types.ConfigTypes
{
    public struct ReplConfig
    {
        public bool ColorPrinting { get; set; }
        public bool Version { get; }
        public ReplConfig(bool version, bool colorPrinting)
        {
            Version = version;
            ColorPrinting = colorPrinting;
        }
    }

    public struct ExecutionConfig
    {
        public bool GenerateAST { get; set; }
        public string Path { get; }
        public bool FileError { get; }
        public ExecutionConfig(bool generateAST, string path, bool error)
        {
            FileError = error;
            Path = path;
            GenerateAST = generateAST;
        }
    }

    public struct EdenRuntimeConfig
    {
        public bool ReplExecution { get; }
        public string RuntimeLocation { get; }
        public string[] Parameters { get; }
        public EdenRuntimeConfig(bool repl, string runtimeLocation, string[] parameters)
        {
            ReplExecution = repl;
            RuntimeLocation = runtimeLocation;
            Parameters = parameters;
        }

        public ExecutionConfig GenerateExecConfig()
        {
            bool fileError = true;
            bool generateAST = false;
            string path = string.Empty;

            if (Parameters.Length != 0)
            {
                string fileName = Parameters[0];
                path = Path.Combine(RuntimeLocation, fileName);
                bool fileExists = File.Exists(path);
                if(fileExists == true)
                {
                //Console.WriteLine(path);
                    fileError = false;

                    if (Parameters.Contains("--generate-ast"))
                    {
                        generateAST = true;
                    }
                }
            }

            return new ExecutionConfig(generateAST: generateAST, path: path, error: fileError);
        }

        public ReplConfig GenerateReplConfig()
        {
            bool colorPrinting = true;
            bool useVersion = false;

            if (Parameters.Length != 0)
            {
                if (Parameters.Contains("--version"))
                {
                    useVersion = true;
                }
                else 
                {
                    if (Parameters.Contains("--disable-colors"))
                    {
                        colorPrinting = false;
                    }
                }
            }

            return new ReplConfig(version: useVersion, colorPrinting: colorPrinting);
        }
    }
}
