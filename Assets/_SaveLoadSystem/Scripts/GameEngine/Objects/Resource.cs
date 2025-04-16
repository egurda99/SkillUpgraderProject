using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private string _id;
        [SerializeField] private int _amount;

        public ResourceType ResourceType => _resourceType;

        public string ID => _id;

        public int Amount => _amount;

        public void Setup(ResourceType resourceType, int amount)
        {
            _resourceType = resourceType;
            _amount = amount;
        }
    }
}
