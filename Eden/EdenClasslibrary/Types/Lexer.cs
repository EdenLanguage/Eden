using System.Globalization;

namespace EdenClasslibrary.Types
{
    public class Lexer
    {
        private string _filename;

        private string _input;

        private int _readPosition;
        private int _nextReadPosition;
        private char _currentChar;

        //  Helper variables
        private int _currentLine;               // Current line index.
        private int _currentLinePosition;       // Position of currently parsed character.

        public Lexer()
        {
            _filename = string.Empty;
            _input = string.Empty;
            _readPosition = 0;
            _nextReadPosition = 1;
            _currentChar = '\0';
            _currentLine = 0;
        }

        public void SetInput(string input)
        {
            ClearLexer();
            _input = input;
        }

        public void LoadFile(string filePath)
        {
            bool fileExists = File.Exists(filePath);
            if (fileExists == true)
            {
                try
                {
                    _input = File.ReadAllText(filePath);
                    _filename = filePath;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        /// <summary>
        /// Increments current line pointer.
        /// </summary>
        public void IncrementLinePointer()
        {
            _currentLine++;
        }

        /// <summary>
        /// Increments pointer of the current character in line.
        /// </summary>
        public void IncrementLineReadPositionPointer()
        {
            _currentLinePosition++;
        }

        public void ClearCurrentLinePosition()
        {
            _currentLinePosition = 0;
        }

        public void ClearLexer()
        {
            _input = string.Empty;
            _currentLine = 0;
            _currentLinePosition = 0;
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

        public void SkipWhitespaces()
        {
            /*  What can we eat?
             *  \n (new line) - should be eaten and current line should be incremented.
             *  \r (carriage return) - it is used in Mac and windows with \n so i tkink i will skip it for now.
             *  \t (tab) - w.e.
             */
            bool canEat = IsCurrentCharacterEatable();
            while (canEat == true)
            {
                char currChar = ReadCurrentCharacter();
                NextCharacter();
                if (currChar == '\n')
                {
                    IncrementLinePointer();
                    ClearCurrentLinePosition();
                }
                canEat = IsCurrentCharacterEatable();
            }
        }

        public void NextCharacter()
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

        public char ReadCurrentCharacter()
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

        public char PeekNextCharacter()
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

        public bool IsNextCharacterEatable()
        {
            char nextChar = PeekNextCharacter();
            if (nextChar == '\n' || nextChar == '\t' || nextChar == '\r' || nextChar == ' ')
            {
                return true;
            }
            else return false;
        }

        public bool IsCurrentCharacterEatable()
        {
            char nextChar = ReadCurrentCharacter();
            if (nextChar == '\n' || nextChar == '\t' || nextChar == '\r' || nextChar == ' ')
            {
                return true;
            }
            else return false;
        }

        public bool IsPotentianIdentifier()
        {
            if (char.IsLetter(_currentChar)) return true;
            return false;
        }

        public bool CanTakeNextCharacter()
        {
            if (_nextReadPosition < _input.Length && char.IsLetter(_input[_nextReadPosition])) return true;
            return false;
        }

        /// <summary>
        /// Check whether next character is '"'
        /// </summary>
        /// <returns></returns>
        public bool IsNextCharStringToken()
        {
            return PeekNextCharacter() == '"';
        }

        public bool IsCurrentCharStringToken()
        {
            return ReadCurrentCharacter() == '"';
        }

        public bool IsNextCharNumber()
        {
            return char.IsNumber(PeekNextCharacter());
        }

        public bool IsCurrentCharNumber()
        {
            if (char.IsNumber(_currentChar)) return true;
            return false;
        }

        /// <summary>
        /// Reads input literal till it finds non matching token. Then it stops. This operation 'eats' next character in input.
        /// </summary>
        /// <returns></returns>
        public string ReadInputNumber()
        {
            string inputNumber = string.Empty;

            bool canTakeNextChar = true;
            while (canTakeNextChar)
            {
                inputNumber += _currentChar;

                canTakeNextChar = IsNextCharNumber();
                if (canTakeNextChar == true)
                {
                    NextCharacter();
                }
            }
            return inputNumber;
        }

        /// <summary>
        /// Reads input literal for Int and Float types.
        /// </summary>
        /// <returns></returns>
        public string ReadInputNumberLiteral()
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
                // 10000000,

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
        public string ReadInputLiteral()
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
        public string ReadStringLiteral()
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
                NextCharacter();
            }
            return stringLiteral;
        }

        public bool IsValidStringLiteral(string literal)
        {
            if (literal != null && literal.StartsWith('\"') && literal.EndsWith('\"')) return true;
            return false;
        }

        /// <summary>
        /// Creates new 'Token' from current character.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Token CreateNewToken(TokenType type)
        {
            Token toker = new Token();

            int tokenInLineBegPtr = _currentLinePosition;

            toker.SetAttributes(type, _currentChar, _currentLine, tokenInLineBegPtr, _filename);
            return toker;
        }


        /// <summary>
        /// Creates new 'Token' from literal parsed value.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Token CreateNewToken(TokenType type, string value)
        {
            Token toker = new Token();

            int tokenInLineBegPtr = _currentLinePosition - (value.Length - 1);

            toker.SetAttributes(type, value, _currentLine, tokenInLineBegPtr, _filename);
            return toker;
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
                            nextToken = CreateNewToken(TokenType.Inequal, "!=");
                            // This is looking forward. So in case there is matching character. It should 'eat' this next one as well.
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
                            nextToken = CreateNewToken(TokenType.Equal, "==");
                            NextCharacter();
                            break;
                        default:
                            nextToken = CreateNewToken(TokenType.Assign);
                            break;
                    }
                    break;
                case ',': nextToken = CreateNewToken(TokenType.Comma); break;
                case '.': nextToken = CreateNewToken(TokenType.Dot); break;
                case ';': nextToken = CreateNewToken(TokenType.Semicolon); break;
                case '^': nextToken = CreateNewToken(TokenType.Power); break;
                case '*': nextToken = CreateNewToken(TokenType.Star); break;
                case '/': nextToken = CreateNewToken(TokenType.Slash); break;
                case '(': nextToken = CreateNewToken(TokenType.LeftParenthesis); break;
                case ')': nextToken = CreateNewToken(TokenType.RightParenthesis); break;
                case '{': nextToken = CreateNewToken(TokenType.LeftBracket); break;
                case '}': nextToken = CreateNewToken(TokenType.RightBracket); break;
                case '"':
                    string stringLiteral = ReadStringLiteral();
                    bool isValidStringLiteral = IsValidStringLiteral(stringLiteral);
                    if (isValidStringLiteral == true)
                    {
                        nextToken = CreateNewToken(TokenType.String, stringLiteral);
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
                    }
                    else if (IsCurrentCharNumber())
                    {
                        //  This function is handling numbers. So Int and Float should be parsable.
                        string inputLiteral = ReadInputNumberLiteral();
                        double convertedNumber = 0;
                        bool couldConvertToDouble = double.TryParse(inputLiteral, CultureInfo.InvariantCulture, out convertedNumber);

                        //  If contains '.' then it could be double
                        if (inputLiteral.Contains('.'))
                        {
                            if (couldConvertToDouble == true)
                            {
                                if (inputLiteral.EndsWith('.'))
                                {
                                    inputLiteral = inputLiteral.Replace(".", "");
                                }
                                nextToken = CreateNewToken(TokenType.Float, inputLiteral);
                            }
                            else
                            {
                                nextToken = CreateNewToken(TokenType.Illegal, inputLiteral);
                            }
                        }
                        else
                        {
                            int literalAsInt = 0;
                            bool couldConvertToInt = int.TryParse(inputLiteral, out literalAsInt);
                            if (couldConvertToInt)
                            {
                                nextToken = CreateNewToken(TokenType.Int, inputLiteral);
                            }
                            else
                            {
                                nextToken = CreateNewToken(TokenType.Illegal, inputLiteral);
                            }
                        }
                    }
                    else
                    {
                        nextToken = CreateNewToken(TokenType.Illegal);
                    }
                    break;
            }

            NextCharacter();

            if (nextToken.Keyword == TokenType.Illegal) throw new Exception($"Lexer encountered illegal token: {nextToken.PrintTokenDetails()}");

            return nextToken;
        }
    }
}
