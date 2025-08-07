using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyCodeBase.UI
{
    public static class DoTweenAnimationManager
    {
        public static Tween FadeInWithScale(CanvasGroup canvasGroup, Transform transform, float fadeDuration,
            float scaleDuration)
        {
            canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * 0.8f;

            return DOTween.Sequence()
                .Append(canvasGroup.DOFade(1f, fadeDuration))
                .Join(transform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack))
                .SetUpdate(true);
        }

        public static Tween FadeOutWithScale(CanvasGroup canvasGroup, Transform transform, float scaleDuration = 0.3f,
            float fadeDuration = 0.1f, Action onComplete = null)
        {
            return DOTween.Sequence()
                .Join(transform.DOScale(0f, scaleDuration).SetEase(Ease.InBack))
                .Append(canvasGroup.DOFade(0f, fadeDuration))
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        public static Tween FadeIn(CanvasGroup canvasGroup, float duration, float targetAlpha = 1f)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            return canvasGroup
                .DOFade(targetAlpha, duration)
                .SetUpdate(true);
        }

        public static Tween FadeOut(CanvasGroup canvasGroup, float duration, float targetAlpha = 0f,
            Action onComplete = null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            return canvasGroup
                .DOFade(targetAlpha, duration)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        public static Tween StartHighlight(Image background, Image icon, Color highlightColor, Color normalColor,
            float duration)
        {
            return DOTween.Sequence()
                .Append(background.DOColor(highlightColor, duration))
                .Join(icon.DOColor(highlightColor, duration))
                .Append(background.DOColor(normalColor, duration))
                .Join(icon.DOColor(normalColor, duration))
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }

        public static void StopHighlight(Image background, Image icon, Color normalColor, float duration)
        {
            background.DOColor(normalColor, duration).SetUpdate(true);
            icon.DOColor(normalColor, duration).SetUpdate(true);
        }

        public static Tween ChangeColor(Image background, Image icon, Color toColor, float duration)
        {
            return DOTween.Sequence()
                .Append(background.DOColor(toColor, duration))
                .Join(icon.DOColor(toColor, duration))
                .SetUpdate(true);
        }

        public static void DoWiggle(Transform iconTransform)
        {
            if (!iconTransform.gameObject.activeInHierarchy)
                return;

            iconTransform.DOKill();
            iconTransform.localRotation = Quaternion.identity;

            iconTransform
                .DOPunchRotation(new Vector3(0, 0, 20f), 0.3f, 6, 0.6f)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }
    }
}
