using UnityEngine;

namespace GameEngine
{
    public sealed class UnitInstallerHelper : MonoBehaviour
    {
        [SerializeField] private Transform _unitContainer;
        [SerializeField] private Unit _orc_MountedShamanPrefab;
        [SerializeField] private Unit _orc_archerPrefab;
        [SerializeField] private Unit _wK_workerPrefab;
        [SerializeField] private Unit _wK_CatapultPrefab;
        [SerializeField] private Unit _wK_spearman_APrefab;

        public Transform UnitContainer => _unitContainer;

        public Unit OrcMountedShamanPrefab => _orc_MountedShamanPrefab;

        public Unit OrcArcherPrefab => _orc_archerPrefab;

        public Unit WKWorkerPrefab => _wK_workerPrefab;

        public Unit WKCatapultPrefab => _wK_CatapultPrefab;

        public Unit WKSpearmanAPrefab => _wK_spearman_APrefab;
    }
}
