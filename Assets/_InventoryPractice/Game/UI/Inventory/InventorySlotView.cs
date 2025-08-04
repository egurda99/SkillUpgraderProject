using _InventoryPractice.Game;
using InventoryPractice;
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
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] private Button _button;


        private InventoryItem _item;
        private Inventory _inventory;

        public int SlotIndex { get; private set; }


        public void SetItem(InventoryItem item)
        {
            _item = item;
        }

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_item == null)
                return;

            DragController.Instance.StartDrag(_item, _icon.sprite);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Следование мыши делает DragItemView
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragController.Instance.EndDrag();
        }


        public void SetSlotIndex(int index)
        {
            SlotIndex = index;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"<color=red>Has item : {DragController.Instance.HasItem}</color>");

            if (!DragController.Instance.HasItem)
                return;

            var draggedItem = DragController.Instance.DraggedItem;
            _inventory.HandleDrop(draggedItem, SlotIndex);
            Debug.Log($"<color=red>HandleDrop : {draggedItem} , {SlotIndex}</color>");

            if (DragController.Instance.HasItem)
                DragController.Instance.EndDrag();
        }


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetAmount(string value)
        {
            _amountText.text = value;
        }

        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }
    }
}
