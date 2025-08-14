using DG.Tweening;
using UnityEngine;

namespace _Tutorial.Core
{
    public sealed class TutorialCursor : MonoBehaviour
    {
        [SerializeField] private float _moveOffset = 0.2f; // Насколько смещается
        [SerializeField] private float _moveDuration = 0.5f; // Время на одно движение
        [SerializeField] private float _pulseScale = 1.1f; // Насколько увеличивается
        [SerializeField] private float _pulseDuration = 0.4f;


        private RectTransform _rectTransform;
        private Vector2 _startPos;
        private Vector3 _startScale;
        private Tween _animTween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPos = _rectTransform.anchoredPosition;
            _startScale = _rectTransform.localScale;
        }

        private void OnEnable()
        {
            PlayClickAnimation();
        }

        private void OnDisable()
        {
            _animTween?.Kill();
            _rectTransform.anchoredPosition = _startPos;
            _rectTransform.localScale = _startScale;
        }

        private void PlayClickAnimation()
        {
            _animTween = DOTween.Sequence()
                // Движение вниз (имитация клика) + лёгкий зум
                .Append(_rectTransform.DOAnchorPos(_startPos + new Vector2(0, -_moveOffset), _moveDuration)
                    .SetEase(Ease.InOutSine))
                .Join(_rectTransform.DOScale(_startScale * _pulseScale, _pulseDuration).SetEase(Ease.InOutSine))
                // Возврат в исходное положение + масштаб
                .Append(_rectTransform.DOAnchorPos(_startPos, _moveDuration).SetEase(Ease.InOutSine))
                .Join(_rectTransform.DOScale(_startScale, _pulseDuration).SetEase(Ease.InOutSine))
                // Бесконечный повтор
                .SetLoops(-1);
        }
    }
}
