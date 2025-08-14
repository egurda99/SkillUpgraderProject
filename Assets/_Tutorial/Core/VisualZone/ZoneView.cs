using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class ZoneView : MonoBehaviour
    {
        [SerializeField] private Sprite _rectangleSprite;
        [SerializeField] private Sprite _circleSprite;


        private SpriteRenderer _spriteRenderer;

        private readonly Vector2 _rectangleSize = new(10, 10);
        private readonly Vector2 _circleSize = new(7, 7);


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

        private void OnEnable()
        {
            _colorTween?.Kill();
        }

        [Button]
        public void ShowRectangle()
        {
            _spriteRenderer.sprite = _rectangleSprite;
            SetSize(_rectangleSize.x, _rectangleSize.y);

            gameObject.SetActive(true);
            _colorTween?.Kill();
            _colorTween = _spriteRenderer.DOColor(_highlightColor, _animationDuration).SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        [Button]
        public void ShowCircle()
        {
            _spriteRenderer.sprite = _circleSprite;
            SetSize(_circleSize.x, _circleSize.y);

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
            _spriteRenderer.size = new Vector2(width, length);
        }
    }
}
