using Atomic.Entities;
using UnityEngine;
using Zenject;

namespace BehaviourTreePractice
{
    public sealed class BotsInstaller : MonoInstaller<BotsInstaller>
    {
        [SerializeField] private SceneEntity _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _botsContainer;


        public override void InstallBindings()
        {
            for (var index = 0; index < _spawnPoints.Length; index++)
            {
                var spawnPoint = _spawnPoints[index];
                var player = Container.InstantiatePrefabForComponent<SceneEntity>(_playerPrefab, spawnPoint.position,
                    Quaternion.identity, _botsContainer);
            }
        }
    }
}
