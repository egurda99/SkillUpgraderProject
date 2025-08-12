using UnityEngine;
using Zenject;

namespace AssetManager.Examples
{
    [RequireComponent(typeof(Collider))]
    public sealed class RegionLoaderTrigger : MonoBehaviour
    {
        [SerializeField] private string _regionKey;

        private bool _isLoaded;
        private RegionLoader _regionLoader;

        [Inject]
        public void Construct(RegionLoader regionLoader)
        {
            _regionLoader = regionLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLoaded && other.CompareTag("Player")) // for test
            {
                _isLoaded = true;
                _regionLoader.LoadRegion(_regionKey);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_isLoaded && other.CompareTag("Player"))
            {
                _isLoaded = false;
                _regionLoader.UnLoadRegion(_regionKey);
            }
        }
    }
}
