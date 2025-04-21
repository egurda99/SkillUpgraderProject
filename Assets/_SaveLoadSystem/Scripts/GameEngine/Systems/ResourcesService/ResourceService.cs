using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace GameEngine
{
    public sealed class ResourceService
    {
        [ShowInInspector] [ReadOnly] private Dictionary<string, Resource> _sceneResources = new();

        public Dictionary<string, Resource> SceneResources => _sceneResources;

        public void SetResources(IEnumerable<Resource> resources)
        {
            _sceneResources.Clear();
            _sceneResources = resources.ToDictionary(it => it.ID);
        }
    }
}
