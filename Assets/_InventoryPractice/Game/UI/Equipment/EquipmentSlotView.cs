using System;
using InventoryPractice;
using MyCodeBase.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    public sealed class EquipmentSlotView : MonoBehaviour, IEquipmentSlotView, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler
    {
        [SerializeField] private ImageEquipmentSlot _imageEquipmentSlot;

        [SerializeField] private Button _button;

        private EquipType _equipType;
        private int _index;


        public event Action<int, EquipType, PointerEventData, IEquipmentSlotView> BeginDragEvent;
        public event Action<PointerEventData, IEquipmentSlotView> EndDragEvent;
        public event Action<int, EquipType, PointerEventData> DropEvent;

        public void InitDotween(DoTweenAnimationManager doTweenAnimationManager)
        {
            _imageEquipmentSlot.InitDotween(doTweenAnimationManager);
        }

        public void SetDragState()
        {
            _imageEquipmentSlot.SetDragState();
        }

        public void SetNormalState()
        {
            _imageEquipmentSlot.SetNormalState();
        }

        public void DoPunchScale()
        {
            _imageEquipmentSlot.DoPunchScale();
        }

        public void SetHighlightedState()
        {
            _imageEquipmentSlot.Highlight();
        }


        public void SetEquipType(EquipType equipType)
        {
            _equipType = equipType;
        }


        public void SetSprite(Sprite sprite)
        {
            _imageEquipmentSlot.SetSprite(sprite);
        }

        public void SetDefaultSprite()
        {
            _imageEquipmentSlot.SetDefaultSprite();
        }


        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
            _button.onClick.AddListener(() => Debug.Log("Clicked"));
        }

        public void RemoveAllButtonListeners()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SetIndex(int i)
        {
            _index = i;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(_index, _equipType, eventData, this);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(eventData, this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(_index, _equipType, eventData);
        }

        private void OnDestroy()
        {
            _imageEquipmentSlot.KillTween();
        }
    }
}
