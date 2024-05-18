using lab10;
using lab12__2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyHashTableTests
{
    [TestClass]
    public class MyHashTableTests
    {
        [TestMethod]
        public void TestCapacity_DefaultConstructor_DefaultCapacity()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();

            // Assert
            Assert.AreEqual(10, hashTable.Capacity);
        }

        [TestMethod]
        public void TestContains_ElementPresent_ReturnsTrue()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage = new Carriage(new IdNumber(1), 123, 100);
            hashTable.AddItem(carriage);

            // Act
            bool contains = hashTable.Contains(carriage);

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void TestContains_ElementNotPresent_ReturnsFalse()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage = new Carriage(new IdNumber(1), 123, 100);

            // Act
            bool contains = hashTable.Contains(carriage);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void TestRemoveData_RemovesElement()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage1 = new Carriage(new IdNumber(1), 123, 100);
            var carriage2 = new Carriage(new IdNumber(2), 456, 200);
            hashTable.AddItem(carriage1);
            hashTable.AddItem(carriage2);

            // Act
            bool removed = hashTable.RemoveData(carriage1);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(hashTable.Contains(carriage1));
        }

        [TestMethod]
        public void TestAddItem_ExpandsTableWhenCapacityExceeded()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>(size: 2, ratio: 0.5);
            var carriage1 = new Carriage(new IdNumber(1), 123, 100);
            var carriage2 = new Carriage(new IdNumber(2), 456, 200);
            var carriage3 = new Carriage(new IdNumber(3), 789, 300);

            // Act
            hashTable.AddItem(carriage1);
            hashTable.AddItem(carriage2);
            hashTable.AddItem(carriage3);

            // Assert
            Assert.AreEqual(3, hashTable.Count);
            Assert.IsTrue(hashTable.Contains(carriage1));
            Assert.IsTrue(hashTable.Contains(carriage2));
        }

        [TestMethod]
        public void TestAddData_ItemAdded_Correctly()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage = new Carriage(new IdNumber(1), 123, 100);

            // Act
            hashTable.AddData(carriage);

            // Assert
            Assert.IsTrue(hashTable.Contains(carriage));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Такой элемент уже есть")]
        public void TestAddData_DuplicateItem_ThrowsException()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage = new Carriage(new IdNumber(1), 123, 100);
            hashTable.AddData(carriage); // добавляем элемент в таблицу

            // Act
            hashTable.AddData(carriage); // пытаемся добавить дубликат

            // Assert
            // Ожидаем исключение, так как дубликат элемента не должен быть добавлен
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "В таблице нет свободных мест!")]
        public void TestAddData_TableFull_ThrowsException()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>(size: 1); // Устанавливаем размер таблицы в 1, чтобы вызвать исключение при попытке добавить второй элемент
            var carriage1 = new Carriage(new IdNumber(1), 123, 100);
            var carriage2 = new Carriage(new IdNumber(2), 456, 200);
            hashTable.AddData(carriage1); // добавляем первый элемент

            // Act
            hashTable.AddData(carriage2); // пытаемся добавить второй элемент

            // Assert
            // Ожидаем исключение, так как таблица уже заполнена
        }

        [TestMethod]
        public void TestUpdateIndexesAfterRemoval_WithEmptySlots()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage1 = new Carriage(new IdNumber(1), 123, 100);
            var carriage2 = new Carriage(new IdNumber(2), 456, 200);
            hashTable.AddItem(carriage1);
            hashTable.AddItem(carriage2);

            // Act
            hashTable.RemoveData(carriage1); // Remove the first carriage, leaving an empty slot
            hashTable.UpdateIndexesAfterRemoval();

            // Assert
            Assert.AreEqual(carriage2, hashTable.table[0]); // Ensure the second carriage is in the correct position
            Assert.IsNull(hashTable.table[1]); // Ensure the empty slot has been cleared
        }

        [TestMethod]
        public void TestUpdateIndexesAfterRemoval_NoEmptySlots()
        {
            // Arrange
            MyHashTable<Carriage> hashTable = new MyHashTable<Carriage>();
            var carriage1 = new Carriage(new IdNumber(1), 123, 100);
            var carriage2 = new Carriage(new IdNumber(2), 456, 200);
            hashTable.AddItem(carriage1);
            hashTable.AddItem(carriage2);

            // Act
            hashTable.RemoveData(carriage2);
            hashTable.UpdateIndexesAfterRemoval();

            // Assert
            Assert.AreEqual(carriage1, hashTable.table[0]);
            Assert.IsNull(hashTable.table[1]);
        }

    }
}