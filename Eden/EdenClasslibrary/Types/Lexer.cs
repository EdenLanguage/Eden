using System.Globalization;

namespace EdenClasslibrary.Types
{
    public class Lexer
    {
        private string _filePath;
        private string _filename;
        private string _input;

        private int _readPosition;
        private int _nextReadPosition;
        private char _currentChar;

        private int _currentLine;               // Current line index.
        private int _currentLinePosition;       // Position of currently parsed character.

        #region Constructor
        public Lexer()
        {
            _filename = string.Empty;
            _filePath = string.Empty;
            ClearLexer();
        }
        #endregion

        #region Public
        public void SetInput(string input)
        {
            ClearLexer();
            _input = input;
            _filename = "REPL";
            _filePath = "REPL";
        }

        public string GetLine(Token token)
        {
            if (string.IsNullOrEmpty(_input) || token.Line < 0)
            {
                return string.Empty;
            }

            try
            {
                string[] lines = _input.Replace("\r\n", "\n").Split('\n');

                if (token.Line - 1 >= lines.Length)
                {
                    return string.Empty;
                }

                return lines[token.Line - 1];
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        public void LoadFile(string filePath)
        {
            bool fileExists = File.Exists(filePath);
            if (fileExists == true)
            {
                try
                {
                    _input = File.ReadAllText(filePath);
                    _filename = Path.GetFileName(filePath);
                    _filePath = filePath;

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        /// <summary>
        /// Forwards parsing, generates new token.
        /// </summary>
        /// <returns></returns>
        public Token NextToken()
        {
            Token nextToken = new Token();

            ReadCurrentCharacter();
            SkipWhitespaces();

            char nextCharacter = PeekNextCharacter();

            switch (_currentChar)
            {
                case '+': nextToken = CreateNewToken(TokenType.Plus); break;
                case '-': nextToken = CreateNewToken(TokenType.Minus); break;
                case '!':
                    switch (nextCharacter)
                    {
                        case '=':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.Inequal, "!=");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.ExclemationMark);
                            break;
                    }
                    break;
                case '=':
                    switch (nextCharacter)
                    {
                        case '=':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.Equal, "==");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.Assign);
                            break;
                    }
                    break;
                case '&':
                    switch (nextCharacter)
                    {
                        case '&':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.And, "&&");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.Illegal);
                            break;
                    }
                    break;
                case '|':
                    switch (nextCharacter)
                    {
                        case '|':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.Or, "||");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.Illegal);
                            break;
                    }
                    break;
                case ',': nextToken = CreateNewToken(TokenType.Comma); break;
                case '.': nextToken = CreateNewToken(TokenType.Dot); break;
                case '%': nextToken = CreateNewToken(TokenType.Modulo); break;
                case ';': nextToken = CreateNewToken(TokenType.Semicolon); break;
                case '~': nextToken = CreateNewToken(TokenType.Tilde); break;
                case '?': nextToken = CreateNewToken(TokenType.QuenstionMark); break;
                case '^': nextToken = CreateNewToken(TokenType.Power); break;
                case '*': nextToken = CreateNewToken(TokenType.Star); break;
                case '/':
                    switch (nextCharacter)
                    {
                        case '*':
                            //  Block comment detected.
                            nextToken = CreateNewToken(TokenType.Comment, "/*");
                            EatBlockComment();
                            break;
                        case '/':
                            //  Line comment detected.
                            nextToken = CreateNewToken(TokenType.Comment, "//");
                            EatLineComment();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.Slash);
                            break;
                    }
                    break;
                case '(': nextToken = CreateNewToken(TokenType.LeftParenthesis); break;
                case ')': nextToken = CreateNewToken(TokenType.RightParenthesis); break;
                case '[': nextToken = CreateNewToken(TokenType.LeftSquareBracket); break;
                case ']': nextToken = CreateNewToken(TokenType.RightSquareBracket); break;
                case '{': nextToken = CreateNewToken(TokenType.LeftBracket); break;
                case '}': nextToken = CreateNewToken(TokenType.RightBracket); break;
                case '\'':
                    string charLiteral = ReadCharLiteral();
                    nextToken = CreateNewToken(TokenType.Char, charLiteral);
                    NextCharacter();
                    break;
                case '"':
                    string stringLiteral = ReadStringLiteral();
                    bool isValidStringLiteral = IsValidStringLiteral(stringLiteral);
                    if (isValidStringLiteral == true)
                    {
                        nextToken = CreateNewToken(TokenType.String, stringLiteral);
                        NextCharacter();
                    }
                    else
                    {
                        nextToken = CreateNewToken(TokenType.Illegal, stringLiteral);
                    }
                    break;
                case '<':
                    switch (nextCharacter)
                    {
                        case '=':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.LesserOrEqual, "<=");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.LeftArrow);
                            break;
                    }
                    break;
                case '>':
                    switch (nextCharacter)
                    {
                        case '=':
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.GreaterOrEqual, ">=");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.RightArrow);
                            break;
                    }
                    break;
                case '\0': nextToken = CreateNewToken(TokenType.Eof); break;
                default:
                    bool isIdentifier = IsPotentianIdentifier();
                    if (isIdentifier == true)
                    {
                        string inputLiteral = ReadInputLiteral();
                        bool isKeyword = Keyword.IsKeyword(inputLiteral);
                        if (isKeyword == true)
                        {
                            TokenType type = Keyword.ToTokenType(inputLiteral);
                            nextToken = CreateNewToken(type, inputLiteral);
                        }
                        else
                        {
                            nextToken = CreateNewToken(TokenType.Identifier, inputLiteral);
                        }
                        NextCharacter();
                    }
                    else if (IsCurrentCharNumber())
                    {
                        //  This function is handling numbers. So Int and Float should be parsable.
                        string inputLiteral = ReadInputNumberLiteral();
                        char type = PeekNextCharacter();
                        bool isTypeChar = IsLiteralTypeIndicator(type);

                        if (isTypeChar == true)
                        {
                            NextCharacter();
                            switch (type)
                            {
                                case 'c':
                                    nextToken = CreateNewToken(TokenType.Char, $"{inputLiteral}{type}");
                                    break;
                                case 'i':
                                    //  Int
                                    int literalAsInt = 0;
                                    bool couldConvertToInt = int.TryParse(inputLiteral, out literalAsInt);
                                    if (couldConvertToInt)
                                    {
                                        nextToken = CreateNewToken(TokenType.Int, inputLiteral + type);
                                    }
                                    else
                                    {
                                        nextToken = CreateNewToken(TokenType.Illegal, inputLiteral + type);
                                    }
                                    //NextCharacter();
                                    break;
                                case 'f':
                                    //  Float
                                    float convertedNumber = 0;
                                    bool couldConvertToFloat = float.TryParse(inputLiteral, CultureInfo.InvariantCulture, out convertedNumber);

                                    //  If contains '.' then it could be double
                                    if (couldConvertToFloat == true)
                                    {
                                        if (inputLiteral.EndsWith('.'))
                                        {
                                            inputLiteral = inputLiteral.Replace(".", "");
                                        }
                                        nextToken = CreateNewToken(TokenType.Float, inputLiteral + type);
                                    }
                                    else
                                    {
                                        nextToken = CreateNewToken(TokenType.Illegal, inputLiteral + type);
                                    }
                                    break;
                                default:
                                    nextToken = CreateNewToken(TokenType.Illegal, inputLiteral + type);
                                    break;
                            }
                            NextCharacter();
                        }
                        else
                        {
                            NextCharacter();
                            nextToken = CreateNewToken(TokenType.Illegal, inputLiteral + type);
                        }

                    }
                    else
                    {
                        nextToken = CreateNewToken(TokenType.Illegal);
                    }
                    break;
            }

            if (nextToken.Keyword == TokenType.Comment)
            {
                nextToken = NextToken();
            }

            return nextToken;
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Increments current line pointer.
        /// </summary>
        private void IncrementLinePointer()
        {
            _currentLine++;
        }

        /// <summary>
        /// Increments pointer of the current character in line.
        /// </summary>
        private void IncrementLineReadPositionPointer()
        {
            _currentLinePosition++;
        }

        private void ClearCurrentLinePosition()
        {
            _currentLinePosition = 1;
        }

        public void ClearLexer()
        {
            _input = string.Empty;
            _currentLine = 1;
            ClearCurrentLinePosition();
            _readPosition = 0;
            _nextReadPosition = 1;
            _currentChar = '\0';
        }

        /// <summary>
        /// Parse whole input.
        /// </summary>
        /// <returns>Input as tokens</returns>
        public Token[] Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token currentToken = new Token();

            do
            {
                currentToken = NextToken();
                tokens.Add(currentToken);
            } while (currentToken.IsValidAndNotEof());

            ClearLexer();
            return tokens.ToArray();
        }

        private void EatLineComment()
        {
            NextCharacter();
            NextCharacter();
            while (!IsNewLine())
            {
                NextCharacter();
                if (ReadCurrentCharacter() == '\0') break;
            }
        }

        private void EatBlockComment()
        {
            NextCharacter();
            NextCharacter();
            while (!IsNewLine() && !(ReadCurrentCharacter() == '*' && PeekNextCharacter() == '/') /*&& !(PeekNextCharacter() == '\0')*/)
            {
                NextCharacter();
                if (ReadCurrentCharacter() == '\0') break;
            }
            if (ReadCurrentCharacter() == '*' && PeekNextCharacter() == '/')
            {
                NextCharacter();
                NextCharacter();
            }
            if (IsNewLine())
            {
                SkipNewLine();
            }
        }

        private void SkipNewLine()
        {
            if (IsNewLine())
            {
                NextCharacter();
            }
            IncrementLinePointer();
            ClearCurrentLinePosition();
        }

        private void SkipTab()
        {
            if (ReadCurrentCharacter() == '\t')
            {
                NextCharacter();
            }
        }

        private void SkipWhitespaces()
        {
            /*  What can we eat?
             *  \n (new line) - should be eaten and current line should be incremented.
             *  \r (carriage return) - it is used in Mac and windows with \n so i tkink i will skip it for now.
             *  \t (tab) - w.e.
             */
            bool canEat = IsCurrentCharacterEatable();
            while (canEat == true)
            {
                char nextchar = PeekNextCharacter();

                if (IsNewLine())
                {
                    SkipNewLine();
                }
                else if (_currentChar == '\t')
                {
                    SkipTab();
                }
                else
                {
                    // ' '
                    NextCharacter();
                }

                canEat = IsCurrentCharacterEatable();
            }
        }

        private void NextCharacter()
        {
            if (_nextReadPosition >= _input.Length)
            {
                _currentChar = '\0';
            }
            else
            {
                _currentChar = _input[_nextReadPosition];
            }

            IncrementLineReadPositionPointer();

            _readPosition = _nextReadPosition;
            _nextReadPosition++;
        }

        private char ReadCurrentCharacter()
        {
            if (_readPosition < _input.Length)
            {
                _currentChar = _input[_readPosition];
            }
            else
            {
                _currentChar = '\0';
            }
            return _currentChar;
        }

        private char PeekNextCharacter()
        {
            if (_nextReadPosition < _input.Length)
            {
                return _input[_nextReadPosition];
            }
            else
            {
                return '\0';
            }
        }

        private bool IsNewLine()
        {
            bool isNewLine = false;
            char currentChar = ReadCurrentCharacter();
            if (currentChar == '\n')
            {
                isNewLine = true;
            }
            return isNewLine;
        }

        private bool IsCurrentCharacterEatable()
        {
            char currentChar = ReadCurrentCharacter();
            char nextChar = PeekNextCharacter();
            if (IsNewLine() || currentChar == '\r' || currentChar == '\t' || currentChar == ' ')
            {
                return true;
            }
            else return false;
        }

        private bool IsPotentianIdentifier()
        {
            if (char.IsLetter(_currentChar)) return true;
            return false;
        }

        private bool CanTakeNextCharacter()
        {
            if (_nextReadPosition < _input.Length && char.IsLetter(_input[_nextReadPosition])) return true;
            return false;
        }

        private bool IsCurrentCharStringToken()
        {
            return ReadCurrentCharacter() == '"';
        }

        private bool IsNextCharNumber()
        {
            return char.IsNumber(PeekNextCharacter());
        }

        private bool IsCurrentCharNumber()
        {
            if (char.IsNumber(_currentChar)) return true;
            return false;
        }

        /// <summary>
        /// Reads input literal for Int and Float types.
        /// </summary>
        /// <returns></returns>
        private string ReadInputNumberLiteral()
        {
            bool firstDigitIsNumber = true;
            bool commaEncountered = false;
            string inputNumber = string.Empty;

            bool keepParsing = true;

            bool isNextCharNumber = true;
            bool isNextCharComa = false;

            while (keepParsing)
            {
                inputNumber += _currentChar;

                //  ',' will never be first character because this function reacts on number.
                if (_currentChar == '.')
                {
                    commaEncountered = true;
                }

                isNextCharNumber = IsNextCharNumber();
                isNextCharComa = PeekNextCharacter() == '.';

                keepParsing = IsNextCharNumber() || (inputNumber.EndsWith('.') && IsNextCharNumber()) || (commaEncountered == false && PeekNextCharacter() == '.');

                if ((isNextCharNumber == true || (isNextCharComa && commaEncountered == false)) && keepParsing)
                {
                    NextCharacter();
                }

            }
            return inputNumber;
        }


        /// <summary>
        /// Reads input literal till it finds non matching token. Then it stops. This operation 'eats' next character in input.
        /// </summary>
        /// <returns></returns>
        private string ReadInputLiteral()
        {
            bool canTakeNextChar = true;
            string inputLiteral = string.Empty;
            while (canTakeNextChar == true)
            {
                inputLiteral += _currentChar;
                canTakeNextChar = CanTakeNextCharacter();
                if (canTakeNextChar == true)
                {
                    NextCharacter();
                }
            }
            return inputLiteral;
        }

        /// <summary>
        /// Makes token out of string literal. Requires refactoring.
        /// </summary>
        /// <returns></returns>
        [Obsolete("This function requires refactoring!!!")]
        private string ReadStringLiteral()
        {
            int metStrSymbol = 0;
            bool currCharStringToken = false;
            string stringLiteral = string.Empty;

            while (currCharStringToken == false)
            {
                stringLiteral += _currentChar;
                currCharStringToken = IsCurrentCharStringToken();
                if (currCharStringToken == true)
                {
                    if (metStrSymbol > 0)
                    {
                        // If '\"' was encountered twice break.
                        return stringLiteral;
                    }
                    else
                    {
                        currCharStringToken = false;
                        metStrSymbol++;
                    }
                }
                if (PeekNextCharacter() == '\0')
                {
                    return stringLiteral;
                }
                NextCharacter();
            }
            return stringLiteral;
        }

        private string ReadCharLiteral()
        {
            string input = string.Empty;
            char currentChar = ReadCurrentCharacter();

            input += currentChar;

            if (currentChar != '\'')
            {
                return null;
            }
            NextCharacter();

            while (ReadCurrentCharacter() != '\'')
            {
                input += ReadCurrentCharacter();
                NextCharacter();
            }
            input += ReadCurrentCharacter();

            char symbol = '\0';

            return input;
        }

        private bool IsValidStringLiteral(string literal)
        {
            if (literal != null && literal.StartsWith('\"') && literal.EndsWith('\"')) return true;
            return false;
        }

        private bool IsLiteralTypeIndicator(char symbol)
        {
            if (symbol == 'c' || symbol == 'f' || symbol == 'i')
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates new 'Token' from current character.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Token CreateNewToken(TokenType type)
        {
            Token toker = new Token();

            int tokenInLineBegPtr = _currentLinePosition;

            toker.SetAttributes(keyword: type, value: _currentChar, line: _currentLine, startPos: _currentLinePosition, filename: _filename);

            NextCharacter();

            return toker;
        }

        /// <summary>
        /// Creates new 'Token' from literal parsed value.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Token CreateNewToken(TokenType type, string value)
        {
            Token toker = new Token();

            int tokenInLineBegPtr = _currentLinePosition - (value.Length - 1);

            toker.SetAttributes(keyword: type, value: value, line: _currentLine, startPos: tokenInLineBegPtr, filename: _filename);
            return toker;
        }
        #endregion
    }
}