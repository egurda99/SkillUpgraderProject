using System;
using UnityEngine;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [Serializable]
    public sealed class ImageEquipmentSlot
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Image _background;

        [SerializeField] private Color _onDragColor;

        [SerializeField] private Color _normalColor;

        public void SetSprite(Sprite sprite)
        {
            _defaultIcon.enabled = false;
            _icon.enabled = true;
            _icon.sprite = sprite;
        }

        public void SetDefaultSprite()
        {
            _defaultIcon.enabled = true;
            _icon.enabled = false;
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