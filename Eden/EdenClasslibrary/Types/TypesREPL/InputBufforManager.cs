namespace EdenClasslibrary.Types.TypesREPL
{
    public class InputBufforManager
    {
        private InputBuffor _preCursorBuffor;
        private InputBuffor _postCursorBuffor;

        public InputBufforManager()
        {
            _postCursorBuffor = new InputBuffor();
            _preCursorBuffor = new InputBuffor();
        }

        public string DumpBuffor()
        {
            string buffor = GetBuffor();
            _preCursorBuffor.Dump();
            _postCursorBuffor.Dump();
            return buffor;
        }

        public void DeleteLastCharacter()
        {
            _preCursorBuffor.Pop();
        }

        public void DumpSufixBuffor()
        {
            _postCursorBuffor.Dump();
        }

        public int RightWordOffset()
        {
            int space = _postCursorBuffor.NextWhiteSpaceOffset(' ');
            return space;
        }

        public int LeftWordOffset()
        {
            int space = _preCursorBuffor.NextWhiteSpaceOffset(' ');
            return space;
        }

        public string GetBuffor()
        {
            string buffor = string.Empty;
            buffor = _preCursorBuffor.Dump();
            string reversed = new string(buffor.Reverse().ToArray());
            buffor = reversed;
            buffor += _postCursorBuffor.Dump();
            return buffor;
        }

        public bool HasAnyInput()
        {
            return _postCursorBuffor.Length != 0 || _preCursorBuffor.Length != 0;
        }

        public string GetPostBuffor()
        {
            string buffor = _postCursorBuffor.PeekAll();
            return buffor;
        }

        public void Append(char character)
        {
            _preCursorBuffor.Push(character);
        }

        public void Append(string input)
        {
            foreach(char symbol in input)
            {
                _preCursorBuffor.Push(symbol);
            }
        }

        /// <summary>
        /// Returns true if last character is ' ' and doesn't have any other symbols.
        /// </summary>
        /// <returns></returns>
        public bool QualifyForDumping()
        {
            if(_postCursorBuffor.Length == 1 && _postCursorBuffor.Peek() == ' ' || _postCursorBuffor.Length == 1 && _postCursorBuffor.Peek() == '\t')
            {
                return true;
            }
            return false;
        }

        public bool MoveLeft()
        {
            if(_preCursorBuffor.Length > 0)
            {
                _postCursorBuffor.Push(_preCursorBuffor.Pop());
                return true;
            }
            return false;
        }

        public bool MoveRight()
        {
            if(_postCursorBuffor.Length > 0)
            {
                _preCursorBuffor.Push(_postCursorBuffor.Pop());
                return true;
            }
            return false;   
        }

        public bool HasSufix()
        {
            return _postCursorBuffor.Length > 0;
        }
    }
}
