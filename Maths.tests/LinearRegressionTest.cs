using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths.Regression;

namespace Brain.Tests
{
    [TestClass]
    public class LinearRegressionTest
    {
        [TestMethod]
        public void TestRegressaoLinear1()
        {
            var obj = new Linear();
            
            for(int i = 0; i<100; i++) 
            {
                obj.InserirDados(i, i);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1);
            Assert.IsTrue(obj.Formula() == "1x+0");
            Assert.IsTrue(obj.Calcular(1) == 1);
        }

        [TestMethod]
        public void TestRegressaoLinear2()
        {
            var obj = new Linear();

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                obj.InserirDados(i, i*2);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1);
            Assert.IsTrue(obj.Formula() == "2x+0");
            Assert.IsTrue(obj.Calcular(1) == 2);
        }

        [TestMethod]
        public void TestRegressaoLinear3()
        {
            var obj = new Linear();

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                obj.InserirDados(i, (i * 2)+50);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1);
            Assert.IsTrue(obj.Formula() == "2x+50");
            Assert.IsTrue(obj.Calcular(1) == 52);
        }

        [TestMethod]
        public void TestRegressaoLinear4()
        {
            var obj = new Linear();

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                obj.InserirDados(i, (i * 2) + rnd.Next(0,2));
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 >= 0.9);
            Assert.IsTrue(obj.Calcular(0) >= 0 && obj.Calcular(0) <= 1);
        }
    }
}
