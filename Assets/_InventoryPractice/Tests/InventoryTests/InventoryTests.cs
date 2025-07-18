using InventoryPractice;
using NUnit.Framework;

namespace TestsPractice
{
    [TestFixture]
    public sealed class InventoryTests
    {
        private Inventory _inventory;
        private InventoryInstallerDebug _inventoryInstaller;

        private InventoryItem _lumberItem;
        private InventoryItem _woodItem;


        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory();
            _inventoryInstaller = new InventoryInstallerDebug();
            _inventoryInstaller.Initialize(_inventory);

            _lumberItem = TestItemFactory.CreateLumber();
            _woodItem = TestItemFactory.CreateWood();
        }

        [Test]
        public void WhenEmptyInventory_AndAddNonStackItem_ThenHaveItem()
        {
            // Act
            _inventory.AddItem(_lumberItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_lumberItem));
            Assert.AreEqual(1, _inventory.GetStacksOfItem(_lumberItem.Id));
        }

        [Test]
        public void WhenEmptyInventory_AndAddNonStackItem_ThenHaveItemWithWeight()
        {
            // Act
            _inventory.AddItem(_lumberItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_lumberItem));
            Assert.AreEqual(1, _inventory.GetStacksOfItem(_lumberItem.Id));
            Assert.AreEqual(2, _lumberItem.Weight);
        }

        [Test]
        public void WhenEmptyInventory_AndAddTwoNonStackItems_ThenHave2Items()
        {
            // Act
            _inventory.AddItem(_lumberItem);
            _inventory.AddItem(_lumberItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_lumberItem));
            Assert.AreEqual(2, _inventory.GetStacksOfItem(_lumberItem.Id));
        }

        [Test]
        public void WhenEmptyInventory_AndAddStackableItemsUnderStackSize_ThenHave1ItemStack()
        {
            // Act
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_woodItem));
            Assert.AreEqual(1, _inventory.GetStacksOfItem(_woodItem.Id));
        }

        [Test]
        public void WhenEmptyInventory_AndAddStackableItemsAboveStackSize_ThenHave2ItemStack()
        {
            // Act
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            // Assert
            Assert.IsTrue(_inventory.HasItem(_woodItem));
            Assert.AreEqual(2, _inventory.GetStacksOfItem(_woodItem.Id));
        }

        [Test]
        public void WhenHaveOneItem_AndRemoveItem_ThenHaveEmptyInventory()
        {
            // Act
            _inventory.AddItem(_woodItem);
            _inventory.RemoveItem(_woodItem);

            // Assert

            Assert.IsFalse(_inventory.HasItem(_woodItem));
            Assert.AreEqual(0, _inventory.GetStacksOfItem(_woodItem.Id));
        }

        [Test]
        public void WhenHaveTwoStackableItems_AndRemoveOneItem_ThenHaveOneItem()
        {
            // Act
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.RemoveItem(_woodItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_woodItem));
            Assert.AreEqual(1, _inventory.GetStacksOfItem(_woodItem.Id));
        }

        [Test]
        public void WhenHaveTwoFullStackableItems_AndRemoveOneAndHalfStackItem_ThenHaveOneStackItem()
        {
            // Arrange // 2 full stacks
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);

            // Act

            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);

            // Assert

            Assert.IsTrue(_inventory.HasItem(_woodItem));
            Assert.AreEqual(1, _inventory.GetStacksOfItem(_woodItem.Id));
            Assert.AreEqual(3, _inventory.GetTotalItemCount(_woodItem.Id));
        }

        [Test]
        public void WhenHaveTwoFullAndOneHalfStackableItems_AndRemoveOneAndHalfStackItem_ThenHaveTwoStackItem()
        {
            //Arrange
            // 3.5  stacks
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);

            _inventory.AddItem(_woodItem);
            _inventory.AddItem(_woodItem);
            // Act
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);
            _inventory.RemoveItem(_woodItem);


            // Assert

            Assert.IsTrue(_inventory.HasItem(_woodItem));
            Assert.AreEqual(2, _inventory.GetStacksOfItem(_woodItem.Id));
            Assert.AreEqual(6, _inventory.GetTotalItemCount(_woodItem.Id));
        }

        [Test]
        public void WhenInventoryWeightLimitExceeded_AndAddNonStackItem_ThenItemIsNotAdded()
        {
            // Arrange
            var item = TestItemFactory.CreateLumber(weight: 200);
            var inventory = new Inventory();
            inventory.Init(10, 100);

            // Act
            var canAdd = inventory.CanAddItem(item);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [Test]
        public void WhenInventoryWeightLimitExceeded_AndAddStackItem_ThenItemIsNotAdded()
        {
            // Arrange
            var item = TestItemFactory.CreateWood(weight: 200);
            var inventory = new Inventory();
            inventory.Init(10, 100);

            // Act
            var canAdd = inventory.CanAddItem(item);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [Test]
        public void WhenInventorySlotsSizeExceeded_AndAddNonStackableItem_ThenItemIsNotAdded()
        {
            // Arrange
            var inventory = new Inventory();
            inventory.Init(0, 100);

            // Act
            var canAdd = inventory.CanAddItem(_lumberItem);

            // Assert
            Assert.IsFalse(inventory.HasFreeSlot); // нет слота
            Assert.IsFalse(canAdd); // нельзя добавить
        }

        [Test]
        public void WhenInventorySlotsSizeExceeded_AndAddStackableItem_ThenItemAdded()
        {
            // Arrange
            var inventory = new Inventory();
            inventory.Init(1, 1000);
            inventory.AddItem(_woodItem);

            // Act
            var canAdd = inventory.CanAddItem(_woodItem);

            // Assert
            Assert.IsFalse(inventory.HasFreeSlot); // нет слота
            Assert.IsTrue(canAdd);
        }

        [Test]
        public void WhenInventorySlotsSizeExceeded_AndAddStackableItem_ThenItemNotAdded()
        {
            // Arrange
            var inventory = new Inventory();
            inventory.Init(1, 1000);
            inventory.AddItem(TestItemFactory.CreateWood(stackSize: 4, value: 4));

            // Act
            var canAdd = inventory.CanAddItem(TestItemFactory.CreateWood());

            // Assert
            Assert.IsFalse(inventory.HasFreeSlot); // нет слота
            Assert.IsFalse(canAdd);
        }
    }
}
