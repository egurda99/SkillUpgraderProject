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

        public static InventoryItem CreateShield(string id = "shield_03", int weight = 4)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Equipable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "shield",
                    Description = "Armor",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new EquipableItemComponentDebug(EquipType.Hand, 1, 1, 1, 1)
                }
            };
        }

        public static InventoryItem CreateMagicSword(string id = "MagicSword_04", int weight = 4)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Equipable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "MagicSword",
                    Description = "MagicSword",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new EquipableItemComponentDebug(EquipType.Hand, 2, 2, 2, 2)
                }
            };
        }

        public static InventoryItem CreateHeavySword(string id = "HeavySword_05", int weight = 4)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Equipable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "HeavySword",
                    Description = "HeavySword",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new EquipableItemComponentDebug(EquipType.Hand, 3, 3, 3, 3)
                }
            };
        }

        public static InventoryItem CreateHeavyArmor(string id = "HeavyArmor_06", int weight = 4)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Equipable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "HeavyArmor",
                    Description = "HeavyArmor",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new EquipableItemComponentDebug(EquipType.Armor, 4, 4, 4, 4)
                }
            };
        }

        public static InventoryItem CreateLightArmor(string id = "LightArmor_07", int weight = 4)
        {
            return new InventoryItem
            {
                Id = id,
                Weight = weight,
                Flags = InventoryItemFlags.Equipable,
                MetaData = new InventoryItemMetaData
                {
                    Name = "LightArmor",
                    Description = "LightArmor",
                    Icon = null
                },
                Components = new IItemComponent[]
                {
                    new EquipableItemComponentDebug(EquipType.Armor, 5, 5, 5, 5)
                }
            };
        }
    }
}
