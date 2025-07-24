using System;
using _UpgradePractice.Scripts;
using BehaviorDesigner.Runtime;
using Zenject;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class BackpackObserver : IInitializable, IDisposable
    {
        private readonly BehaviorTree _behaviorTree;

        private bool _isInitialized;
        private readonly IInventory _inventory;


        public BackpackObserver(IInventory inventory, BehaviorTree behaviorTree)
        {
            _inventory = inventory;
            _behaviorTree = behaviorTree;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            _inventory.OnBackpackFilledStateChanged += OnBackpackFilledStateChanged;
        }

        private void OnBackpackFilledStateChanged(bool value)
        {
            _behaviorTree.SetVariableValue(IS_BACKPACK_FULL, value);
        }


        public void Dispose()
        {
            _inventory.OnBackpackFilledStateChanged -= OnBackpackFilledStateChanged;
        }
    }
}
