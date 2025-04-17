using UnityEngine;

namespace GameEngine
{
    public sealed class UnitInstallerHelper : MonoBehaviour
    {
        [SerializeField] private Transform _unitContainer;
        [SerializeField] private GameObject _orc_MountedShamanPrefab;
        [SerializeField] private GameObject _orc_archerPrefab;
        [SerializeField] private GameObject _wK_workerPrefab;
        [SerializeField] private GameObject _wK_CatapultPrefab;
        [SerializeField] private GameObject _wK_spearman_APrefab;

        public Transform UnitContainer => _unitContainer;

        public GameObject OrcMountedShamanPrefab => _orc_MountedShamanPrefab;

        public GameObject OrcArcherPrefab => _orc_archerPrefab;

        public GameObject WKWorkerPrefab => _wK_workerPrefab;

        public GameObject WKCatapultPrefab => _wK_CatapultPrefab;

        public GameObject WKSpearmanAPrefab => _wK_spearman_APrefab;
    }
}
