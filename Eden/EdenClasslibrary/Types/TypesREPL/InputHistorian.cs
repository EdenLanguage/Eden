namespace EdenClasslibrary.Types.TypesREPL
{
    public class InputHistorian
    {
        private int _index;
        private List<string> _history;

        public InputHistorian()
        {
            _history = new List<string>();
            ResetIndexer();
        }

        public void Clear()
        {
            _history.Clear();
            ResetIndexer();
        }

        public void ResetIndexer()
        {
            _index = _history.Count;
        }

        /// <summary>
        /// Append input to history. If input is already present in history, it does not add it.
        /// </summary>
        /// <param name="command"></param>
        public void Append(string command)
        {
            if (!_history.Contains(command))
            {
                _history.Add(command);
                ResetIndexer();
            }
        }

        public string Previous()
        {
            string result = string.Empty;

            if(CanGoBack())
            {
                result = _history[--_index];
            }

            return result;
        }

        public bool CanGoBack()
        {
            return ((_index - 1) < _history.Count) && ((_index - 1) >= 0);
        }

        public bool CanGoForward()
        {
            return (_index + 1) < _history.Count;
        }

        public string Next()
        {
            string result = string.Empty;

            if (CanGoForward())
            {
                result = _history[++_index];
            }
            else
            {
                _index++;
            }

            return result;
        }
    }
}
