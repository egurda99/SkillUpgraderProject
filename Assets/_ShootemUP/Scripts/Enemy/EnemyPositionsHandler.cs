using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionsHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;

        [SerializeField] private Transform[] _attackPositions;

        private TransformRandomizer _transformRandomizer;

        private void Awake()
        {
            _transformRandomizer = new TransformRandomizer();
        }

        public Transform GetRandomSpawnPosition()
        {
            return _transformRandomizer.RandomTransform(_spawnPositions);
        }

        public Transform GetRandomAttackPosition()
        {
            return _transformRandomizer.RandomTransform(_attackPositions);
        }
    }
}
