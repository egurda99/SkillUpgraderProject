using System;
using DG.Tweening;
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


        [SerializeField] private float _transitionDuration = 0.45f;

        private Tween _colorTween;

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }

        // public void SetDragState()
        // {
        //     _background.color = _onDragColor;
        //     _icon.color = _onDragColor;
        // }
        //
        // public void SetNormalState()
        // {
        //     _background.color = _normalColor;
        //     _icon.color = _normalColor;
        // }

        public void SetDragState()
        {
            KillTween();

            _colorTween = DOTween.Sequence()
                .Append(_background.DOColor(_onDragColor, _transitionDuration))
                .Join(_icon.DOColor(_onDragColor, _transitionDuration))
                .SetUpdate(true);
        }

        public void SetNormalState()
        {
            KillTween();

            _colorTween = DOTween.Sequence()
                .Append(_background.DOColor(_normalColor, _transitionDuration))
                .Join(_icon.DOColor(_normalColor, _transitionDuration))
                .SetUpdate(true);
        }

        public void KillTween()
        {
            if (_colorTween != null && _colorTween.IsActive())
            {
                _colorTween.Kill();
                _colorTween = null;
            }
        }
    }
}
