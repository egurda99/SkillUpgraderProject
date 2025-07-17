using UnityEngine;

namespace RealTimePractice
{
    [CreateAssetMenu(
        fileName = "RewardConfig",
        menuName = "SO/New RewardConfig"
    )]
    public sealed class RewardConfig : ScriptableObject
    {
        [SerializeField] public ChestRewards ChestRewards;
    }
}
