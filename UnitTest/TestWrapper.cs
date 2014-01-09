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
            wrapper.Dispose();
        }

        [TestMethod]
        public void TestNbCase()
        {
            WrapperCarte wrapper = new WrapperCarte(5, "gaulois", "nains");
            List<int> carte = wrapper.getCarte();
            Assert.AreEqual(carte.Count,25);
            wrapper.Dispose();
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
            wrapper.Dispose();
        }

        [TestMethod]
        public void TestPositionnement()
        {
            WrapperCarte wrapper = new WrapperCarte(5, "gaulois", "nains");
            Assert.IsTrue(wrapper.getPosJA()>=0);
            Assert.IsTrue(wrapper.getPosJB() >= 0);
            Assert.IsTrue(wrapper.getPosJA() < 25);
            Assert.IsTrue(wrapper.getPosJB() < 25);
            wrapper.Dispose();
        }

        [TestMethod]
        public void TestDestinations()
        {
            List<int> carte = new List<int>();
            carte.Add(1);
            carte.Add(1);
            carte.Add(1);
            carte.Add(1);
            List<int> pos = new List<int>();
            pos.Add(3);
            List<int> res = Destinations.destinations("vikings", 1, carte, 2, 1, pos);
            Assert.IsTrue(res.Contains(0));
            Assert.IsFalse(res.Contains(1));
            Assert.IsFalse(res.Contains(2));
            Assert.IsTrue(res.Contains(3));
        }
    }
}
