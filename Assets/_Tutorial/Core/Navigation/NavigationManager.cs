using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationManager : ITickable, IInitializable
    {
        private readonly NavigationArrow _arrow;
        private readonly Transform _worldContainer;


        private Vector3 _position;

        [PropertySpace] [ReadOnly] [ShowInInspector]
        private Vector3 _targetPosition;

        [ReadOnly] [ShowInInspector] private bool _isActive;

        public NavigationManager(NavigationArrow arrow, Transform worldContainer)
        {
            _arrow = arrow;
            _worldContainer = worldContainer;
        }

        private bool _spawned;


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
                _arrow.SetPosition(_position);
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
            Object.Instantiate(_arrow.RootGameObject, _arrow.RootTransform.position, Quaternion.identity,
                _worldContainer);
            _spawned = true;
        }
    }
}
