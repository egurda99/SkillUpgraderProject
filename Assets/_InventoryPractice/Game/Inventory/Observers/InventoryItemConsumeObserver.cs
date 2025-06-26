namespace InventoryPractice
{
    public sealed class InventoryItemConsumeObserver : IInventoryItemConsumeObserver
    {
        private readonly Inventory _inventory;

        public InventoryItemConsumeObserver(Inventory inventory)
        {
            _inventory = inventory;
            _inventory.OnItemConsumed += OnItemConsumed;
        }


        public void OnItemConsumed(InventoryItem item)
        {
            if (item.TryGetComponent(out HealthItemComponent component))
            {
                //  Hero.Instance.MaxHitPoints += component.Health;
            }
        }

        public void Dispose()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }
    }
}
