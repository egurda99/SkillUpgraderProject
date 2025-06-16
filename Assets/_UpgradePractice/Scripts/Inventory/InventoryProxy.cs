using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class InventoryProxy : MonoBehaviour
    {
        [SerializeField] private DebugInventory _debugInventory;

        public DebugInventory DebugInventory => _debugInventory;
    }
}
