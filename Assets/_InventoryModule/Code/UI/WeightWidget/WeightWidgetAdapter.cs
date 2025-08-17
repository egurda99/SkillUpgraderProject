using System;
using InventoryPractice;

namespace _InventoryPractice
{
    public sealed class WeightWidgetAdapter : IDisposable
    {
        private readonly ValueWidgetView _view;
        private readonly Inventory _inventory;

        public WeightWidgetAdapter(ValueWidgetView view, Inventory inventory)
        {
            _view = view;
            _inventory = inventory;

            _inventory.OnWeightChanged += UpdateWeightWidget;
            UpdateWeightWidget(_inventory.UsedWeight);
        }

        public void UpdateWeightWidget(int value)
        {
            _view.SetText($"{value}/{_inventory.WeightLimit}");
        }

        public void Dispose()
        {
            _inventory.OnWeightChanged -= UpdateWeightWidget;
        }
    }
}
