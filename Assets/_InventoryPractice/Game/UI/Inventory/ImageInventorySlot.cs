using System;
using DG.Tweening;
using MyCodeBase.UI;
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
        private DoTweenAnimationManager _dotweenAnimationManager;

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }

        public void InitDotween(DoTweenAnimationManager doTweenAnimationManager)
        {
            _dotweenAnimationManager = doTweenAnimationManager;
        }

        public void SetDragState()
        {
            KillTween();
            _colorTween = _dotweenAnimationManager.ChangeColor(_background, _icon, _onDragColor, _transitionDuration);

            // _colorTween = DOTween.Sequence()
            //     .Append(_background.DOColor(_onDragColor, _transitionDuration))
            //     .Join(_icon.DOColor(_onDragColor, _transitionDuration))
            //     .SetUpdate(true);
        }

        public void SetNormalState()
        {
            KillTween();

            // _colorTween = DOTween.Sequence()
            //     .Append(_background.DOColor(_normalColor, _transitionDuration))
            //     .Join(_icon.DOColor(_normalColor, _transitionDuration))
            //     .SetUpdate(true);

            _colorTween = _dotweenAnimationManager.ChangeColor(_background, _icon, _normalColor, _transitionDuration);
        }

        public void KillTween()
        {
            if (_colorTween != null && _colorTween.IsActive())
            {
                _colorTween.Kill();
                _colorTween = null;
            }
        }

        public void DoWiggle()
        {
            if (_icon == null || !_icon.gameObject.activeInHierarchy)
                return;

            // ��������, ��� �� ����������� �����
            _icon.transform.DOKill();

            // ����� �������� ����� ���������
            _icon.transform.localRotation = Quaternion.identity;

            _dotweenAnimationManager.DoWiggle(_icon.transform);
        }

        public void DoPunchScaleEffect()
        {
            if (_icon == null || !_icon.gameObject.activeInHierarchy)
                return;

            // ��������, ��� �� ����������� �����
            _icon.transform.DOKill();

            // ����� �������� ����� ���������
            _icon.transform.localRotation = Quaternion.identity;

            _dotweenAnimationManager.DoPunchScale(_icon.transform);

            // // ��������� ������������ "����"
            // _icon.transform
            //     .DOPunchRotation(
            //         new Vector3(0, 0, 20f), // ��������� "�����" �� Z
            //         0.3f, // ������������
            //         6, // ���������� ��������
            //         0.6f) // ������������ (0..1)
            //     .SetEase(Ease.OutQuad)
            //     .SetUpdate(true);
        }
    }
}
