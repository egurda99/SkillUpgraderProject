using System;

namespace InventoryPractice
{
    [Serializable]
    public sealed class HealthItemComponent : IItemComponent
    {
        public int Health;

        public IItemComponent Clone()
        {
            return new HealthItemComponent
            {
                Health = Health
            };
        }
    }
}
