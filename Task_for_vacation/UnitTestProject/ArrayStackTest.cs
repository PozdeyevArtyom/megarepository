using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary;

namespace MyLibraryTests
{
    [TestClass]
    public class ArrayStackTest
    {
        [TestMethod]
        public void DefaultInitTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Assert.AreEqual(Stack.Count, 0);
            Assert.AreEqual((Stack as MyArrayStack<int>).Capacity, 10);
        }

        [TestMethod]
        public void CustomCapacityInitTest()
        {
            int actualCapacity = 24;
            MyStack<int> Stack = new MyArrayStack<int>(actualCapacity);
            Assert.AreEqual(Stack.Count, 0);
            Assert.AreEqual((Stack as MyArrayStack<int>).Capacity, actualCapacity);
        }

        [TestMethod]
        public void CustomCapacityLessThanZeroInitTest()
        {
            int actualCapacity = -83;
            try
            {
                MyStack<int> Stack = new MyArrayStack<int>(actualCapacity);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Вместимость стека должна быть натуральным числом.");
            }
        }

        [TestMethod]
        public void CopyConstructorTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(1);
            Stack.Push(2);
            MyStack<int> Stack2 = new MyArrayStack<int>(Stack);
            Assert.AreEqual(Stack2, Stack);
        }

        [TestMethod]
        public void ClearTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(1);
            Stack.Push(2);
            Stack.Clear();
            Assert.AreEqual(Stack.Count, 0);
            Assert.AreEqual((Stack as MyArrayStack<int>).Capacity, 10);
        }

        [TestMethod]
        public void ContainTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(31);
            Stack.Push(9);
            Assert.AreEqual(Stack.Contains(31), true);
        }

        [TestMethod]
        public void NotContainTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(31);
            Stack.Push(9);
            Assert.AreEqual(Stack.Contains(63), false);
        }

        [TestMethod]
        public void CopyToArray()
        {
            int[] array = new int[2];
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(51);
            Stack.Push(94);
            Stack.CopyTo(array, 0);
            Assert.AreEqual(array[0], 51);
            Assert.AreEqual(array[1], 94);
        }

        [TestMethod]
        public void InvalidCopyToArray()
        {
            int[] array = new int[2];
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(51);
            Stack.Push(94);
            try
            {
                Stack.CopyTo(array, 2);
            }
            catch (IndexOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Индекс вне диапазона.");
            }
        }

        [TestMethod]
        public void PushPopTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(85);
            Stack.Push(48);
            Assert.AreEqual(Stack.Pop(), 48);
        }

        public void PushMoreThanCapacityTest()
        {
            int actualCapacity = 1;
            MyStack<int> Stack = new MyArrayStack<int>(actualCapacity);
            Stack.Push(70);
            Stack.Push(52);
            Assert.AreEqual(Stack.Count, 2);
            Assert.AreEqual((Stack as MyArrayStack<int>).Capacity, actualCapacity + 10);
        }

        [TestMethod]
        public void PopFromEmptyStackTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            try
            {
                Stack.Pop();
            }
            catch (InvalidOperationException e)
            {
                StringAssert.Contains(e.Message, "Стек пуст.");
            }
        }

        [TestMethod]
        public void PeekTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            Stack.Push(98);
            Stack.Push(20);
            Assert.AreEqual(Stack.Peek(), 20);
        }

        [TestMethod]
        public void PeekFromEmptyTest()
        {
            MyStack<int> Stack = new MyArrayStack<int>();
            try
            {
                Stack.Peek();
            }
            catch (InvalidOperationException e)
            {
                StringAssert.Contains(e.Message, "Стек пуст.");
            }

        }
    }
}
