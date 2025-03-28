namespace EdenClasslibrary.Types.TypesREPL
{
    public class InputBuffor
    {
        private Stack<char> _stack;

        public int Length
        {
            get
            {
                return _stack.Count;
            }
        }

        public InputBuffor()
        {
            _stack = new Stack<char>();
        }

        public int NextWhiteSpaceOffset(char symbol)
        {
            return NextSymbolOffset(symbol);
            //int index = _stack.ToList().IndexOf(' ');

            //if(index == 0)
            //{
            //    index = _stack.ToList().FindIndex(c => c != ' ');
            //}

            //if(index != -1)
            //{
            //    return index;
            //}
            //return _stack.Count;
        }

        private int NextSymbolOffset(char symbol)
        {
            int index = _stack.ToList().IndexOf(symbol);

            if (index == 0)
            {
                index = _stack.ToList().FindIndex(c => c != symbol);
            }

            if (index != -1)
            {
                return index;
            }
            return _stack.Count;
        }

        /// <summary>
        /// Delete last added element of buffor.
        /// </summary>
        /// <returns></returns>
        public char Pop()
        {
            return _stack.Pop();
        }

        /// <summary>
        /// Check first element of buffor without deleting it.
        /// </summary>
        /// <returns></returns>
        public char Peek()
        {
            return _stack.Peek();
        }

        public string PeekAll()
        {
            string result = string.Empty;
            int i = 0;

            while (i < _stack.Count)
            {
                result += _stack.ElementAt(i++);
            }

            return result;
        }

        /// <summary>
        /// Push character to top of buffor.
        /// </summary>
        /// <param name="character"></param>
        public void Push(char character)
        {
            _stack.Push(character);
        }

        /// <summary>
        /// Dumps buffor content to string.
        /// </summary>
        /// <returns></returns>
        public string Dump()
        {
            string result = string.Empty;

            while(_stack.Count > 0)
            {
                result += _stack.Pop();
            }

            return result;
        }
    }
}
