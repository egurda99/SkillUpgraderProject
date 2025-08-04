using _InventoryPractice.Game;
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
        private InventoryItem _item;

        private Equipment _equipment;
        private EquipType _equipType;
        private int _index;


        public void SetItem(InventoryItem item)
        {
            _item = item;
        }

        public void SetEquipment(Equipment equipment)
        {
            _equipment = equipment;
        }

        public void SetEquipType(EquipType equipType)
        {
            _equipType = equipType;
        }


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _defaultIcon.enabled = false;
            _icon.enabled = true;
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
            Debug.Log($"<color=orange>item: {_item}</color>");
            if (_item == null)
                return;

            DragController.Instance.StartDrag(_item, _icon.sprite, DragSourceType.Equipment);
            // DragController.Instance.SetDragFromEquipment(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Следование мыши делает DragItemView
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"<color=orange>item: {_item}</color>");
            DragController.Instance.EndDrag();
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"<color=red>Has item : {DragController.Instance.HasItem}</color>");

            if (!DragController.Instance.HasItem)
                return;

            var draggedItem = DragController.Instance.DraggedItem;
            // _equipment.EquipItemFromDrop(draggedItem, _index, _equipType);
            // Debug.Log($"<color=red>HandleDrop : {draggedItem} , {_equipType}</color>");

            if (DragController.Instance.HasItem)
                DragController.Instance.EndDragAfterSuccessDropAtEquipment(_index, _equipType);
        }
    }
}
