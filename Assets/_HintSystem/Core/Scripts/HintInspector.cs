using UnityEngine;
using Zenject;

namespace Game.Hints
{
    public abstract class HintInspector : MonoBehaviour
    {
        [SerializeField] private HintType _hintType;

        protected HintManager HintManager;

        [Inject]
        public void Construct(HintManager hintManager)
        {
            HintManager = hintManager;
        }


        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            HintManager.OnCompleted += OnHintCompleted;

            if (!HintManager.IsCompleted(_hintType))
            {
                OnStartInspect();
            }
        }

        private void OnDestroy()
        {
            HintManager.OnCompleted -= OnHintCompleted;
        }


        protected abstract void OnStartInspect();

        protected abstract void OnFinishInspect();

        protected void CompleteHint()
        {
            HintManager.Complete(_hintType);
        }

        private void OnHintCompleted(HintType hintType)
        {
            if (hintType == _hintType)
            {
                OnFinishInspect();
            }
        }
    }
}
