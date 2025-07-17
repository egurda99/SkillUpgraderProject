using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace RealTimePractice
{
    public sealed class ChestsManager : IDisposable
    {
        [ShowInInspector] [ReadOnly] private List<Chest> _chests = new();
        private readonly SaveLoadManager _saveLoadManager;
        public List<Chest> Chests => _chests;

        public event Action<Chest> OnChestAdded;
        public event Action<Chest> OnChestRemoved;
        public event Action<Chest> OnChestOpened;

        public ChestsManager(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        [Button]
        public void AddChest(Chest chest)
        {
            foreach (var chest1 in _chests)
            {
                if (chest1.ID == chest.ID)
                {
                    return;
                }
            }

            _chests.Add(chest);
            chest.OnStarted += OnChestOpenStarted;
            chest.OnOpened += OnChestOpenedEvent;
            OnChestAdded?.Invoke(chest);
            chest.FirstStart();
        }


        [Button]
        public void RemoveChest(Chest chest)
        {
            _chests.Remove(chest);
            chest.OnStarted -= OnChestOpenStarted;
            chest.OnOpened -= OnChestOpenedEvent;

            OnChestRemoved?.Invoke(chest);
        }

        [Button]
        public bool CanOpenChest()
        {
            foreach (var chest in _chests)
            {
                if (chest.CanReceiveReward())
                {
                    return true;
                }
            }

            return false;
        }

        [Button]
        public void OpenChests()
        {
            foreach (var chest in _chests)
            {
                if (chest.CanReceiveReward())
                {
                    chest.ReceiveReward();
                }
            }
        }


        public void SetupChests(List<Chest> chestList)
        {
            _chests.Clear();
            foreach (var chest in chestList)
            {
                chest.OnStarted += OnChestOpenStarted;
                chest.OnOpened += OnChestOpenedEvent;

                chest.Start();
                _chests.Add(chest);
                OnChestAdded?.Invoke(chest);
            }
        }

        private void OnChestOpenedEvent(Chest chest)
        {
            OnChestOpened?.Invoke(chest);
        }

        private void OnChestOpenStarted(Chest chest)
        {
            _saveLoadManager.Save();
        }

        public void Dispose()
        {
            foreach (var chest in _chests)
            {
                chest.OnStarted -= OnChestOpenStarted;
                chest.OnOpened -= OnChestOpenedEvent;
            }

            _chests.Clear();
        }
    }
}
