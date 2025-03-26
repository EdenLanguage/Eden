using EdenClasslibrary.Types.ConfigTypes;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.TypesREPL;
using EdenClasslibrary.Utility;
using Pastel;
using TextCopy;

namespace EdenClasslibrary.Types
{
    public class EdenREPL
    {
        private EdenRuntime _runtime;
        private InputBufforManager _bufforManager;
        private InputHistorian _inputHistorian;
        private int _tabSize;
        private ReplConfig _config;

        public EdenREPL(EdenRuntime runtime, ReplConfig config)
        {
            _tabSize = 8;
            _config = config;
            _runtime = runtime;
            Common.ColorfulPrinting = _config.ColorPrinting;
            _bufforManager = new InputBufforManager();
            _inputHistorian = new InputHistorian();
        }

        private void EnableCursor()
        {
            Console.CursorVisible = true;
            Console.Write("\x1b[6 q");
        }

        /// <summary>
        /// Initializes REPL. It has to be called first.
        /// </summary>
        private void InitializeConsole()
        {
            Console.TreatControlCAsInput = true;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Eden";
            //  Calc tab size
            Console.Clear();
            int startX = Console.CursorLeft;
            Console.Write("\t");
            int endX = Console.CursorLeft;
            _tabSize = endX - startX;

            //  Clear after initialization
            Console.Clear();
        }

        private void PrintREPLInitialization()
        {
            Console.WriteLine("Eden runtime".Pastel(ConsoleColor.Green) + ", press the Escape (Esc) key to quit.");
            Console.Write(">> ");
        }

        private void DisableCursor()
        {
            Console.CursorVisible = false;
        }

        /// Clears current line 
        private string ClearLine()
        {
            string result = string.Empty;
            if (_bufforManager.HasAnyInput())
            {
                DisableCursor();
                result = _bufforManager.DumpBuffor();
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(">> ");
                EnableCursor();
            }
            return result;
        }

        /// <summary>
        /// If there are any characters in input, deletes last one.
        /// </summary>
        private void DeleteCharactedFromInput()
        {
            if (Console.CursorLeft > 3)
            {
                DisableCursor();
                _bufforManager.DeleteLastCharacter();
                Console.Write("\b \b");
                string postBuff = _bufforManager.GetPostBuffor();
                foreach (char character in postBuff)
                {
                    Console.Write(character);
                }
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - (postBuff.Length + 1), Console.CursorTop);
                _inputHistorian.ResetIndexer();
                EnableCursor();
            }
        }

        private void AppendSymbolToInput(string symbols)
        {
            foreach(char symbol in symbols)
            {
                AppendSymbolToInput(symbol);
            }
        }

        private void AppendSymbolToInput(char consoleKey)
        {
            if(consoleKey == '\t')
            {
                string tab = TabAsSpaces();
                _bufforManager.Append(tab);
                Console.Write(tab);
            }
            else
            {
                _bufforManager.Append(consoleKey);
                Console.Write(consoleKey);
            }

            _inputHistorian.ResetIndexer();

            PrintPostBuffor();
        }

        private void DeletePostBuffor()
        {
            DisableCursor();
            string postBuff = _bufforManager.GetPostBuffor();
            Console.SetCursorPosition(Console.CursorLeft + postBuff.Length, Console.CursorTop);
            for(int i = 0; i < postBuff.Length; i++)
            {
                Console.Write("\b \b");
            }
            EnableCursor();
        }

        private void PrintPostBuffor()
        {
            DisableCursor();
            string postBuff = _bufforManager.GetPostBuffor();
            foreach (char character in postBuff)
            {
                Console.Write(character);
            }
            Console.SetCursorPosition(Console.CursorLeft - postBuff.Length, Console.CursorTop);
            EnableCursor();
        }

        private void ContinueInputInNewLine()
        {
            DisableCursor();
            Console.WriteLine();
            Console.Write("-> ");
            EnableCursor();
        }

        private string TabAsSpaces()
        {
            int preLine = Console.CursorLeft;
            Console.Write('\t');
            int postLine = Console.CursorLeft;
            int tabWidth = postLine - preLine;

            Console.SetCursorPosition(preLine, Console.CursorTop);
            return new string(' ', tabWidth);
        }

        private void PrintOutputToConsole(string input = "")
        {
            DisableCursor();
            Console.WriteLine();
            if(input != "")
            {
                Console.WriteLine($"{input}");
            }
            Console.Write(">> ");
            EnableCursor();
        }

