using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public sealed class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] [ReadOnly] private string _id;
        [SerializeField] private int _amount;

        public ResourceType ResourceType => _resourceType;

        public string ID => _id;

        public int Amount => _amount;

        public void Setup(ResourceType resourceType, int amount)
        {
            _resourceType = resourceType;
            _amount = amount;
        }

        public void TryGenerateId()
        {
            if (string.IsNullOrEmpty(_id))
            {
                GenerateId();
            }
        }

        [Button]
        private void GenerateId()
        {
            var instanceId = GetInstanceID().ToString();
            _id = IdGenerator.Generate<Resource>("RES_") + "_" + instanceId;
        }
    }
}
