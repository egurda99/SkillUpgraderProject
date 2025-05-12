using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupViewModel : IPopupViewModel, IDisposable
    {
        private readonly CharacterStatsSectionViewModel _characterStatsSection;
        private readonly PlayerLevelSectionViewModel _playerLevelSection;
        private readonly UserInfoSectionViewModel _userInfoSection;


        private readonly List<ISectionViewModel> _sections = new();

        public PlayerPopupViewModel(PlayerPopupSectionsFactory factory)
        {
            _characterStatsSection = factory.CreateCharacterStatsSection();
            _playerLevelSection = factory.CreatePlayerLevelSection();
            _userInfoSection = factory.CreateUserInfoSection();
            _sections.Add(_characterStatsSection);
            _sections.Add(_playerLevelSection);
            _sections.Add(_userInfoSection);
        }

        public void Show()
        {
            for (var index = 0; index < _sections.Count; index++)
            {
                var section = _sections[index];
                section.Show();
            }
        }

        public void Hide()
        {
            for (var index = 0; index < _sections.Count; index++)
            {
                var section = _sections[index];
                section.Hide();
            }
        }

        public void Dispose()
        {
            for (var index = 0; index < _sections.Count; index++)
            {
                var section = _sections[index];
                section.Dispose();
            }
        }
    }
}
