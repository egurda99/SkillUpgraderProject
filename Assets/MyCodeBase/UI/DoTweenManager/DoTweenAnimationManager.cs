using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyCodeBase.UI
{
    public sealed class DoTweenAnimationManager
    {
        public Tween FadeInWithScale(CanvasGroup canvasGroup, Transform transform, float fadeDuration,
            float scaleDuration)
        {
            canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * 0.8f;

            return DOTween.Sequence()
                .Append(canvasGroup.DOFade(1f, fadeDuration))
                .Join(transform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack))
                .SetUpdate(true);
        }

        public Tween FadeOutWithScale(CanvasGroup canvasGroup, Transform transform, float scaleDuration = 0.3f,
            float fadeDuration = 0.1f, Action onComplete = null)
        {
            return DOTween.Sequence()
                .Join(transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack))
                .Append(canvasGroup.DOFade(0f, fadeDuration))
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        public Tween FadeIn(CanvasGroup canvasGroup, float duration, float targetAlpha = 1f)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            return canvasGroup
                .DOFade(targetAlpha, duration)
                .SetUpdate(true);
        }

        public Tween FadeOut(CanvasGroup canvasGroup, float duration, float targetAlpha = 0f,
            Action onComplete = null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            return canvasGroup
                .DOFade(targetAlpha, duration)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        public Tween StartHighlight(Image background, Image icon, Color highlightColor, Color normalColor,
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

        public void StopHighlight(Image background, Image icon, Color normalColor, float duration)
        {
            background.DOColor(normalColor, duration).SetUpdate(true);
            icon.DOColor(normalColor, duration).SetUpdate(true);
        }

        public Tween ChangeColor(Image background, Image icon, Color toColor, float duration)
        {
            return DOTween.Sequence()
                .Append(background.DOColor(toColor, duration))
                .Join(icon.DOColor(toColor, duration))
                .SetUpdate(true);
        }

        public void DoWiggle(Transform target, Graphic raycastBlocker = null, Button button = null,
            CanvasGroup canvasGroup = null)
        {
            if (!target.gameObject.activeInHierarchy)
                return;

            // Отключаем взаимодействие
            if (raycastBlocker != null)
                raycastBlocker.raycastTarget = false;
            if (button != null)
                button.interactable = false;
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = false;

            target.DOKill();
            target.localRotation = Quaternion.identity;

            target
                .DOPunchRotation(new Vector3(0, 0, 20f), 0.3f, 6, 0.6f)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    // Возвращаем взаимодействие
                    if (raycastBlocker != null)
                        raycastBlocker.raycastTarget = true;
                    if (button != null)
                        button.interactable = true;
                    if (canvasGroup != null)
                        canvasGroup.blocksRaycasts = true;
                });
        }

        public void DoPunchScale(Transform iconTransform)
        {
            if (!iconTransform.gameObject.activeInHierarchy)
                return;

            iconTransform.DOKill();
            iconTransform.localRotation = Quaternion.identity;
            iconTransform.localScale = Vector3.one;

            iconTransform.transform.DOPunchScale(
                Vector3.one * 0.2f, // амплитуда "удара"
                0.25f, // длительность
                8, // вибрации
                0.8f // эластичность
            ).SetEase(Ease.OutBack).SetUpdate(true);
        }

        public Tween StartIdleScale(Graphic uiElement, float scaleMultiplier = 1.1f, float duration = 0.5f)
        {
            if (uiElement == null || uiElement.transform == null)
                return null;

            // Убиваем все твины, чтобы не накапливались
            uiElement.transform.DOKill();

            // Сохраняем исходный масштаб
            var startScale = uiElement.transform.localScale;

            // Делаем бесконечную анимацию увеличения и уменьшения
            return uiElement.transform
                .DOScale(startScale * scaleMultiplier, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true);
        }
    }
}
