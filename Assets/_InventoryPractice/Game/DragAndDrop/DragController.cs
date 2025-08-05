using System;
using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice.Game
{
    public sealed class DragController : MonoBehaviour
    {
        [SerializeField] private DragItemView _dragItemViewPrefab;
        [SerializeField] private Transform _container;

        public DragSourceType SourceType { get; private set; } = DragSourceType.None;


        private DragItemView _currentView;
        private int _slotIndex;

        public InventoryItem DraggedItem { get; private set; }
        public bool HasItem => DraggedItem != null;

        public int SlotIndex => _slotIndex;

        public event Action<InventoryItem, DragSourceType, int, EquipType, int> OnSuccessDragEventAtEquipment;
        public event Action<InventoryItem, DragSourceType, int> OnSuccessDragEventAtInventory;

        public void StartDrag(InventoryItem item, Sprite icon, DragSourceType source, string amount)
        {
            Debug.Log($"<color=red>Started: {item.Id}</color>");
            SourceType = source;
            DraggedItem = item;
            _currentView = Instantiate(_dragItemViewPrefab, _container);
            _currentView.SetIcon(icon);
            _currentView.SetAmount(amount);
        }

        public void StartDragFromInventory(InventoryItem item, Sprite icon, DragSourceType source, string amount,
            int slotIndex)
        {
            Debug.Log($"<color=red>Started: {item.Id}</color>");
            SourceType = source;
            DraggedItem = item;
            _currentView = Instantiate(_dragItemViewPrefab, _container);
            _currentView.SetIcon(icon);
            _currentView.SetAmount(amount);
            _slotIndex = slotIndex;
        }

        public void EndDrag()
        {
            SourceType = DragSourceType.None;

            DraggedItem = null;
            _slotIndex = -1;
            if (_currentView != null)
                Destroy(_currentView.gameObject);
        }


        public void EndDragAfterSuccessDropAtInventory(int slotIndex)
        {
            Debug.Log($"<color=red>EndedAtInventory: {_currentView}</color>");

            OnSuccessDragEventAtInventory?.Invoke(DraggedItem, SourceType, slotIndex);
            EndDrag();
        }

        public void EndDragAfterSuccessDropAtEquipment(int index, EquipType equipType)
        {
            Debug.Log($"<color=red>EndedAtEquipment: {_currentView}</color>");

            OnSuccessDragEventAtEquipment?.Invoke(DraggedItem, SourceType, index, equipType, _slotIndex);
            EndDrag();
        }
    }
}
