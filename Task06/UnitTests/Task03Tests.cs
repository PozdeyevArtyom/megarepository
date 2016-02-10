using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task03;

namespace UnitTests
{
    [TestClass]
    public class Task03Tests
    {
        //DynamicArrayTests
        [TestMethod]
        public void DefaultConstructorTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>();
            Assert.AreEqual(Arr.Capacity, 8);
            Assert.AreEqual(Arr.Length, 0); 
        }

        [TestMethod]
        public void CustomCapacityConstructorTest()
        {
            int ActualCapacity = 5;
            DynamicArray<int> Arr = new DynamicArray<int>(ActualCapacity);
            Assert.AreEqual(Arr.Capacity, ActualCapacity);
            Assert.AreEqual(Arr.Length, 0);
        }

        [TestMethod]
        public void CustomIncorrectCapacityConstructorTest()
        {
            int ActualCapacity = -4;
            try
            {
                DynamicArray<int> Arr = new DynamicArray<int>(ActualCapacity);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Ёмкость массива должна быть натуральным числом.");
                return;
            }
            Assert.Fail("Не было выброшено ниодного исключения.");
        }

        [TestMethod]
        public void AddTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>();
            Arr.Add(23);
            Assert.AreEqual(Arr.Length, 1);
            Assert.AreEqual(Arr[0], 23);
        }

        [TestMethod]
        public void AddMoreThanCapacityTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>(2);
            Arr.Add(65);
            Arr.Add(16);
            Arr.Add(76);
            Assert.AreEqual(Arr.Length, 3);
            Assert.AreEqual(Arr.Capacity, 4);
            Assert.AreEqual(Arr[2], 76);
        }
        
        [TestMethod]
        public void AddRangeTest()
        {
            int[] a = { 28, 98, 89, 25, 43 };
            DynamicArray<int> Arr = new DynamicArray<int>();
            Arr.Add(68);
            Arr.Add(73);
            Arr.AddRange(a);
            Assert.AreEqual(Arr.Length, 7);
            Assert.AreEqual(Arr.Capacity, 8);
            Assert.AreEqual(Arr[2], 28);
            Assert.AreEqual(Arr[6], 43);
        }

        [TestMethod]
        public void AddRangeMoreThanCapacity()
        {
            int[] a = { 28, 98, 89, 25, 43 };
            DynamicArray<int> Arr = new DynamicArray<int>(2);
            Arr.Add(68);
            Arr.Add(73);
            Arr.AddRange(a);
            Assert.AreEqual(Arr.Length, 7);
            Assert.AreEqual(Arr.Capacity, 8);
            Assert.AreEqual(Arr[2], 28);
            Assert.AreEqual(Arr[6], 43);
        }

        public void InsertTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>();
            Arr.Add(75);
            Arr.Add(51);
            Arr.Add(18);
            Arr.Insert(78, 2);
            Assert.AreEqual(Arr.Length, 4);
            Assert.AreEqual(Arr[2], 78);
            Assert.AreEqual(Arr[3], 18);
        }

        public void InsertInFullListTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>(2);
            Arr.Add(91);
            Arr.Add(36);
            Arr.Insert(46, 1);
            Assert.AreEqual(Arr.Length, 3);
            Assert.AreEqual(Arr.Capacity, 4);
            Assert.AreEqual(Arr[1], 46);
            Assert.AreEqual(Arr[2], 36);
        }

        public void InsertOutOfRangeTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>();
            try
            {
                Arr.Insert(73, 4);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Индекс вне диапазона.");
            }
            Assert.Fail("Не было выброшено ниодного исключения.");
        }

        public void RemoveExistingElementTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>(2);
            Arr.Add(99);
            Arr.Add(90);
            Assert.AreEqual(Arr.Remove(90), true);
            Assert.AreEqual(Arr.Length, 1);
        }

        public void RemoveNotexistingElementTest()
        {
            DynamicArray<int> Arr = new DynamicArray<int>(2);
            Arr.Add(21);
            Arr.Add(26);
            Assert.AreEqual(Arr.Remove(87), false);
        }
    }
}
