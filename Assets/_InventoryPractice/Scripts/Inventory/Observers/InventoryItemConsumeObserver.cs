namespace InventoryPractice
{
    public sealed class InventoryItemConsumeObserver : IInventoryItemConsumeObserver
    {
        private readonly Inventory _inventory;

        public InventoryItemConsumeObserver(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void OnStartGame()
        {
            _inventory.OnItemConsumed += OnItemConsumed;
        }

        public void OnFinishGame()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }

        public void OnItemConsumed(InventoryItem item)
        {
            if (item.TryGetComponent(out HealthItemComponent component))
            {
                //  Hero.Instance.MaxHitPoints += component.Health;
            }
        }
    }
}
