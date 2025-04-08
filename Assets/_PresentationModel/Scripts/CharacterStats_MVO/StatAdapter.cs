using System;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatAdapter : IInitializable, IDisposable
    {
        private readonly CharacterStat _characterStat;
        private readonly StatView _statView;

        public StatAdapter(CharacterStat characterStat, StatView statView)
        {
            _characterStat = characterStat;
            _statView = statView;
        }

        public void Initialize()
        {
            _characterStat.OnValueChanged += OnValueChanged;
            _statView.SetupStat(_characterStat.Name, _characterStat.Value.ToString());
        }

        public void Dispose()
        {
            _characterStat.OnValueChanged -= OnValueChanged;
        }

        public void Show()
        {
            _statView.SetupStat(_characterStat.Name, _characterStat.Value.ToString());
            _statView.Show();
        }

        public void Hide()
        {
            _statView.Hide();
        }


        private void OnValueChanged(int value)
        {
            _statView.UpdateStatValue(value.ToString());
        }
    }
}
