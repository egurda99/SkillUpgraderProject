using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatsTester : MonoBehaviour
    {
        [SerializeField] private bool _loadDefaultStats;


        private CharacterStatsHolder _characterStatsHolder;
        private readonly List<CharacterStat> _characterStats = new();

        [Inject]
        public void Construct(CharacterStatsHolder characterStatsHolder)
        {
            _characterStatsHolder = characterStatsHolder;
        }

        private void Start()
        {
            if (_loadDefaultStats)
            {
                LoadDefaultStats();
            }
        }

        [Button]
        public void LoadDefaultStats()
        {
            var powerStat = new CharacterStat("Power", 50);
            var aguilityStat = new CharacterStat("Aguility", 20);
            var armorStat = new CharacterStat("Armor", 60);
            var intelligenceStat = new CharacterStat("Intelligence", 55);
            var moveSpeedStat = new CharacterStat("Move speed", 30);
            var dexterityStat = new CharacterStat("Dexterity", 15);

            _characterStats.Add(powerStat);
            _characterStats.Add(aguilityStat);
            _characterStats.Add(armorStat);
            _characterStats.Add(intelligenceStat);
            _characterStats.Add(moveSpeedStat);
            _characterStats.Add(dexterityStat);


            for (var i = 0; i < _characterStats.Count; i++)
            {
                _characterStatsHolder.AddStat(_characterStats[i]);
            }
        }

        [Button]
        public void RemoveDefaultStats()
        {
            for (var i = 0; i < _characterStats.Count; i++)
            {
                var stat = _characterStatsHolder.GetStat(_characterStats[i].Name);

                _characterStatsHolder.RemoveStat(stat);
            }
        }


        [Button]
        public void AddStat(string name, int value)
        {
            var stat = new CharacterStat(name, value);
            _characterStatsHolder.AddStat(stat);
        }

        [Button]
        public void RemoveStat(string statName)
        {
            var stat = _characterStatsHolder.GetStat(statName);

            _characterStatsHolder.RemoveStat(stat);
        }
    }
}
