using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class UnitManager
    {
        [ShowInInspector] [ReadOnly] private HashSet<Unit> _sceneUnits = new();
        private readonly UnitSpawner _unitSpawner;
        public HashSet<Unit> SceneUnits => _sceneUnits;

        public UnitManager(UnitSpawner unitSpawner)
        {
            _unitSpawner = unitSpawner;
        }

        public void SetupUnits(IEnumerable<Unit> units)
        {
            DestroyActiveUnits();
            _sceneUnits = new HashSet<Unit>(units);
        }

        private void DestroyActiveUnits()
        {
            foreach (var unit in _sceneUnits)
            {
                Object.Destroy(unit.gameObject);
            }

            _sceneUnits.Clear();
        }

        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = _unitSpawner.SpawnUnit(prefab, position, rotation);
            _sceneUnits.Add(unit);
            return unit;
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
