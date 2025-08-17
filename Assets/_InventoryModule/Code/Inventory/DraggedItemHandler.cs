using UnityEngine;

namespace InventoryPractice
{
    public sealed class DraggedItemHandler
    {
        private readonly Inventory _inventory;

        public DraggedItemHandler(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void HandleDraggedItem(InventoryItem draggedItem, int targetIndex)
        {
            if (targetIndex < 0 || targetIndex >= _inventory.SlotsLimit)
            {
                Debug.LogWarning("Неверный индекс слота");
                return;
            }

            // Перетаскиваем из слота в слот
            var currentIndex = _inventory.Items.IndexOf(draggedItem);

            if (currentIndex == targetIndex)
                return;

            // Если предмет уже был в инвентаре — переместить
            if (currentIndex != -1)
            {
                var previousItem = _inventory.Items[targetIndex];

                if (_inventory.Items[currentIndex].Id == "null")
                {
                    _inventory.Items[currentIndex] = _inventory.CreateNullableItem();
                }

                else
                {
                    _inventory.Items[currentIndex] = previousItem;
                }

                _inventory.Items[targetIndex] = draggedItem;

                if (previousItem.Id != "null")
                {
                    _inventory.FireItemsChangedEventByDragAndDrop(targetIndex, currentIndex);
                    return;
                }


                _inventory.FireItemsChangedEventByDragAndDrop(targetIndex);
            }
            else
            {
                // Если добавляется новый предмет
                if (_inventory.Items[targetIndex].Id == "null")
                {
                    _inventory.Items[targetIndex] = draggedItem;
                    //   _inventory.OnItemAdded?.Invoke(draggedItem);
                    _inventory.FireItemsChangedEvent();
                }
                else
                {
                    Debug.LogWarning("Слот занят, не удалось вставить предмет");
                }
            }
        }
    }
}
