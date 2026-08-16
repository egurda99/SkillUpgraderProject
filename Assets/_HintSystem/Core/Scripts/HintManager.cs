using System;
using System.Collections.Generic;

namespace Game.Hints
{
    public sealed class HintManager
    {
        public event Action<HintType> OnCompleted;

        private Dictionary<HintType, bool> _hints = new();

        public void Initialize(Dictionary<HintType, bool> hints)
        {
            _hints = hints;
        }

        public bool IsCompleted(HintType type)
        {
            return _hints.TryGetValue(type, out bool completed) && completed;
        }

        public void Complete(HintType type)
        {
            if (IsCompleted(type))
            {
                return;
            }

            _hints[type] = true;
            OnCompleted?.Invoke(type);
        }

        public List<HintType> GetCompletedHints()
        {
            var completed = new List<HintType>();

            foreach (KeyValuePair<HintType, bool> pair in _hints)
            {
                if (pair.Value)
                    completed.Add(pair.Key);
            }

            return completed;
        }
    }
}
