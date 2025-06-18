using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyCodeBase
{
    public abstract class StateButtonBase<TState> : MonoBehaviour where TState : Enum
    {
        [Serializable]
        protected class StateVisual
        {
            public TState State;
            public Sprite BackgroundSprite;
            public bool Interactable = true;
        }

        [SerializeField] protected Button _button;
        [SerializeField] protected Image _backgroundImage;
        [SerializeField] protected List<StateVisual> _stateVisuals = new();

        private Dictionary<TState, StateVisual> _visualMap;
        [SerializeField] private TState _currentState;

        protected virtual void Awake()
        {
            _visualMap = _stateVisuals.ToDictionary(v => v.State, v => v);
        }

        public void SetState(TState state)
        {
            if (!_visualMap.TryGetValue(state, out var visual))
            {
                Debug.LogError($"[{GetType().Name}] No visuals defined for state: {state}");
                return;
            }

            _currentState = state;


            ApplyVisualState(state, visual);

            _button.interactable = visual.Interactable;
            OnStateApplied(state);
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        public TState GetCurrentState() => _currentState;


        protected virtual void ApplyVisualState(TState state, StateVisual visual)
        {
            _backgroundImage.sprite = visual.BackgroundSprite;
        }

        protected virtual void OnStateApplied(TState state)
        {
        }
    }
}
