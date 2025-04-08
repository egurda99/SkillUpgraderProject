using System;
using R3;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class StatAdapter : IInitializable, IDisposable
    {
        private readonly CharacterStat _characterStat;
        private readonly StatView _statView;

        private readonly CompositeDisposable _disposable = new();

        public StatAdapter(CharacterStat characterStat, StatView statView)
        {
            _characterStat = characterStat;
            _statView = statView;
        }

        public void Initialize()
        {
            _characterStat.Value
                .Subscribe(OnValueChanged)
                .AddTo(_disposable);

            _statView.SetupStat(_characterStat.Name, _characterStat.Value.ToString());
        }

        public void Dispose()
        {
            _disposable.Dispose();
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
