using UnityEngine;
using Zenject;

namespace GameEngine
{
    public sealed class SceneStarter : MonoBehaviour
    {
        [SerializeField] private Transform _unitsContainer;
        [SerializeField] private Transform _resorcesContainer;
        private UnitManager _unitmanager;
        private ResourceService _resorcesService;


        [Inject]
        public void Construct(UnitManager unitmanager, ResourceService resourceService)
        {
            _unitmanager = unitmanager;
            _resorcesService = resourceService;
        }

        private void Start()
        {
            _unitmanager.SetupUnits(_unitsContainer.GetComponentsInChildren<Unit>());
            _resorcesService.SetResources(_resorcesContainer.GetComponentsInChildren<Resource>());
        }
    }
}
