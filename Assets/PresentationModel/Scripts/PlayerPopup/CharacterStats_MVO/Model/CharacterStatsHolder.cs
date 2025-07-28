using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStatsHolder
    {
        public Subject<CharacterStat> OnStatAdded = new();
        public Subject<CharacterStat> OnStatRemoved = new();

        private readonly Dictionary<string, CharacterStat> _stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (_stats.ContainsKey(stat.Name))
            {
                Debug.LogWarning($"Stat with name {stat.Name} already exists.");
                return;
            }

            _stats.Add(stat.Name, stat);
            OnStatAdded.OnNext(stat);
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.TryGetValue(stat.Name, out var existingStat))
            {
                _stats.Remove(stat.Name);
                OnStatRemoved.OnNext(existingStat);
            }
            else
            {
                Debug.LogWarning($"Trying to remove non-existent stat: {stat.Name}");
            }
        }

        public CharacterStat GetStat(string name)
        {
            if (_stats.TryGetValue(name, out var stat))
            {
                return stat;
            }

            throw new Exception($"Stat '{name}' is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _stats.Values.ToArray();
        }
    }
}
