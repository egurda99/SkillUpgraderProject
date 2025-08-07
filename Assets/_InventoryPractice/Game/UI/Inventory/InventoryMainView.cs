using DG.Tweening;
using MyCodeBase.UI;
using UnityEngine;

namespace _InventoryPractice
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class InventoryMainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private Tween _showTween;
        private Tween _hideTween;

        public void Show()
        {
            KillTweens();

            _canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * 0.8f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _showTween = DoTweenAnimationManager.FadeInWithScale(_canvasGroup, transform, 0f, 0.4f)
                .OnComplete(() => _canvasGroup.alpha = 1);
        }

        public void Hide()
        {
            KillTweens();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _hideTween = DoTweenAnimationManager.FadeOut(_canvasGroup, 0.5f);
        }

        private void KillTweens()
        {
            _showTween?.Kill();
            _hideTween?.Kill();
            _showTween = null;
            _hideTween = null;
        }

        private void OnDestroy()
        {
            KillTweens();
        }
    }
}
