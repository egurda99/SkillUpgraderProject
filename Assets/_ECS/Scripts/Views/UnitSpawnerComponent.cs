using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Client.Views
{
    public sealed class UnitSpawnerComponent : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _unitsContainer;
        [SerializeField] private Entity _archerPrefab;
        [SerializeField] private Entity _swordmanBluePrefab;

        private readonly EcsCustomInject<EntityManager> _entityManager;


        [Button]
        public void SpawnArcher()
        {
            EcsStartup.Instance.EntityManager.Create(_archerPrefab, _spawnPosition.position, Quaternion.identity,
                _unitsContainer);
        }

        [Button]
        public void SpawnSwordman()
        {
            EcsStartup.Instance.EntityManager.Create(_swordmanBluePrefab, _spawnPosition.position, Quaternion.identity,
                _unitsContainer);
        }
    }
}
