using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class WorkerAnimationHandler
    {
        private static readonly int State = Animator.StringToHash("State");
        private readonly Animator _animator;

        public WorkerAnimationHandler(Animator animator)
        {
            _animator = animator;
        }

        public void StartWork()
        {
            _animator.SetInteger(State, 1);
        }

        public void StopWork()
        {
            _animator.SetInteger(State, 0);
        }
    }
}
