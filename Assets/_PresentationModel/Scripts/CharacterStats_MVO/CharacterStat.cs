using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged;

        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        [ShowInInspector] [ReadOnly] public string Name { get; private set; }

        [ShowInInspector] [ReadOnly] public int Value { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}
