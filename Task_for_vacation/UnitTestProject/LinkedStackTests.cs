using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary;

namespace MyLibraryTests
{
    [TestClass]
    public class LinkedStackTests
    {
        [TestMethod]
        public void DefaultInitTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
            Assert.AreEqual(Stack.Count, 0);
        }

        [TestMethod]
        public void CopyConstructorTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(1);
            Stack.Push(2);
            MyStack<int> Stack2 = new MyLinkedStack<int>(Stack);
            Assert.AreEqual(Stack2, Stack);
        }

        [TestMethod]
        public void ClearTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(1);
            Stack.Push(2);
            Stack.Clear();
            Assert.AreEqual(Stack.Count, 0);
        }

        [TestMethod]
        public void ContainTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(31);
            Stack.Push(9);
            Assert.AreEqual(Stack.Contains(31), true);
        }

        [TestMethod]
        public void NotContainTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(31);
            Stack.Push(9);
            Assert.AreEqual(Stack.Contains(63), false);
        }

        [TestMethod]
        public void CopyToLinked()
        {
            int[] array = new int[2];
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(51);
            Stack.Push(94);
            Stack.CopyTo(array, 0);
            Assert.AreEqual(array[0], 51);
            Assert.AreEqual(array[1], 94);
        }

        [TestMethod]
        public void InvalidCopyToLinked()
        {
            int[] array = new int[2];
            MyStack<int> Stack = new MyLinkedStack<int>();
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
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(85);
            Stack.Push(48);
            Assert.AreEqual(Stack.Pop(), 48);
        }

        [TestMethod]
        public void PopFromEmptyStackTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
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
            MyStack<int> Stack = new MyLinkedStack<int>();
            Stack.Push(98);
            Stack.Push(20);
            Assert.AreEqual(Stack.Peek(), 20);
        }

        [TestMethod]
        public void PeekFromEmptyTest()
        {
            MyStack<int> Stack = new MyLinkedStack<int>();
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
