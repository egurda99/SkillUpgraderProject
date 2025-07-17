using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RealTimePractice
{
    public sealed class RewardManager : IDisposable
    {
        private readonly ChestsManager _chestsManager;
        private readonly RewardConfig _rewardConfig;

        public RewardManager(ChestsManager chestsManager, RewardConfig rewardConfig)
        {
            _chestsManager = chestsManager;
            _rewardConfig = rewardConfig;
            _chestsManager.OnChestOpened += OnChestOpenProcessReward;
        }

        private void OnChestOpenProcessReward(Chest chest)
        {
            GetRandomRewardForType(chest.ChestType);
        }

        private void GetRandomRewardForType(ChestType chestType)
        {
            switch (chestType)
            {
                case ChestType.Wooden:
                {
                    var rewardList = _rewardConfig.ChestRewards.WoodenChestRewards;
                    var rewardPrototype = rewardList[Random.Range(0, rewardList.Length)];

                    var rewardInstance = rewardPrototype.Clone();

                    ApplyChestReward(rewardInstance);
                    break;
                }

                case ChestType.Steel:
                {
                    var rewardList = _rewardConfig.ChestRewards.SteelChestRewards;
                    var rewardInstance = rewardList[Random.Range(0, rewardList.Length)].Clone();
                    ApplyChestReward(rewardInstance);
                    break;
                }

                case ChestType.Golden:
                {
                    var rewardList = _rewardConfig.ChestRewards.GoldenChestRewards;
                    var rewardInstance = rewardList[Random.Range(0, rewardList.Length)].Clone();
                    ApplyChestReward(rewardInstance);
                    break;
                }

                default:
                    Debug.LogWarning($"Unknown chest type: {chestType}");
                    break;
            }
        }

        private void ApplyChestReward(IChestReward reward)
        {
            switch (reward)
            {
                case MoneyChestReward money:
                    Debug.Log($"Player received {money.Money} coins!");
                    break;

                case GemsChestReward resource:
                    Debug.Log($"Player received gems: {resource.Gems}");
                    break;

                case CrystallsChestReward crystall:
                    Debug.Log($"Player received crystalls: {crystall.Crystalls}");
                    break;

                default:
                    Debug.LogWarning("Unknown reward type.");
                    break;
            }
        }


        public void Dispose()
        {
            _chestsManager.OnChestOpened -= OnChestOpenProcessReward;
        }
    }
}
