using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RealTimePractice
{
    public sealed class ChestsDebugger : MonoBehaviour
    {
        private ChestsManager _chestsManager;
        [ShowInInspector] [ReadOnly] public List<Chest> Chests => _chestsManager.Chests;

        [Inject]
        public void Construct(ChestsManager chestsManager)
        {
            _chestsManager = chestsManager;
        }

        [Button]
        public void AddChest(ChestConfig chestConfig)
        {
            var chest = new Chest(chestConfig.ChestType, chestConfig.Id, chestConfig.Duration);

            _chestsManager.AddChest(chest);
        }

        [Button]
        public bool CanOpenChest()
        {
            return _chestsManager.CanOpenChest();
        }

        [Button]
        public void OpenChests()
        {
            _chestsManager.OpenChests();
        }
    }
}
