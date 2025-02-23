using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EdenClasslibrary.Types
{
    public class Lexer
    {
        public string Input {  get; set; }
        public int ReadPosition {  get; set; }
        public int NextReadPosition {  get; set; }
        public char CurrentChar {  get; set; }

        public Lexer()
        {
            Input = string.Empty;
            ReadPosition = 0;
            NextReadPosition = 1;
            CurrentChar = '\0';
        }

        public void SkipWhitespaces()
        {
            while(CurrentChar == ' ' || CurrentChar == '\r' || CurrentChar == '\t' || CurrentChar == '\n')
            {
                NextCharacter();
            }
        }

        public void NextCharacter()
        {
            if(NextReadPosition >= Input.Length)
            {
                CurrentChar = '\0';
            }
            else
            {
                CurrentChar = Input[NextReadPosition];
            }
            ReadPosition = NextReadPosition;
            NextReadPosition++;
        }

        public void ReadCurrentCharacter()
        {
            if(ReadPosition < Input.Length)
            {
                CurrentChar = Input[ReadPosition];
            }
            else
            {
                CurrentChar = '\0';
            }
        }

        public char PeekNextCharacter()
        {
            if(NextReadPosition < Input.Length)
            {
                return Input[NextReadPosition];
            }
            else
            {
                return '\0';
            }
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            Token currentToken = new Token();
            ReadCurrentCharacter();

            do
            {
                currentToken = NextToken();
                tokens.Add(currentToken);
            } while(currentToken.CanParseNextToken());

            return tokens;
        }

        public bool IsPotentianIdentifier()
        {
            if (char.IsLetter(CurrentChar)) return true;
            return false;
        }

        public bool CanTakeNextCharacter()
        {
            if (NextReadPosition < Input.Length && char.IsLetter(Input[NextReadPosition])) return true;
            return false;
        }

        public bool IsNumber()
        {
            if(char.IsNumber(CurrentChar)) return true;
            return false;
        }

        public string ReadInputNumber()
        {
            string inputNumber = string.Empty;
            do
            {
                inputNumber += CurrentChar;
                NextCharacter();
            } while (IsNumber());
            return inputNumber;
        }

        public string ReadInputLiteral()
        {
            bool canTakeNextChar = true;
            string inputLiteral = string.Empty;
            while (canTakeNextChar == true)
            {
                inputLiteral += CurrentChar;
                canTakeNextChar = CanTakeNextCharacter();
                NextCharacter();
            } 
            return inputLiteral;
        }

        public Token CreateNewToken(TokenType type, char value)
        {
            Token toker = new Token();
            toker.SetAttributes(type, value);
            NextCharacter();
            return toker;
        }

        public Token CreateNewToken(TokenType type, string value)
        {
            Token toker = new Token();
            toker.SetAttributes(type, value);
            NextCharacter();
            return toker;
        }

        public Token NextToken()
        {
            Token token = new Token();

            //NextCharacter();
            SkipWhitespaces();
            ReadCurrentCharacter();

            char nextCharacter = PeekNextCharacter();
            switch (CurrentChar)
            {
                case '+': token = CreateNewToken(TokenType.Plus, CurrentChar); break;
                case '-': token = CreateNewToken(TokenType.Minus, CurrentChar); break;
                case '!':
                    switch (nextCharacter)
                    {
                        case '=':
                            token = CreateNewToken(TokenType.Inequal, "!=");
                            NextCharacter();
                            break;
                        default:
                            token = CreateNewToken(TokenType.ExclemationMark, CurrentChar);
                            break;
                    }
                    break;
                case '=':
                    switch (nextCharacter)
                    {
                        case '=': 
                            token = CreateNewToken(TokenType.Equal, "==");
                            NextCharacter();
                            break;
                        default: 
                            token = CreateNewToken(TokenType.Assign, CurrentChar); 
                            break;
                    }
                    break;
                case ',': token = CreateNewToken(TokenType.Comma, CurrentChar); break;
                case ';': token = CreateNewToken(TokenType.Semicolon, CurrentChar); break;
                case '(': token = CreateNewToken(TokenType.LeftParenthesis, CurrentChar); break;
                case ')': token = CreateNewToken(TokenType.RightParenthesis, CurrentChar); break;
                case '{': token = CreateNewToken(TokenType.LeftBracket, CurrentChar); break;
                case '}': token = CreateNewToken(TokenType.RightBracket, CurrentChar); break;
                case '<':
                    switch (nextCharacter)
                    {
                        case '=':
                            token = CreateNewToken(TokenType.LesserOrEqual, "<=");
                            NextCharacter();
                            break;
                        default:
                            token = CreateNewToken(TokenType.LeftArrow, CurrentChar);
                            break;
                    }
                    break;
                case '>':
                    switch (nextCharacter)
                    {
                        case '=':
                            token = CreateNewToken(TokenType.GreaterOrEqual, ">=");
                            NextCharacter();
                            break;
                        default:
                            token = CreateNewToken(TokenType.RightArrow, CurrentChar);
                            break;
                    }
                    break;
                case '\0': token = CreateNewToken(TokenType.Eof, CurrentChar); break;
                default:
                    bool isIdentifier = IsPotentianIdentifier();
                    if(isIdentifier == true)
                    {
                        string inputLiteral = ReadInputLiteral();
                        bool isKeyword = Keyword.IsKeyword(inputLiteral);
                        if(isKeyword == true)
                        {
                            TokenType type = Keyword.ToTokenType(inputLiteral);
                            token.SetAttributes(type, inputLiteral);
                        }
                        else
                        {
                            token.SetAttributes(TokenType.Indentifier, inputLiteral);
                        }
                    }
                    else if (IsNumber())
                    {
                        string inputLiteral = ReadInputNumber();
                        int convertedNumber = 0;
                        bool couldConvert = int.TryParse(inputLiteral, out convertedNumber);

                        if(couldConvert == true)
                        {
                            token.SetAttributes(TokenType.Number, inputLiteral);
                        }
                        else
                        {
                            token.SetAttributes(TokenType.Illegal, inputLiteral);
                        }
                    }
                    else
                    {
                        token.SetAttributes(TokenType.Illegal, CurrentChar);
                    }
                    break;
            }


            return token;
        }
    }
}
