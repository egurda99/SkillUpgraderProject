using System;

namespace InventoryPractice
{
    [Flags]
    public enum InventoryItemFlags
    {
        None = 0, //0000
        Consumable = 1 << 0, //0001
        Effectable = 1 << 1, //0010
        Equipable = 4, //0100
        Stackable = 8, //1000
        All = ~0
    }
}