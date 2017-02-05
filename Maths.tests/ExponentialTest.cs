using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths.tests
{
    [TestClass]
    public class ExponentialTest
    {
        [TestMethod]
        public void Exponential1()
        {
            Assert.IsTrue(Sequences.Exponential(1) == 1);
        }

        [TestMethod]
        public void Exponential10()
        {
            Assert.IsTrue(Sequences.Exponential(10) == 10000000000);
        }
        
    }
}
