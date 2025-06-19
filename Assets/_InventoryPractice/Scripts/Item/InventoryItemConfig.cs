using UnityEngine;

namespace InventoryPractice
{
    [CreateAssetMenu(
        fileName = "InventoryItemConfig",
        menuName = "Lesson/Inventory/New InventoryItemConfig"
    )]
    public sealed class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem PrototypeItem;
    }
}
