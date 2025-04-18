using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameEngine
{
    public sealed class UnitManager : IInitializable
    {
        private readonly Transform _container;

        [ShowInInspector] [ReadOnly] private List<Unit> _sceneUnits = new();
        private readonly UnitSpawner _unitSpawner;

        public List<Unit> SceneUnits => _sceneUnits;

        public UnitManager(Transform container, UnitSpawner unitSpawner)
        {
            _container = container;
            _unitSpawner = unitSpawner;
        }

        void IInitializable.Initialize()
        {
            _sceneUnits.AddRange(_container.GetComponentsInChildren<Unit>());
        }

        public void SetupUnits(List<UnitData> units)
        {
            var existingUnitsById = new Dictionary<string, Unit>();

            foreach (var unit in _sceneUnits)
            {
                existingUnitsById[unit.ID] = unit;
            }

            foreach (var data in units)
            {
                if (existingUnitsById.TryGetValue(data.Id, out var existingUnit))
                {
                    existingUnit.transform.position = data.Position;
                    existingUnit.transform.eulerAngles = data.EulerAngles;
                    existingUnit.Setup(data.Type, data.Id, data.HitPoints);
                }
                else
                {
                    var newUnit = _unitSpawner.SpawnUnitByType(data.Type, data.Position, data.EulerAngles);
                    newUnit.Setup(data.Type, data.Id, data.HitPoints);
                    _sceneUnits.Add(newUnit);
                }
            }
        }

        public void SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = _unitSpawner.SpawnUnit(prefab, position, rotation, _container);
            _sceneUnits.Add(unit);
        }

        public void DestroyUnit(Unit unit)
        {
            if (_sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }
    }
}
