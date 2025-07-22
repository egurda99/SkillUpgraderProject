using System;
using _UpgradePractice.Scripts;
using MyTimer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourTreePractice
{
    public sealed class Tree : MonoBehaviour
    {
        [SerializeField] private ResourceItem _resourceItem;
        [SerializeField] private float _durationForTransfer = 1f;
        [ShowInInspector] [ReadOnly] private bool _isTreeOccupied;
        [ShowInInspector] [ReadOnly] private bool _canTransferResource;
        [ShowInInspector] [ReadOnly] private Timer _timer;


        public event Action<Tree> OnTreeDespawned;


        private void Update()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Tick();
        }

        public void Init()
        {
            _timer = new Timer();
            _timer.SetInterval(_durationForTransfer);
            _timer.OnElapsed += OnTimerFinished;
            _timer.Start();
        }

        private void OnTimerFinished()
        {
            _canTransferResource = true;
            _timer.Reset();
            _timer.Stop();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<InventoryProxy>(out var proxy))
                return;

            _isTreeOccupied = true;

            var inventory = proxy.DebugInventory;

            if (_canTransferResource)
            {
                TryTransfer(ResourceType.Wood, inventory);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            _isTreeOccupied = false;
        }


        private void TryTransfer(ResourceType type, IInventory inventory)
        {
            if (_resourceItem.Amount <= 0)
            {
                return;
            }

            if (inventory.IsFull)
            {
                return;
            }


            var oneUnit = new ResourceItem(_resourceItem.Type, 1);
            inventory.AddItem(oneUnit);
            _resourceItem.Amount--;

            if (_resourceItem.Amount <= 0)
            {
                OnTreeDespawned?.Invoke(this);
                Destroy(gameObject);
            }

            _canTransferResource = false;
            _timer.Start();
        }
    }
}
