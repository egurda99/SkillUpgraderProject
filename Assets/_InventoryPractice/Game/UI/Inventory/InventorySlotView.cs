using System;
using Coffee.UIExtensions;
using MyCodeBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    public sealed class InventorySlotView : MonoBehaviour, IInventorySlotView, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler
    {
        [SerializeField] private ImageInventorySlot _imageInventorySlot;


        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] private Button _button;

        [SerializeField] private UIParticle _uiParticle;


        public void SetNormalState()
        {
            _imageInventorySlot.SetNormalState();
        }

        public event Action<PointerEventData> BeginDragEvent;
        public event Action<PointerEventData> EndDragEvent;
        public event Action<PointerEventData> DropEvent;

        public void DoWiggleEffect()
        {
            _imageInventorySlot.DoWiggle();
            _uiParticle.Play();
        }

        public void DoPunchScaleEffect()
        {
            _imageInventorySlot.DoPunchScaleEffect();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(eventData);
        }


        public void InitDotween(DoTweenAnimationManager doTweenAnimationManager)
        {
            _imageInventorySlot.InitDotween(doTweenAnimationManager);
        }

        public void SetSprite(Sprite sprite)
        {
            _imageInventorySlot.SetSprite(sprite);
        }

        public void SetAmount(string value)
        {
            _amountText.text = value;
            _amountText.enabled = !string.IsNullOrEmpty(value);
        }

        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void SetDragState()
        {
            _imageInventorySlot.SetDragState();
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        private void OnDestroy()
        {
            _imageInventorySlot.KillTween();
        }
    }
}
