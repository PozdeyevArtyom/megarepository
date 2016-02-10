using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary;

namespace MyLibraryTests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void DefaultInitTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            Assert.AreEqual(List.Count, 0);
        }

        [TestMethod]
        public void CopyConstructorTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(1);
            List.Add(2);
            MyList<int> List2 = new MyLinkedList<int>(List);
            Assert.AreEqual(List2, List);
        }

        [TestMethod]
        public void AddTest()
        {
            int actualValue = 38;
            MyList<int> List = new MyLinkedList<int>();
            List.Add(actualValue);
            Assert.AreEqual(List.Count, 1);
            Assert.AreEqual(List[0], actualValue);

        }

        [TestMethod]
        public void ClearTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(1);
            List.Add(2);
            List.Clear();
            Assert.AreEqual(List.Count, 0);
        }

        [TestMethod]
        public void ContainTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(31);
            List.Add(9);
            Assert.AreEqual(List.Contains(31), true);
        }

        [TestMethod]
        public void NotContainTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(31);
            List.Add(9);
            Assert.AreEqual(List.Contains(63), false);
        }

        [TestMethod]
        public void CopyToLinked()
        {
            int[] array = new int[2];
            MyList<int> List = new MyLinkedList<int>();
            List.Add(51);
            List.Add(94);
            List.CopyTo(array, 0);
            Assert.AreEqual(array[0], 51);
            Assert.AreEqual(array[1], 94);
        }

        [TestMethod]
        public void InvalidCopyToLinked()
        {
            int[] array = new int[2];
            MyList<int> List = new MyLinkedList<int>();
            List.Add(51);
            List.Add(94);
            try
            {
                List.CopyTo(array, 2);
            }
            catch (IndexOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Индекс вне диапазона.");
            }
        }

        [TestMethod]
        public void FindExistingElement()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(8);
            List.Add(61);
            Assert.AreEqual(List.Find(8), 0);
            Assert.AreEqual(List.Find(61), 1);
        }

        [TestMethod]
        public void FindNotExistingElement()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(8);
            List.Add(61);
            Assert.AreEqual(List.Find(5), -1);
        }

        [TestMethod]
        public void GetElementInRange()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(52);
            List.Add(95);
            Assert.AreEqual(List.Get(0), 52);
            Assert.AreEqual(List.Get(1), 95);
        }

        [TestMethod]
        public void GetElementOutOfRange()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(52);
            List.Add(95);
            try
            {
                List.Get(2);
            }
            catch (IndexOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Индекс вне диапазона.");
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(55);
            List.Add(41);
            List.Insert(79, 1);
            Assert.AreEqual(List[1], 79);
        }

        [TestMethod]
        public void InsertMoreThanRangeTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(55);
            List.Add(41);
            List.Insert(79, 10);
            Assert.AreEqual(List[2], 79);
        }

        [TestMethod]
        public void RemoveExistingElementTest()
        {
            MyList<int> List = new MyLinkedList<int>();
            List.Add(75);
            List.Add(57);
            List.Add(50);
            Assert.AreEqual(List.Remove(57), true);
            Assert.AreEqual(List.Contains(57), false);
        }
    }
}
