using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class CapacityTable
    {
        public float CapacityStep => _capacityStep;

        [Space] [InfoBox("Capacity: Non-Linear (Exponential Curve, Integer Rounded)")] [SerializeField]
        private float _startCapacity = 10f;

        [SerializeField] private float _endCapacity = 2f;

        [SerializeField] [Range(0.1f, 5f)] private float _curvePower = 2.5f;

        [ReadOnly] [SerializeField] private float _capacityStep;

        [Space]
        [ListDrawerSettings(IsReadOnly = true, OnBeginListElementGUI = "DrawLabelForListElement")]
        [SerializeField]
        private int[] _table;

        public int GetCapacity(int level)
        {
            var index = Mathf.Clamp(level - 1, 0, _table.Length - 1);
            return _table[index];
        }

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            _table = new int[maxLevel];

            for (var i = 0; i < maxLevel; i++)
            {
                var normalized = (float)i / (maxLevel - 1);
                var curved = Mathf.Pow(1f - normalized, _curvePower);
                var rawValue = _endCapacity + (_startCapacity - _endCapacity) * curved;

                _table[i] = Mathf.RoundToInt(rawValue);
            }

            if (maxLevel >= 2)
                _capacityStep = _table[0] - _table[1];
            else
                _capacityStep = 0f;
        }

#if UNITY_EDITOR
        private void DrawLabelForListElement(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
#endif
    }
}
