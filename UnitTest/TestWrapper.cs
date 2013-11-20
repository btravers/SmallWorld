using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WrapperSmallWorld;

namespace UnitTest
{
    [TestClass]
    public class TestWrapperCarte
    {
        [TestMethod]
        public void TestNotNull()
        {
            List<int> carte =  WrapperCarte.genererCarte(5);
            Assert.IsNotNull(carte);
        }

        [TestMethod]
        public void TestNbCase()
        {
            List<int> carte = WrapperCarte.genererCarte(5);
            Assert.AreEqual(carte.Count,25);
        }

        [TestMethod]
        public void TestTypeCase()
        {
            List<int> carte = WrapperCarte.genererCarte(5);
            int[] type = new int[5];
            foreach(int c in carte)
            {
                type[c]++;
            }

            Assert.IsTrue((25 / 5 + 1) >= type[0]);
            Assert.IsTrue((25 / 5 + 1) >= type[1]);
            Assert.IsTrue((25 / 5 + 1) >= type[2]);
            Assert.IsTrue((25 / 5 + 1) >= type[3]);
            Assert.IsTrue((25 / 5 + 1) >= type[4]);
        }
    }
}
