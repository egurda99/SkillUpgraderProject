using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class ZoneView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        [Header("Animation settings")] [SerializeField]
        private Color _highlightColor = new(1f, 1f, 1f, 0.5f);

        [SerializeField] private float _animationDuration = 0.5f;

        private Color _originalColor;
        private Tween _colorTween;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _originalColor = _spriteRenderer.color;
        }

        [Button]
        public void Show()
        {
            gameObject.SetActive(true);
            _colorTween?.Kill();
            _colorTween = _spriteRenderer.DOColor(_highlightColor, _animationDuration).SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        [Button]
        public void Hide()
        {
            _colorTween?.Kill();
            _colorTween = _spriteRenderer.DOColor(_originalColor, _animationDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void HideInstant()
        {
            _colorTween?.Kill();
            _spriteRenderer.color = _originalColor;
            gameObject.SetActive(false);
        }

        public void SetSize(float width, float length)
        {
            transform.localScale = new Vector3(width, 1f, length);
        }
    }
}
