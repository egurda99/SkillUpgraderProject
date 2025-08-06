using System;
using UnityEngine;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [Serializable]
    public sealed class ImageInventorySlot
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;

        [SerializeField] private Color _onDragColor;

        [SerializeField] private Color _normalColor;

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }

        public void SetDragState()
        {
            _background.color = _onDragColor;
            _icon.color = _onDragColor;
        }

        public void SetNormalState()
        {
            _background.color = _normalColor;
            _icon.color = _normalColor;
        }
    }
}