        public void Loop()
        {
            try
            {
                ConsoleKeyInfo inputCharacter;

                InitializeConsole();
                PrintREPLInitialization();

                do
                {
                    inputCharacter = Console.ReadKey(true);

                    //  Handling key input with 'Ctrl' pressed. For example 'Ctrl' + 'LeftArrow'
                    if ((inputCharacter.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        int leftOffset = _bufforManager.LeftWordOffset();
                        int rightOffset = _bufforManager.RightWordOffset();
                        switch (inputCharacter.Key)
                        {
                            case ConsoleKey.V:
                                string clipboardVal = ClipboardService.GetText().Replace("\t", new string(' ', _tabSize));
                                AppendSymbolToInput(clipboardVal);
                                break;
                            case ConsoleKey.Backspace:
                                for (int i = 0; i < leftOffset; i++)
                                {
                                    DeleteCharactedFromInput();
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                DisableCursor();
                                for (int i = 0; i < leftOffset; i++)
                                {
                                    if (Console.CursorLeft > 3)
                                    {
                                        bool wasMoved = _bufforManager.MoveLeft();
                                        if (wasMoved == true)
                                        {
                                            if (_bufforManager.QualifyForDumping())
                                            {
                                                _bufforManager.DumpSufixBuffor();
                                            }
                                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                        }
                                    }
                                }
                                EnableCursor();
                                break;
                            case ConsoleKey.RightArrow:
                                DisableCursor();
                                for (int i = 0; i < rightOffset; i++)
                                {
                                    if (_bufforManager.HasSufix())
                                    {
                                        bool wasMoved = _bufforManager.MoveRight();
                                        if (wasMoved == true)
                                        {
                                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                                        }
                                    }
                                }
                                EnableCursor();
                                break;
                            default:
                                //  Other
                                break;
                        }
                    }
                    else if ((inputCharacter.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        int leftOffset = _bufforManager.LeftWordOffset();
                        int rightOffset = _bufforManager.RightWordOffset();
                        switch (inputCharacter.Key)
                        {
                            case ConsoleKey.Enter:
                                DeletePostBuffor();
                                ContinueInputInNewLine();
                                PrintPostBuffor();
                                break;
                            default:
                                AppendSymbolToInput(inputCharacter.KeyChar);
                                break;
                        }
                    }
                    else if (inputCharacter.Key == ConsoleKey.Enter)
                    {
                        _inputHistorian.ResetIndexer();
                        string input = _bufforManager.DumpBuffor();
                        string outputBuffer = string.Empty;

                        if (input != "")
                        {

                            //Evaluate line
                            IObject evalRes = _runtime.Evaluate(input);

                            if (evalRes is not NoneObject)
                            {
                                outputBuffer = evalRes.ToString();
                            }
                            
                            if(evalRes is not ErrorObject)
                            {
                                //  Only if output is valid append this input to history.
                                _inputHistorian.Append(input);
                            }
                        }
                        
                        PrintOutputToConsole(outputBuffer);

                    }
                    else if (inputCharacter.Key == ConsoleKey.Backspace)
                    {
                        DeleteCharactedFromInput();
                    }
                    else if (inputCharacter.Key == ConsoleKey.UpArrow)
                    {
                        if (_inputHistorian.CanGoBack())
                        {
                            DisableCursor();
                            ClearLine();
                            string previous = _inputHistorian.Previous();
                            if (previous != "")
                            {
                                _bufforManager.Append(previous);
                                foreach (char character in previous)
                                {
                                    Console.Write(character);
                                }
                            }
                            EnableCursor();
                        }
                    }
                    else if (inputCharacter.Key == ConsoleKey.DownArrow)
                    {
                        if (_inputHistorian.CanGoForward())
                        {
                            DisableCursor();
                            ClearLine();
                            string previous = _inputHistorian.Next();
                            if (previous != "")
                            {
                                _bufforManager.Append(previous);
                                foreach (char character in previous)
                                {
                                    Console.Write(character);
                                }
                            }
                            EnableCursor();
                        }

                    }
                    else if (inputCharacter.Key == ConsoleKey.LeftArrow)
                    {
                        if (Console.CursorLeft > 3)
                        {
                            bool wasMoved = _bufforManager.MoveLeft();
                            if (wasMoved == true)
                            {
                                if (_bufforManager.QualifyForDumping())
                                {
                                    _bufforManager.DumpSufixBuffor();
                                }
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                        }
                    }
                    else if (inputCharacter.Key == ConsoleKey.Delete)
                    {
                        //  Clear current line
                        ClearLine();
                    }
                    else if (inputCharacter.Key == ConsoleKey.RightArrow)
                    {
                        if (_bufforManager.HasSufix())
                        {
                            bool wasMoved = _bufforManager.MoveRight();
                            if (wasMoved == true)
                            {
                                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                            }
                        }
                    }
                    else
                    {
                        AppendSymbolToInput(inputCharacter.KeyChar);
                    }
                } while (inputCharacter.Key != ConsoleKey.Escape);
            }
            catch(Exception exception)
            {
                Console.Clear();
                Console.WriteLine("Eden interpreter encountered fatal error!");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Godspeed!");
            }
        }
    }
}
