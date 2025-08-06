using System.Linq;
using InventoryPractice;
using NUnit.Framework;

namespace TestsPractice
{
    [TestFixture]
    public sealed class EquipmentTests
    {
        private Inventory _inventory;
        private InventoryInstallerForEquipmentDebug _inventoryInstaller;

        private InventoryItem _shieldItem;
        private InventoryItem _magicSwordItem;
        private InventoryItem _heavySwordItem;
        private InventoryItem _lightArmorItem;
        private InventoryItem _heavyArmorItem;


        private PlayerStatsDebug _playerStats;
        private Equipment _equipment;


        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory();
            _playerStats = new PlayerStatsDebug();
            _equipment = new Equipment();

            _inventoryInstaller = new InventoryInstallerForEquipmentDebug();

            _inventoryInstaller.Initialize(_inventory, _playerStats, _equipment);

            _shieldItem = TestItemFactory.CreateShield();
            _magicSwordItem = TestItemFactory.CreateMagicSword();
            _heavySwordItem = TestItemFactory.CreateHeavySword();
            _lightArmorItem = TestItemFactory.CreateLightArmor();
            _heavyArmorItem = TestItemFactory.CreateHeavyArmor();

            _inventory.AddItemToInventory(_shieldItem);
            _inventory.AddItemToInventory(_magicSwordItem);
            _inventory.AddItemToInventory(_heavySwordItem);
            _inventory.AddItemToInventory(_lightArmorItem);
            _inventory.AddItemToInventory(_heavyArmorItem);
        }

        [Test]
        public void WhenEmptyEquipment_AndItemEquipped_ThenItIsInEquippedItemsList()
        {
            // Act
            _inventory.EquipItem(_heavyArmorItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Armor);
            Assert.Contains(_heavyArmorItem, equipped);
        }

        [Test]
        public void WhenItemEquipped_AndItemUnEquipped_ThenItIsInInventoryAndSlotEmpty()
        {
            // Arrange
            _inventory.EquipItem(_heavyArmorItem);

            // Act
            _equipment.Unequip(_heavyArmorItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Armor);
            Assert.IsFalse(equipped.Contains(_heavyArmorItem));
            Assert.IsTrue(_inventory.HasItem(_heavyArmorItem));
            Assert.IsTrue(equipped.All(i => i == null));
        }

        [Test]
        public void WhenItemEquipped_AndItemEquippedOnSameSlot_ThenOldItemIsReplaced()
        {
            // Arrange
            _inventory.EquipItem(_heavyArmorItem);

            // Act
            _inventory.EquipItem(_lightArmorItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Armor);
            Assert.IsFalse(equipped.Contains(_heavyArmorItem));
            Assert.IsTrue(_inventory.HasItem(_heavyArmorItem));
            Assert.Contains(_lightArmorItem, equipped);
        }

        [Test]
        public void WhenItemEquipped_AndItemDropped_ThenSlotEmptyAndDropEventInvoked()
        {
            // Arrange

            var eventCalled = false;
            _equipment.OnDropOutItem += (_, droppedItem, _) =>
            {
                if (droppedItem == _heavyArmorItem)
                    eventCalled = true;
            };

            _inventory.EquipItem(_heavyArmorItem);


            // Act
            _equipment.DropItemFromEquipped(_heavyArmorItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Armor);
            Assert.IsFalse(equipped.Contains(_heavyArmorItem));
            Assert.IsFalse(_inventory.HasItem(_heavyArmorItem));
            Assert.IsTrue(eventCalled);
        }

        [Test]
        public void WhenTwoItemsOfSameEquipTypeEquipped_AndEquipThirdOne_ThenOldItemIsUnequipped()
        {
            // Arrange
            _inventory.EquipItem(_magicSwordItem);
            _inventory.EquipItem(_heavySwordItem);

            // Act
            _inventory.EquipItem(_shieldItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Hand);

            Assert.IsTrue(equipped.Contains(_heavySwordItem));
            Assert.IsTrue(equipped.Contains(_shieldItem));
            Assert.IsFalse(equipped.Contains(_magicSwordItem));
            Assert.AreEqual(2,
                _equipment.GetEquippedItems(EquipType.Hand).Count(i =>
                    i.GetComponent<EquipableItemComponentDebug>().EquipType == EquipType.Hand));
        }

        [Test]
        public void
            WhenTwoItemsOfSameEquipTypeEquipped_AndUnEquipLastOneAndEquipThirdOne_ThenOldestItemAndThirdAreEquipped()
        {
            // Arrange
            _inventory.EquipItem(_magicSwordItem);
            _inventory.EquipItem(_heavySwordItem);

            // Act
            _equipment.Unequip(_heavySwordItem);
            _inventory.EquipItem(_shieldItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Hand);

            Assert.IsFalse(equipped.Contains(_heavySwordItem));
            Assert.IsTrue(equipped.Contains(_shieldItem));
            Assert.IsTrue(equipped.Contains(_magicSwordItem));
            Assert.AreEqual(2,
                _equipment.GetEquippedItems(EquipType.Hand).Count(i =>
                    i.GetComponent<EquipableItemComponentDebug>().EquipType == EquipType.Hand));
        }

        [Test]
        public void
            WhenItemEquippedAndInvenotryFullBySlots_AndTryUnEquip_ThenItemIsDropped()
        {
            // Arrange
            _inventory.EquipItem(_magicSwordItem);
            _inventory.Init(4, 100);

            // Act
            _equipment.Unequip(_magicSwordItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Hand);

            Assert.IsFalse(_inventory.HasItem(_magicSwordItem));
            Assert.IsFalse(equipped.Contains(_magicSwordItem));
        }

        [Test]
        public void
            WhenItemEquippedAndInvenotryFullByWeight_AndTryUnEquip_ThenItemIsDropped()
        {
            // Arrange
            _inventory.EquipItem(_magicSwordItem);
            _inventory.Init(4, 2);

            // Act
            _equipment.Unequip(_magicSwordItem);

            // Assert
            var equipped = _equipment.GetEquippedItems(EquipType.Hand);

            Assert.IsFalse(_inventory.HasItem(_magicSwordItem));
            Assert.IsFalse(equipped.Contains(_magicSwordItem));
        }
    }
}
