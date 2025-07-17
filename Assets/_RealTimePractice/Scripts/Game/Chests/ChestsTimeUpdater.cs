using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RealTimePractice
{
    public sealed class ChestsTimeUpdater : ITickable, IDisposable
    {
        private readonly ChestsManager _chestsManager;
        private readonly List<Chest> _activeChests = new();

        public ChestsTimeUpdater(ChestsManager chestsManager)
        {
            _chestsManager = chestsManager;
            _chestsManager.OnChestAdded += OnChestAdded;
            _chestsManager.OnChestRemoved += OnChestRemoved;
        }

        private void OnChestRemoved(Chest chest)
        {
            if (_activeChests.Contains(chest))
                _activeChests.Remove(chest);
        }

        private void OnChestAdded(Chest chest)
        {
            if (!_activeChests.Contains(chest))
                _activeChests.Add(chest);
        }

        public void Tick()
        {
            foreach (var chest in _activeChests)
            {
                chest.Update(Time.deltaTime);
            }
        }

        public void Dispose()
        {
            _chestsManager.OnChestAdded -= OnChestAdded;
            _chestsManager.OnChestRemoved -= OnChestRemoved;
        }
    }
}