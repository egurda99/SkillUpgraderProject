using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    public sealed class UnitSpawner
    {
        private readonly Transform _container;

        private readonly GameObject _orc_MountedShamanPrefab;
        private readonly GameObject _orc_archerPrefab;
        private readonly GameObject _wK_workerPrefab;
        private readonly GameObject _wK_CatapultPrefab;
        private readonly GameObject _wK_spearman_APrefab;

        public UnitSpawner(GameObject orcMountedShamanPrefab, GameObject orcArcherPrefab, GameObject wKWorkerPrefab,
            GameObject wKCatapultPrefab, GameObject wKSpearmanAPrefab, Transform container)
        {
            _orc_MountedShamanPrefab = orcMountedShamanPrefab;
            _orc_archerPrefab = orcArcherPrefab;
            _wK_workerPrefab = wKWorkerPrefab;
            _wK_CatapultPrefab = wKCatapultPrefab;
            _wK_spearman_APrefab = wKSpearmanAPrefab;
            _container = container;
        }

        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var unit = Object.Instantiate(prefab, position, rotation, parent);
            return unit;
        }

        public Unit SpawnUnitByType(string type, Vector3 position, Vector3 eulerAngles)
        {
            GameObject unitPrefab = null;

            switch (type)
            {
                case "Orc_MountedShaman":
                    unitPrefab = _orc_MountedShamanPrefab;
                    break;
                case "Orc_archer":
                    unitPrefab = _orc_archerPrefab;
                    break;
                case "WK_worker":
                    unitPrefab = _wK_workerPrefab;
                    break;
                case "WK_Catapult":
                    unitPrefab = _wK_CatapultPrefab;
                    break;
                case "WK_spearman_A":
                    unitPrefab = _wK_spearman_APrefab;
                    break;

                default:
                    throw new ArgumentException("Unknown Unit Type");
                    break;
            }


            var unit = Object.Instantiate(unitPrefab, position, Quaternion.Euler(eulerAngles), _container);
            var unitComp = unit.GetComponent<Unit>();
            return unitComp;
        }
    }
}
