using System;
using UnityEngine;

namespace RealTimePractice
{
    [Serializable]
    public sealed class ChestRewards
    {
        [SerializeReference] public IChestReward[] WoodenChestRewards;
        [SerializeReference] public IChestReward[] SteelChestRewards;
        [SerializeReference] public IChestReward[] GoldenChestRewards;
    }
}