using System;

namespace Lessons.Architecture.PM
{
    public interface IPlayerLevelPresentationModel : IViewModel
    {
        event Action OnStateChanged;

        string GetCurrentLevel();
        string GetCurrentXp();
        string GetRequiredXp();
        bool CanUpgrade();

        void OnUpgradeClicked();
    }
}