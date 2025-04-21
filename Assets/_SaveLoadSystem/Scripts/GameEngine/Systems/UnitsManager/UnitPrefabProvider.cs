using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public sealed class UnitPrefabProvider
    {
        private readonly Unit _orc_MountedShamanPrefab;
        private readonly Unit _orc_archerPrefab;
        private readonly Unit _wK_workerPrefab;
        private readonly Unit _wK_CatapultPrefab;
        private readonly Unit _wK_spearman_APrefab;

        private readonly Dictionary<string, Unit> _unitsPrefabs = new();

        public UnitPrefabProvider(Unit orcMountedShamanPrefab, Unit orcArcherPrefab, Unit wKWorkerPrefab,
            Unit wKCatapultPrefab, Unit wKSpearmanAPrefab)
        {
            _orc_MountedShamanPrefab = orcMountedShamanPrefab;
            _orc_archerPrefab = orcArcherPrefab;
            _wK_workerPrefab = wKWorkerPrefab;
            _wK_CatapultPrefab = wKCatapultPrefab;
            _wK_spearman_APrefab = wKSpearmanAPrefab;

            DictionaryInit();
        }

        public Unit GetPrefab(string type)
        {
            _unitsPrefabs.TryGetValue(type, out var unitPrefab);
            return unitPrefab;
        }

        public string GetUnitTypeByPrefab(Unit prefab)
        {
            return _unitsPrefabs.FirstOrDefault(x => x.Value == prefab).Key
                   ?? throw new Exception("Prefab not found");
        }


        private void DictionaryInit()
        {
            _unitsPrefabs.Add("Orc_MountedShaman", _orc_MountedShamanPrefab);
            _unitsPrefabs.Add("Orc_archer", _orc_archerPrefab);
            _unitsPrefabs.Add("WK_worker", _wK_workerPrefab);
            _unitsPrefabs.Add("WK_Catapult", _wK_CatapultPrefab);
            _unitsPrefabs.Add("WK_spearman_A", _wK_spearman_APrefab);
        }
    }
}
