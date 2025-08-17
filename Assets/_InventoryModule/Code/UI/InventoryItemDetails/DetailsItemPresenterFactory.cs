using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class DetailsItemPresenterFactory
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;


        public DetailsItemPresenterFactory(Inventory inventory, Equipment equipment)
        {
            _inventory = inventory;
            _equipment = equipment;
        }

        public InventoryItemDetailPresenter Create()
        {
            return new InventoryItemDetailPresenter(_inventory, _equipment);
        }
    }
}
