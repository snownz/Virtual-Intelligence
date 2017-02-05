using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Brain.Node;
using Maths;

namespace Brain.Tests
{
    [TestClass]
    public class SensorNeuronTest
    {
        [TestMethod]
        public void TestRegressionNN()
        {
            SensorNeuron sn = new SensorNeuron();
            sn.IntervalNormalization = new Range(0, 50);
            sn.IntervalNormalizationOutput = new Range(0, 1);
            sn.Value = 15;

            double? i = sn.Output();
            Assert.IsTrue(i == .3);            
        }

        [TestMethod]
        public void TestRegression01()
        {
            SensorNeuron sn = new SensorNeuron();
            sn.IntervalNormalization = new Range(0, 1);
            sn.IntervalNormalizationOutput = new Range(0, 1);
            sn.Value = .6;

            double? i = sn.Output();
            Assert.IsTrue(i == .6);
        }


        [TestMethod]
        public void TestRegression00()
        {
            SensorNeuron sn = new SensorNeuron();
            sn.IntervalNormalization = new Range(0, 0);
            sn.IntervalNormalizationOutput = new Range(0, 1);
            sn.Value = .6;

            double? i = sn.Output();
            Assert.IsTrue(i == .6);
        }
    }
}
