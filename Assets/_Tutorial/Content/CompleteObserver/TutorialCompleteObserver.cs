using System;
using _UpgradePractice.Scripts;
using Game.Tutorial;

namespace _Tutorial.Content
{
    public sealed class TutorialCompleteObserver : IDisposable
    {
        private readonly TutorialManager _tutorialManager;
        private readonly UpgradeTriggerPoint _upgradeTriggerPoint;

        public TutorialCompleteObserver(TutorialManager tutorialManager, UpgradeTriggerPoint upgradeTriggerPoint)
        {
            _tutorialManager = tutorialManager;
            _upgradeTriggerPoint = upgradeTriggerPoint;
            _upgradeTriggerPoint.gameObject.SetActive(false);

            _tutorialManager.OnCompleted += OnTutorialCompleted;

            if (_tutorialManager.IsCompleted)
            {
                OnTutorialCompleted();
            }
        }

        private void OnTutorialCompleted()
        {
            _upgradeTriggerPoint.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            _tutorialManager.OnCompleted -= OnTutorialCompleted;
        }
    }
}
