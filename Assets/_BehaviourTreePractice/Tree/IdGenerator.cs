using System;
using System.Collections.Generic;

namespace BehaviourTreePractice
{
    public static class IdGenerator
    {
        private static readonly Dictionary<Type, int> _counters = new();

        public static string Generate<T>(string prefix = "")
        {
            var type = typeof(T);
            if (!_counters.ContainsKey(type))
            {
                _counters[type] = 1;
            }

            return $"{prefix}{type.Name}_{_counters[type]++}";
        }
    }
}
