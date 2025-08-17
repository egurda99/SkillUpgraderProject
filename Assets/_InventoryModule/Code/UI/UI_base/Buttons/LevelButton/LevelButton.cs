using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyCodeBase
{
    public sealed class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space] [SerializeField] private Image _buttonBackground;

        [SerializeField] private Sprite _availableButtonSprite;

        [SerializeField] private Sprite _lockedButtonSprite;

        [Space] private LevelButtonState _state;

        private Image _priceIcon;

        public Button Button => _button;

        public void AddListener(UnityAction action)
        {
            Button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            Button.onClick.RemoveListener(action);
        }

        public void SetIcon(Sprite icon)
        {
            _priceIcon.sprite = icon;
        }

        public void SetAvailable(bool isAvailable)
        {
            var state = isAvailable ? LevelButtonState.Available : LevelButtonState.Locked;
            SetState(state);
        }

        public void SetState(LevelButtonState state)
        {
            _state = state;

            switch (state)
            {
                case LevelButtonState.Available:
                    Button.interactable = true;
                    _buttonBackground.sprite = _availableButtonSprite;
                    break;
                case LevelButtonState.Locked:
                    Button.interactable = false;
                    _buttonBackground.sprite = _lockedButtonSprite;
                    break;
                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
