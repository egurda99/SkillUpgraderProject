using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Tutorial
{
    public abstract class TutorialStepControllerBase : MonoBehaviour
    {
        [SerializeField] private TutorialStep _step;

        protected TutorialManager TutorialManager;

        [Inject]
        public void Construct(TutorialManager tutorialManager)
        {
            TutorialManager = tutorialManager;
        }


        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            TutorialManager.OnStepFinished += CheckForFinish;
            TutorialManager.OnNextStep += CheckForStart;
            StartGame();
        }

        private void StartGame()
        {
            var stepFinished = TutorialManager.IsStepPassed(_step);
            if (!stepFinished)
            {
                CheckForStart(TutorialManager.CurrentStep);
            }
        }

        private void OnDestroy()
        {
            TutorialManager.OnStepFinished -= CheckForFinish;
            TutorialManager.OnNextStep -= CheckForStart;
        }


        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        protected void NotifyAboutComplete()
        {
            if (TutorialManager.CurrentStep == _step)
            {
                TutorialManager.FinishCurrentStep();
            }
        }

        protected void NotifyAboutMoveNext()
        {
            if (TutorialManager.CurrentStep == _step)
            {
                TutorialManager.MoveToNextStep();
            }
        }

        protected void NotifyAboutCompleteAndMoveNext()
        {
            if (TutorialManager.CurrentStep == _step)
            {
                TutorialManager.FinishCurrentStep();
                TutorialManager.MoveToNextStep();
            }
        }

        [Button]
        protected bool IsStepFinished()
        {
            return TutorialManager.IsStepPassed(_step);
        }

        private void CheckForFinish(TutorialStep step)
        {
            if (_step == step)
            {
                OnStop();
            }
        }

        private void CheckForStart(TutorialStep step)
        {
            if (_step == step)
            {
                OnStart();
            }
        }
    }
}
