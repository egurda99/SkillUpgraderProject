using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public sealed class TutorialStarter : MonoBehaviour
    {
        [SerializeField] private int _stepIndex;
        [SerializeField] private bool _isCompleted;
        [ShowInInspector] [ReadOnly] private TutorialManager _tutorialManager;


        [Inject]
        public void Construct(TutorialManager manager)
        {
            _tutorialManager = manager;
            _tutorialManager.Initialize(_isCompleted, _stepIndex);
        }

        // private void Start()
        // {
        //     _tutorialManager.Initialize(_isCompleted, _stepIndex);
        // }

        [Button]
        public void SetStep(TutorialStep step)
        {
            _tutorialManager.SetStep(step);
        }
    }
}
