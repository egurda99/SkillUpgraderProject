using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyCodeBase.UI
{
    public sealed class InfoPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Image _iconImage;

        [Header("Animation Settings")] [SerializeField]
        private float _showDuration = 0.6f;

        [SerializeField] private float _pulseScale = 1.1f;
        [SerializeField] private float _pulseDuration = 0.4f;

        private Vector3 _startScale;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        public void ShowWithAnimation()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOKill(); // отменяем прошлые анимации, если есть

            // Плавное появление с небольшой упругостью
            transform.DOScale(_startScale, _showDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    // Пульс для привлечения внимания
                    transform.DOScale(_startScale * _pulseScale, _pulseDuration)
                        .SetLoops(2, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                });
        }


        public void HideWithAnimation(float hideDuration = 0.3f)
        {
            transform.DOKill();
            transform.DOScale(Vector3.zero, hideDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
