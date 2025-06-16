using System;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public class ResourceItem
    {
        public ResourceType Type;
        public int Amount;

        public ResourceItem()
        {
        }

        public ResourceItem(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }


    // [CreateAssetMenu(fileName = "resource", menuName = "SO/Resources")]
    // public class ResourceItem : ScriptableObject
    // {
    //     public ResourceType Type;
    //     public int Amount;
    // }
}
