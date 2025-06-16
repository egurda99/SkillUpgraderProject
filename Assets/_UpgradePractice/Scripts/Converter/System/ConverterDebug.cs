using Sirenix.OdinInspector;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public class ConverterDebug : MonoBehaviour
    {
        [SerializeField] private ConverterInstaller _converterInstaller;
        private ConverterSystem _system;

        private void Start()
        {
            _system = _converterInstaller.System;
        }

        [Button]
        public void AddItem(ResourceItem item)
        {
            _system.TryAddInput(item);
        }
    }
}
