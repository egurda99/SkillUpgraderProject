using System;
using InventoryPractice;

namespace TestsPractice
{
    public static class TestItemFactory
    {
        public static InventoryItem CreateLumber(string id = "lumber_01", int weight = 2)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.None,
                MetaData = new InventoryItemMetaData
                {
                    Name = "Lumber",
                    Description = "Piece of lumber",
                    Icon = null
                },
                Components = Array.Empty<IItemComponent>()
            };
        }

        public static InventoryItem CreateWood(string id = "wood_02", int weight = 1, int stackSize = 4, int value = 1)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Stackable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "Lumber",
                    Description = "Piece of lumber",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new StackableItemComponentDebug(stackSize, value)
                }
            };
        }
    }
}
