using DG.Tweening;
using MyCodeBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class InventoryItemDetailView : MonoBehaviour, IInventoryItemDetailView
    {
        [SerializeField] [Range(0f, 1f)] private float _fadeDuration;
        [SerializeField] [Range(0f, 1f)] private float _scaleDuration;


        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _weightText;

        [SerializeField] private Button _useButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _unEquipButton;
        [SerializeField] private Button _dropButton;

        private Tween _showTween;
        private Tween _hideTween;
        private DoTweenAnimationManager _dotweenAnimationManager;

        public void Show()
        {
            // gameObject.SetActive(true);

            gameObject.SetActive(true);

            _hideTween?.Kill();
            _showTween?.Kill();
            _showTween =
                _dotweenAnimationManager.FadeInWithScale(_canvasGroup, transform, _fadeDuration, _scaleDuration);
            // _canvasGroup.alpha = 0f;
            // transform.localScale = Vector3.one * 0.8f;
            //
            // _showTween = DOTween.Sequence()
            //     .Append(_canvasGroup.DOFade(1f, _fadeDuration))
            //     .Join(transform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack))
            //     .SetUpdate(true);
        }

        public void Hide()
        {
            if (!IsValid())
                return;

            _hideTween = _dotweenAnimationManager.FadeOutWithScale(_canvasGroup, transform, 0.3f, 0.1f, () =>
            {
                if (IsValid())
                    gameObject.SetActive(false);
            });

            // _hideTween = DOTween.Sequence()
            //     .Join(transform.DOScale(0.0f, 0.3f).SetEase(Ease.InBack))
            //     .Append(_canvasGroup.DOFade(0f, 0.1f))
            //     .SetUpdate(true)
            //     .OnComplete(() =>
            //     {
            //         if (IsValid()) gameObject.SetActive(false);
            //     });
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        public void SetAmount(string amount)
        {
            _amountText.text = amount;
        }

        public void SetWeight(string weight)
        {
            _weightText.text = weight;
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void ShowUseButton(bool show)
        {
            _useButton.gameObject.SetActive(show);
        }

        public void ShowEquipButton(bool show)
        {
            _equipButton.gameObject.SetActive(show);
        }

        public void ShowUnEquipButton(bool show)
        {
            _unEquipButton.gameObject.SetActive(show);
        }

        public void ShowDropButton(bool show)
        {
            _dropButton.gameObject.SetActive(show);
        }

        public void SetUseActionListener(UnityAction action)
        {
            _useButton.onClick.AddListener(action);
        }

        public void SetEquipActionListener(UnityAction action)
        {
            _equipButton.onClick.AddListener(action);
        }

        public void SetDropActionListener(UnityAction action)
        {
            _dropButton.onClick.AddListener(action);
        }

        public void RemoveUseActionListener(UnityAction action)
        {
            _useButton.onClick.RemoveListener(action);
        }

        public void RemoveEquipActionListener(UnityAction action)
        {
            _equipButton.onClick.RemoveListener(action);
        }

        public void RemoveDropActionListener(UnityAction action)
        {
            _dropButton.onClick.RemoveListener(action);
        }

        public void SetUnEquipActionListener(UnityAction action)
        {
            _unEquipButton.onClick.AddListener(action);
        }

        public void RemoveUnEquipActionListener(UnityAction action)
        {
            _unEquipButton.onClick.RemoveListener(action);
        }

        public void SetTweenManager(DoTweenAnimationManager tweenAnimationManager)
        {
            _dotweenAnimationManager = tweenAnimationManager;
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
            _showTween?.Kill();
            _hideTween?.Kill();
        }

        private bool IsValid()
        {
            return this != null && gameObject != null && transform != null && _canvasGroup != null;
        }
    }
}
