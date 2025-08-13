using UnityEngine;
using Zenject;

namespace Game.Tutorial.Gameplay
{
    public sealed class VisualZoneManager : IInitializable
    {
        private ZoneView _zoneView;
        private readonly GameObject _zoneViewPrefab;
        private readonly Transform _worldContainer;
        private bool _spawned;

        public VisualZoneManager(GameObject zoneViewPrefab, Transform worldContainer)
        {
            _zoneViewPrefab = zoneViewPrefab;
            _worldContainer = worldContainer;
        }

        public void Initialize()
        {
            Spawn();
            _zoneView.HideInstant();
        }

        private void Spawn()
        {
            var zone = Object.Instantiate(_zoneViewPrefab, _zoneViewPrefab.transform.position, Quaternion.identity,
                _worldContainer);
            _zoneView = zone.GetComponent<ZoneView>();
            _spawned = true;
        }

        public void ShowZone(Vector3 position, Quaternion rotation)
        {
            position.y = 0;
            _zoneView.transform.position = position;
            _zoneView.transform.rotation = rotation;
            _zoneView.Show();
        }

        public void ShowZone(Vector3 position, Quaternion rotation, float width, float length)
        {
            _zoneView.transform.position = position;
            _zoneView.transform.rotation = rotation;
            _zoneView.SetSize(width, length);
            _zoneView.Show();
        }

        public void HideZone()
        {
            _zoneView.Hide();
        }
    }
}
