using UnityEngine;

namespace RealTimePractice
{
    [CreateAssetMenu(
        fileName = "ChestConfig",
        menuName = "SO/New ChestConfig"
    )]
    public sealed class ChestConfig : ScriptableObject
    {
        public ChestType ChestType;
        public string Id;
        public float Duration;
    }
}
