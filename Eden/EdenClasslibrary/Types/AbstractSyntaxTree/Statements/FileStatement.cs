using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class FileStatement : Statement
    {
        private string _filePath; 
        public string FileName
        {
            get
            {
                if(_filePath == "REPL")
                {
                    return string.Empty;
                }
                else return Path.GetFileName(_filePath);
            }
        }
        public string FilePath
        {
            get
            {
                return _filePath;
            }
        }
        public BlockStatement Block { get; set; }
        public FileStatement(Token token, string filePath = "REPL") : base(token)
        {
            _filePath = filePath;
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            if (FilePath != "REPL")
            {
                sb.AppendLine($"File name: {FileName}");
            }

            sb.AppendLine($"File path: {FilePath}");
            sb.AppendLine("Program:");
            sb.AppendLine("");
            sb.Append($"{Block.Print()}");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            if (FilePath != "REPL")
            {
                sb.AppendLine($"File name: {FileName}");
            }

            sb.AppendLine($"File path: {FilePath}");
            sb.AppendLine($"{nameof(FileStatement)} {{");
            sb.Append($"{Block.ToAbstractSyntaxTree(1)}");
            sb.AppendLine("};");

            string result = sb.ToString();
            return result;
        }
    }
}