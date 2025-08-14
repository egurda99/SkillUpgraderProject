using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class TutorialManager
    {
        public event Action<TutorialStep> OnStepFinished;

        public event Action<TutorialStep> OnNextStep;

        public event Action OnCompleted;

        [ShowInInspector]
        [ReadOnly]
        public bool IsCompleted
        {
            get { return _isCompleted; }
        }

        [ShowInInspector]
        [ReadOnly]
        public TutorialStep CurrentStep
        {
            get { return _stepList[_currentIndex]; }
        }

        [ShowInInspector]
        [ReadOnly]
        public int CurrentIndex
        {
            get { return _currentIndex; }
        }

        private readonly TutorialList _stepList;

        private int _currentIndex;

        private bool _isCompleted;


        public TutorialManager(TutorialList stepList)
        {
            _stepList = stepList;
        }


        public void Initialize(bool isCompleted = false, int stepIndex = 0)
        {
            _isCompleted = isCompleted;
            _currentIndex = Mathf.Clamp(stepIndex, 0, _stepList.LastIndex);
        }

        public void FinishCurrentStep()
        {
            if (!_isCompleted)
            {
                OnStepFinished?.Invoke(CurrentStep);
            }
        }

        public void MoveToNextStep()
        {
            if (_isCompleted)
            {
                return;
            }

            if (_stepList.IsLast(_currentIndex))
            {
                _isCompleted = true;
                OnCompleted?.Invoke();
                return;
            }

            _currentIndex++;
            OnNextStep?.Invoke(CurrentStep);
        }

        public bool IsStepPassed(TutorialStep step)
        {
            if (_isCompleted)
            {
                return true;
            }

            return _stepList.IndexOf(step) < _currentIndex;
        }

        public int IndexOfStep(TutorialStep step)
        {
            return _stepList.IndexOf(step);
        }

        public void SetStep(TutorialStep step)
        {
            _currentIndex = IndexOfStep(step);
            OnNextStep?.Invoke(CurrentStep);
        }
    }
}
