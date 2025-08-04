using System;

namespace InventoryPractice
{
    public sealed class ItemFactory
    {
        public InventoryItem CreateNullableItem()
        {
            var nullableItem = new InventoryItem
            {
                Id = "null",
                Weight = 0,
                Flags = InventoryItemFlags.None,
                MetaData = new InventoryItemMetaData
                {
                    Name = "Lumber",
                    Description = "Piece of lumber",
                    Icon = null
                },
                Components = Array.Empty<IItemComponent>()
            };
            return nullableItem;
        }
    }
}