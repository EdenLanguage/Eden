using EdenClasslibrary.Types;
using EdenClasslibrary.Types.ConfigTypes;

string[] programArguments = Environment.GetCommandLineArgs();

ArgumentParser configParser = new ArgumentParser(AppDomain.CurrentDomain.BaseDirectory, args);
EdenRuntimeConfig runtimeConfig = configParser.ParseParameters();

EdenRuntime runtime = new EdenRuntime();

bool useRepl = runtimeConfig.ReplExecution; 

if (useRepl == true)
{
    ReplConfig config = runtimeConfig.GenerateReplConfig();

    if(config.Version == true)
    {
        Console.WriteLine($"Eden runtime version: '{runtime.Version}'");
    }
    else
    {
        EdenREPL repl = new EdenREPL(runtime, config);
        repl.Loop();
    }
}
else
{
    ExecutionConfig execConfig = runtimeConfig.GenerateExecConfig();

    if(execConfig.FileError == true)
    {
        Console.WriteLine($"Eden cannot access the file at: '{execConfig.Path}'.");
        Console.WriteLine("Ensure the directory path does not contain any whitespace, as the interpreter may fail to access the file.");
        Console.WriteLine("Rename the folder to remove any spaces.");
    }
    else
    {
        EdenScriptRunner scripter = new EdenScriptRunner(runtime, execConfig);
        scripter.Run();
    }
}