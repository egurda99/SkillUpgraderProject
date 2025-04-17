using UnityEngine;

namespace GameEngine
{
    public sealed class ResourceInstallerHelper : MonoBehaviour
    {
        [SerializeField] private Transform _resourceContainer;

        public Transform ResourceContainer => _resourceContainer;
    }
}