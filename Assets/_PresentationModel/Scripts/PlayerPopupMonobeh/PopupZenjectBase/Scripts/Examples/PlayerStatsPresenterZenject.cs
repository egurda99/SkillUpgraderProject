using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPresenterZenject : MonoBehaviour
    {
        [SerializeField] private PlayerStatsPassiveView _view;

        private PlayerLevel _playerLevel;

        [Inject]
        public void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void Show()
        {
            _view.SetStats($"Level: {_playerLevel.CurrentLevelProperty.CurrentValue}\n" +
                           $"XP: {_playerLevel.CurrentExperienceProperty.CurrentValue} / {_playerLevel.RequiredExperience}");
        }

        public void Hide()
        {
        }
    }
}
