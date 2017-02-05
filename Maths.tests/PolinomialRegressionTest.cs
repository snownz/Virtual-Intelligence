using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths.Regression;

namespace Brain.Tests
{
    [TestClass]
    public class PolinomialRegressionTest
    {
        [TestMethod]
        public void TestRegressaoPolinomial1()
        {
            var obj = new Polinominal();
            obj.NumeroGrau = 2;
            
            for(int i = 0; i<100; i++) 
            {
                obj.InserirDados(i, i);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1, obj.R2.ToString());
            Assert.IsTrue(obj.Formula() == "0x^2+1x^1+0");
            Assert.IsTrue(obj.Calcular(1) >= 0.9999999, obj.Calcular(1).ToString());
        }

        [TestMethod]
        public void TestRegressaoPolinomial2()
        {
            var obj = new Polinominal();
            obj.NumeroGrau = 2;

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                obj.InserirDados(i, i*2);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1, obj.R2.ToString());
            Assert.IsTrue(obj.Formula() == "0x^2+2x^1+0");
            Assert.IsTrue(obj.Calcular(1) >= 0.9999999, obj.Calcular(1).ToString());
        }

        [TestMethod]
        public void TestRegressaoPolinomial3()
        {
            var obj = new Linear();

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                obj.InserirDados(i, (i * 2)+50);
            }

            obj.CriarRegressao();
            Assert.IsTrue(obj.R2 == 1, obj.R2.ToString());
        }
    }
}
