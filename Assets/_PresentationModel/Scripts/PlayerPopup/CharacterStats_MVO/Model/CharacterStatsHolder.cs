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

        private readonly HashSet<CharacterStat> _stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (CheckIsStatAlreadyExist(stat))
            {
                Debug.LogWarning("Stat already exist");
                return;
            }


            if (_stats.Add(stat))
            {
                OnStatAdded.OnNext(stat);
            }
        }

        private bool CheckIsStatAlreadyExist(CharacterStat characterStat)
        {
            // � ���� rider ����� alt enter �� ����  ����������� � for. �������� ��� � ���� ��� ����������.
            foreach (var stat in _stats)
            {
                if (stat.Name == characterStat.Name)
                    return true;
            }

            return false;
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat))
            {
                OnStatRemoved.OnNext(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _stats.ToArray();
        }
    }
}
