using R3;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        public ReactiveProperty<int> Value { get; private set; }

        public string Name { get; private set; }

        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = new ReactiveProperty<int>(value);
        }

        public void ChangeValue(int value)
        {
            Value.Value = value;
        }
    }
}
