using System;
using System.Collections.Generic;

namespace _UpgradePractice.Scripts
{
    public class CompositeCondition : ICondition
    {
        private readonly List<Func<bool>> _conditions = new();

        public void AppendCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _conditions.Remove(condition);
        }

        public bool Invoke()
        {
            for (var i = _conditions.Count - 1; i >= 0; i--)
            {
                if (_conditions[i].Invoke() == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
