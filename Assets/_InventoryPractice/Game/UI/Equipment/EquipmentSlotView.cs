using InventoryPractice;
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
        [SerializeField] private Image _icon;
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Button _button;

        private EquipType _equipType;
        private int _index;

        public event UnityAction<int, EquipType, PointerEventData> BeginDragEvent;
        public event UnityAction<PointerEventData> EndDragEvent;
        public event UnityAction<int, EquipType, PointerEventData> DropEvent;


        public void SetEquipType(EquipType equipType)
        {
            _equipType = equipType;
        }


        public void SetSprite(Sprite sprite)
        {
            _defaultIcon.enabled = false;
            _icon.enabled = true;
            _icon.sprite = sprite;
        }

        public void SetDefaultSprite()
        {
            _defaultIcon.enabled = true;
            _icon.enabled = false;
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
            BeginDragEvent?.Invoke(_index, _equipType, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(_index, _equipType, eventData);
        }
    }
}
