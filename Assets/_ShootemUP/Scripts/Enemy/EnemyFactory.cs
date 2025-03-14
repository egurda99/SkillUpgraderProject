using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;

        public Enemy GetEnemy(Transform parent)
        {
            var enemy = Instantiate(_enemyPrefab, parent);
            return enemy;
        }
    }
}
