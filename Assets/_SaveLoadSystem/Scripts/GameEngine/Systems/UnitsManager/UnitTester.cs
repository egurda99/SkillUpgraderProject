using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public sealed class UnitTester : MonoBehaviour
    {
        private UnitManager _unitManager;

        [Inject]
        public void Construct(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        [Button]
        public void SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            _unitManager.SpawnUnit(prefab, position, rotation);
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            _unitManager.DestroyUnit(unit);
        }
    }
}