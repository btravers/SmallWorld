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
            WrapperCarte wrapper = new WrapperCarte(5, "gaulois", "nains");
            List<int> carte =  wrapper.getCarte();
            Assert.IsNotNull(carte);
        }

        [TestMethod]
        public void TestNbCase()
        {
            WrapperCarte wrapper = new WrapperCarte(5, "gaulois", "nains");
            List<int> carte = wrapper.getCarte();
            Assert.AreEqual(carte.Count,25);
        }

        [TestMethod]
        public void TestTypeCase()
        {
            WrapperCarte wrapper = new WrapperCarte(5, "gaulois", "nains");
            List<int> carte = wrapper.getCarte();
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
