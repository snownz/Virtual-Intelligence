using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maths.tests
{
    [TestClass]
    public class SequenceTest
    {
        [TestMethod]
        public void Fibonacci0()
        {
            Assert.IsTrue(Sequences.Fibonacci(0) == 1);
        }

        [TestMethod]
        public void Fibonacci1()
        {
            Assert.IsTrue(Sequences.Fibonacci(1) == 1);
        }

        [TestMethod]
        public void Fibonacci5()
        {
            Assert.IsTrue(Sequences.Fibonacci(5) == 8);
        }

        [TestMethod]
        public void Fibonacci10()
        {
            Assert.IsTrue(Sequences.Fibonacci(10) == 89);
        }
    }
}
