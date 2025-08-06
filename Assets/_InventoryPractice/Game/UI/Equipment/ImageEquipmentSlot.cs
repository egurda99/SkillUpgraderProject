using System;
using DG.Tweening;
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
        [SerializeField] private Color _equipColor;

        [SerializeField] private Color _normalColor;

        [SerializeField] private float _transitionDuration = 0.25f;

        private Tween _highlightTween;

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

            StopHighlight();
        }

        public void Highlight()
        {
            KillTween();

            _highlightTween = DOTween.Sequence()
                .Append(_background.DOColor(_equipColor, _transitionDuration))
                .Join(_icon.DOColor(_equipColor, _transitionDuration))
                .Append(_background.DOColor(_normalColor, _transitionDuration))
                .Join(_icon.DOColor(_normalColor, _transitionDuration))
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }

        public void StopHighlight()
        {
            KillTween();

            _background.DOColor(_normalColor, _transitionDuration).SetUpdate(true);
            _icon.DOColor(_normalColor, _transitionDuration).SetUpdate(true);
        }

        public void KillTween()
        {
            if (_highlightTween != null && _highlightTween.IsActive())
                _highlightTween.Kill();
        }
    }
}
