using UnityEngine;

namespace RealTimePractice
{
    public sealed class SceneInstallerHelper : MonoBehaviour
    {
        [SerializeField] private bool _useAsync = true;
        [SerializeField] private RewardConfig _rewardConfig;

        public bool UseAsync => _useAsync;

        public RewardConfig RewardConfig => _rewardConfig;
    }
}