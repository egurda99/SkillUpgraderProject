using InventoryPractice;
using NUnit.Framework;

namespace TestsPractice
{
    [TestFixture]
    public sealed class StatsTests
    {
        private Inventory _inventory;
        private InventoryInstallerForEquipmentDebug _inventoryInstaller;

        private InventoryItem _shieldItem;
        private InventoryItem _magicSwordItem;
        private InventoryItem _heavySwordItem;
        private InventoryItem _lightArmorItem;
        private InventoryItem _heavyArmorItem;
        private InventoryItem _lumberItem;
        private InventoryItem _woodItem;


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
            _lumberItem = TestItemFactory.CreateLumber();
            _woodItem = TestItemFactory.CreateWood();

            _inventory.AddItemToInventory(_shieldItem);
            _inventory.AddItemToInventory(_magicSwordItem);
            _inventory.AddItemToInventory(_heavySwordItem);
            _inventory.AddItemToInventory(_lightArmorItem);
            _inventory.AddItemToInventory(_heavyArmorItem);
            _inventory.AddItemToInventory(_lumberItem);
        }

        [Test]
        public void WhenZeroStats_AndAddNoStatItem_ThenZeroStats()
        {
            // Act
            _inventory.AddItemToInventory(_woodItem);

            // Assert
            Assert.IsTrue(_playerStats.Armor == 0);
            Assert.IsTrue(_playerStats.Aguility == 0);
            Assert.IsTrue(_playerStats.Health == 0);
            Assert.IsTrue(_playerStats.Power == 0);
        }

        [Test]
        public void WhenZeroStats_AndStatItemEquipped_ThenStatsUpgradedByItem()
        {
            // Act
            _inventory.EquipItem(_heavyArmorItem);

            // Assert
            Assert.AreEqual(_heavyArmorItem.GetComponent<EquipableItemComponentDebug>().HealthValue,
                _playerStats.Health);
        }

        [Test]
        public void WhenItemEquippedAndStatsNotZero_AndItemUnEquipped_ThenStatsDowngradedByItem()
        {
            //Arrange
            _inventory.EquipItem(_heavyArmorItem);

            // Act
            _equipment.Unequip(_heavyArmorItem);

            // Assert
            Assert.IsTrue(_playerStats.Armor == 0);
            Assert.IsTrue(_playerStats.Aguility == 0);
            Assert.IsTrue(_playerStats.Health == 0);
            Assert.IsTrue(_playerStats.Power == 0);
        }
    }
}
