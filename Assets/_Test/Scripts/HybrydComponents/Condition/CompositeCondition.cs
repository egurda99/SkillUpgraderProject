using System;
using System.Collections.Generic;

namespace MyCodeBase.HybridComponents
{
    public sealed class CompositeCondition : ICondition
    {
        private readonly List<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _conditions.Remove(condition);
        }

        //условие && условие && условие && условие && ...
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
