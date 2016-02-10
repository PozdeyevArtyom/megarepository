using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task01;

namespace UnitTests
{
    [TestClass]
    public class Task01Tests
    {
        [TestMethod]
        public void DefaultConstructorTest()
        {
            CycleIntIncList list = new CycleIntIncList();
            Assert.AreEqual(list.Count, 2);
            Assert.AreEqual(list.Peek(), 2);
        }

        [TestMethod]
        public void CustomCountConstructorTest()
        {
            int ActualCount = 7;
            CycleIntIncList list = new CycleIntIncList(ActualCount);
            Assert.AreEqual(list.Count, ActualCount);
            Assert.AreEqual(list.Peek(), ActualCount);
        }

        [TestMethod]
        public void CustomIncorrectCountConstructorTest()
        {
            int ActualCount = -3;
            try
            {
                CycleIntIncList list = new CycleIntIncList(ActualCount);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Длина списка дожна быть больше единицы.");
                return;
            }
            Assert.Fail("Не было выброшено ниодного исключения.");
        }

        [TestMethod]
        public void RemoveTest()
        {
            int ActualCount = 12;
            CycleIntIncList list = new CycleIntIncList(ActualCount);
            list.Remove();
            Assert.AreEqual(list.Count, ActualCount-1);
            Assert.AreEqual(list.Peek(), ActualCount-1);
        }

        [TestMethod]
        public void PeekTest()
        {
            int ActualCount = 4;
            CycleIntIncList list = new CycleIntIncList(ActualCount);
            Assert.AreEqual(list.Peek(), ActualCount);
        }

    }
}
