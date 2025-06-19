namespace InventoryPractice
{
    public sealed class HealthInventoryObserver : IInventoryObserver
    {
        private readonly Inventory _inventory;

        public HealthInventoryObserver(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void OnStartGame()
        {
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        public void OnFinishGame()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }

        public void OnItemAdded(InventoryItem item)
        {
            var isEffectable = item.Flags.HasFlag(InventoryItemFlags.Effectable);
            // bool isEffectable = (item.Flags & InventoryItemFlags.Effectable) == InventoryItemFlags.Effectable;
            // 0011
            //*0010
            //=0010

            if (!isEffectable)
            {
                return;
            }

            if (item.TryGetComponent(out HealthItemComponent component))
            {
                // Hero.Instance.MaxHitPoints += component.Health;
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            var isEffectable = (item.Flags & InventoryItemFlags.Effectable) == InventoryItemFlags.Effectable;

            if (!isEffectable)
            {
                return;
            }

            if (item.TryGetComponent(out HealthItemComponent component))
            {
                // Hero.Instance.MaxHitPoints -= component.Health;
            }
        }
    }
}
