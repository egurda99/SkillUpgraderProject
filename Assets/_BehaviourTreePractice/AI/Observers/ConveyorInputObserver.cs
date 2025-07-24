using System;
using _UpgradePractice.Scripts;
using BehaviorDesigner.Runtime;
using Zenject;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class ConveyorInputObserver : IInitializable, IDisposable
    {
        private bool _isInitialized;
        private readonly IInventory _inventory;
        private readonly ConverterInstaller _converterInstaller;


        public ConveyorInputObserver(ConverterInstaller converterInstaller)
        {
            _converterInstaller = converterInstaller;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            _converterInstaller.System.OnInputChanged += OnInputChanged;
        }

        private void OnInputChanged(int value)
        {
            if (value >= _converterInstaller.View.Data.InputZoneCapacity)
            {
                var isConveyorFull = (SharedBool)GlobalVariables.Instance.GetVariable(IS_CONVEYOR_FULL);
                isConveyorFull.Value = true;
            }

            else
            {
                var isConveyorFull = (SharedBool)GlobalVariables.Instance.GetVariable(IS_CONVEYOR_FULL);
                isConveyorFull.Value = false;
            }
        }

        public void Dispose()
        {
            _converterInstaller.System.OnInputChanged -= OnInputChanged;
        }
    }
}
