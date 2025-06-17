using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class UpgradePriceTable
    {
        [Space] [SerializeField] private int _basePrice;

        [Space] [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")] [SerializeField]
        private int[] _levels;

        public int GetPrice(int level)
        {
            var index = level - 1;
            index = Mathf.Clamp(index, 0, _levels.Length - 1);
            return _levels[index];
        }

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }

        public void OnValidate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = new int();
            for (var level = 2; level <= maxLevel; level++)
            {
                var price = _basePrice * level;
                table[level - 1] = price;
            }

            _levels = table;
        }
    }
}
