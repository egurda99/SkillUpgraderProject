using System;
using _UpgradePractice.Scripts;
using Atomic.Entities;
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
        public event Action<bool> OnTreeOccupiedStatusChanged;

        public bool IsTreeOccupied => _isReserved;

        private bool _isReserved;
        [ShowInInspector] [ReadOnly] private string _reservedBy;

        public Transform Transform => transform;

        public bool TryReserve(string reserver)
        {
            if (_isReserved && _reservedBy == reserver)
                return true;

            if (_isReserved)
                return false;

            _isReserved = true;
            _isTreeOccupied = true;
            _reservedBy = reserver;
            return true;
        }

        public void Release()
        {
            _isReserved = false;
            _isTreeOccupied = false;
            _reservedBy = null;
        }

        private void Update()
        {
            _timer?.Tick();
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

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<InventoryProxy>(out var proxy))
                return;
            if (!other.TryGetComponent<SceneEntityProxy>(out var entityProxy))
                return;

            if (entityProxy.GetEntityID().Value == _reservedBy)
                Release();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<InventoryProxy>(out var proxy))
                return;
            if (!other.TryGetComponent<SceneEntityProxy>(out var entityProxy))
                return;

            if (entityProxy.GetEntityID().Value != _reservedBy)
                return;

            var inventory = proxy.DebugInventory;

            if (_canTransferResource)
            {
                TryTransfer(ResourceType.Wood, inventory);
            }
        }

        private void TryTransfer(ResourceType type, IInventory inventory)
        {
            if (_resourceItem.Amount <= 0 || inventory.IsFull) return;

            inventory.AddItem(new ResourceItem(_resourceItem.Type, 1));
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
