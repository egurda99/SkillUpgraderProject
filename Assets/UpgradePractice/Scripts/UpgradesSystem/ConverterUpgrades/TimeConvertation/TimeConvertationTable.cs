using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class TimeConvertationTable
    {
        public float TimeStep
        {
            get { return _timeStep; }
        }

        [Space] [InfoBox("Time: Linear Function")] [SerializeField]
        private float _startTime;

        [SerializeField] private float _endTime;

        [ReadOnly] [SerializeField] private float _timeStep;

        [Space]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField]
        private float[] _table;

        public float GetTime(int level)
        {
            var index = Mathf.Clamp(level - 1, 0, _table.Length);
            return _table[index];
        }

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            _table = new float[maxLevel];
            _table[0] = _startTime;
            _table[maxLevel - 1] = _endTime;

            var speedStep = (_endTime - _startTime) / (maxLevel - 1);
            _timeStep = (float)Math.Round(speedStep, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                var speed = _startTime + _timeStep * i;
                _table[i] = (float)Math.Round(speed, 2);
            }
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
