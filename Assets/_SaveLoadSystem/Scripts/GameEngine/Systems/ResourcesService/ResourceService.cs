using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public sealed class ResourceService : IInitializable
    {
        private readonly Transform _resourceContainer;
        private readonly List<Resource> _sceneResources = new();
        public List<Resource> SceneResources => _sceneResources;

        public ResourceService(Transform resourceContainer)
        {
            _resourceContainer = resourceContainer;
        }

        void IInitializable.Initialize()
        {
            _sceneResources.AddRange(_resourceContainer.GetComponentsInChildren<Resource>());
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
