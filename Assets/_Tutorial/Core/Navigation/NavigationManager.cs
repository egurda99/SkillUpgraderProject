using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationManager : ITickable, IInitializable
    {
        private readonly GameObject _arrowPrefab;
        private readonly Transform _worldContainer;


        private Vector3 _position;

        [PropertySpace] [ReadOnly] [ShowInInspector]
        private Vector3 _targetPosition;

        private readonly PlayerService _playerService;

        [ReadOnly] [ShowInInspector] private bool _isActive;

        private bool _spawned;
        private NavigationArrow _arrow;


        public NavigationManager(PlayerService playerService, GameObject arrowPrefab, Transform worldContainer)
        {
            _arrowPrefab = arrowPrefab;
            _worldContainer = worldContainer;
            _playerService = playerService;
        }


        [Button]
        public void StartLookAt(Transform targetPoint)
        {
            StartLookAt(targetPoint.position);
        }

        public void StartLookAt(Vector3 targetPosition)
        {
            if (!_spawned)
            {
                Spawn();
            }

            _arrow.Show();
            _isActive = true;
            _targetPosition = targetPosition;
        }

        public void Stop()
        {
            _arrow.Hide();
            _isActive = false;
        }

        public void Tick()
        {
            if (_isActive)
            {
                var playerPosition = _playerService.Player.GetRootTransform().position;
                _arrow.SetPosition(playerPosition);
                _arrow.LookAt(_targetPosition);
            }
        }

        public void Initialize()
        {
            Spawn();
            _arrow.Hide();
        }


        private void Spawn()
        {
            var arrow = Object.Instantiate(_arrowPrefab, _arrowPrefab.transform.position, Quaternion.identity,
                _worldContainer);

            _arrow = arrow.GetComponent<NavigationArrow>();

            _spawned = true;
        }
    }
}
