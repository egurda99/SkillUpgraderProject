using System;
using DG.Tweening;
using InventoryPractice;
using MyCodeBase.UI;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class EquipmentView : MonoBehaviour, IEquipmentView
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private EquipmentSlotView _helmetSlotView;
        [SerializeField] private EquipmentSlotView _armorSlotView;
        [SerializeField] private EquipmentSlotView _handOneSlotView;
        [SerializeField] private EquipmentSlotView _handSecondSlotView;
        [SerializeField] private EquipmentSlotView _bootsSlotView;


        private Tween _showTween;
        private Tween _hideTween;
        private DoTweenAnimationManager _dotweenAnimationManager;

        public void InitDotween(DoTweenAnimationManager doTweenAnimationManager)
        {
            _dotweenAnimationManager = doTweenAnimationManager;
        }


        public void Show()
        {
            KillTweens();

            _canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * 0.8f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _showTween = _dotweenAnimationManager.FadeInWithScale(_canvasGroup, transform, 0f, 0.4f)
                .OnComplete(() => _canvasGroup.alpha = 1);
        }

        public void Hide()
        {
            KillTweens();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _hideTween = _dotweenAnimationManager.FadeOut(_canvasGroup, 0.5f);
        }

        private void KillTweens()
        {
            _showTween?.Kill();
            _hideTween?.Kill();
            _showTween = null;
            _hideTween = null;
        }

        public IEquipmentSlotView GetSlotView(EquipType type, int index)
        {
            return type switch
            {
                EquipType.Helmet => _helmetSlotView,
                EquipType.Armor => _armorSlotView,
                EquipType.Boots => _bootsSlotView,
                EquipType.Hand => index == 0 ? _handOneSlotView : _handSecondSlotView,
                _ => null
            };
        }

        public IEquipmentSlotView[] GetSlotViews(EquipType type)
        {
            return type switch
            {
                EquipType.Helmet => new[] { _helmetSlotView },
                EquipType.Armor => new[] { _armorSlotView },
                EquipType.Boots => new[] { _bootsSlotView },
                EquipType.Hand => new[] { _handOneSlotView, _handSecondSlotView },
                _ => Array.Empty<IEquipmentSlotView>()
            };
        }

        private void OnDestroy()
        {
            KillTweens();
        }
    }
}
