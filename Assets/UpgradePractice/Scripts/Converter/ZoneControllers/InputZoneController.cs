using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class InputZoneController : MonoBehaviour
    {
        [SerializeField] private ConverterInstaller _converterInstaller;
        private ConverterSystem _converterSystem;

        private void Start()
        {
            _converterSystem = _converterInstaller.System;
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (!other.TryGetComponent<InventoryProxy>(out var proxy))
        //         return;
        //
        //     var inventory = proxy.DebugInventory;
        //
        //     TryTransfer(ResourceType.Wood, inventory);
        //     //  TryTransfer(ResourceType.Lumber, inventory);
        // }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<InventoryProxy>(out var proxy))
                return;

            var inventory = proxy.DebugInventory;

            TryTransfer(ResourceType.Wood, inventory);
        }


        private void TryTransfer(ResourceType type, IInventory inventory)
        {
            while (_converterSystem.HasInputSpace())
            {
                var item = inventory.PeekItem(type);
                if (item == null || item.Amount <= 0)
                    break;

                var oneUnit = new ResourceItem(type, 1);
                var added = _converterSystem.TryAddInput(oneUnit);

                if (added)
                {
                    inventory.DecreaseItem(type, 1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
