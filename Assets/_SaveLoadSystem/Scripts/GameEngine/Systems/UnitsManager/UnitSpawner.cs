using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    public sealed class UnitSpawner
    {
        private readonly Transform _container;
        private readonly int _defaultHP = 10;
        private readonly UnitPrefabProvider _unitPrefabProvider;


        public UnitSpawner(Transform container, UnitPrefabProvider prefabProvider)
        {
            _unitPrefabProvider = prefabProvider;
            _container = container;
        }


        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, _container);
            unit.GenerateId();
            unit.Setup(_unitPrefabProvider.GetUnitTypeByPrefab(prefab), _defaultHP);

            return unit;
        }

        public Unit SpawnUnitByType(string type, Vector3 position, Vector3 eulerAngles)
        {
            var unitPrefab = _unitPrefabProvider.GetPrefab(type);
            var unit = Object.Instantiate(unitPrefab, position, Quaternion.Euler(eulerAngles), _container);
            return unit;
        }
    }
}
