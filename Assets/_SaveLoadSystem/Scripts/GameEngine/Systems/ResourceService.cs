using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine
{
    public sealed class ResourceService : MonoBehaviour
    {
        private readonly List<Resource> _sceneResources = new();

        public List<Resource> SceneResources => _sceneResources;

        private void Awake()
        {
            _sceneResources.AddRange(gameObject.GetComponentsInChildren<Resource>());
        }

        public void SetupResources(ResourcesData resourcesData)
        {
            var resourceDataDict = resourcesData.ResourcesDataList.ToDictionary(data => data.Id);

            foreach (var sceneResource in _sceneResources)
            {
                if (resourceDataDict.TryGetValue(sceneResource.ID, out var resourceData))
                {
                    sceneResource.Setup(resourceData.ResourceType, resourceData.Amount);
                }
            }
        }
    }
}
