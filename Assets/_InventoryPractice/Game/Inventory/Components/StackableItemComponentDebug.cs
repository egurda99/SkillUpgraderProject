using System.Reflection;

namespace InventoryPractice
{
    public class StackableItemComponentDebug : StackableItemComponent
    {
        public StackableItemComponentDebug(int stackSize, int value)
        {
            SetStackSize(stackSize);
            SetValue(value);
        }

        private void SetStackSize(int value)
        {
            var field = typeof(StackableItemComponent).GetField("_stackSize",
                BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(this, value);
        }
    }
}
