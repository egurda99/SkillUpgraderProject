using R3;

namespace Lessons.Architecture.PM
{
    public interface IPlayerLevelPresentationModel : IViewModel
    {
        Observable<Unit> OnStateChanged { get; }

        string GetCurrentLevel();
        string GetCurrentXp();
        string GetRequiredXp();
        bool CanUpgrade();

        void OnUpgradeClicked();
    }
}
