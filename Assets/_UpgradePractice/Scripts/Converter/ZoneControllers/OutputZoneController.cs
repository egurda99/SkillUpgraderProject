using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class OutputZoneController : MonoBehaviour
    {
        [SerializeField] private ConverterInstaller _converterInstaller;
        private ConverterSystem _converterSystem;

        private void Start()
        {
            _converterSystem = _converterInstaller.System;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<InventoryProxy>(out var proxy))
            {
                var inventory = proxy.DebugInventory;

                if (_converterSystem.TryTakeAllOutput(out var items))
                {
                    foreach (var item in items)
                    {
                        inventory.AddItem(item);
                    }
                }
            }
        }
    }
}
