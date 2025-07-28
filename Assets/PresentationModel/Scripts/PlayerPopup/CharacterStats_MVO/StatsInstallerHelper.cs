using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class StatsInstallerHelper : MonoBehaviour
    {
        [SerializeField] private Transform _statContainer;
        [SerializeField] private StatView _statPrefab;

        public Transform StatContainer => _statContainer;

        public StatView StatPrefab => _statPrefab;
    }
}