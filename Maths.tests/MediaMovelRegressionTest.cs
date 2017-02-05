using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths.Regression;

namespace Brain.Tests
{
    [TestClass]
    public class MediaMovelRegressionTest
    {
        [TestMethod]
        public void TestMediaMovel1()
        {
            var obj = new MediaMovel();
            obj.CarregarConfig("(0,1);(2,2);(3,3);(4,4);(5,3);(6,2);(7,1);(8,0);PERIODO=3");
            Assert.IsTrue(obj.R2 > 0,"R2="+obj.R2);
            Assert.IsTrue(obj.Formula() == "(0,1);(2,2);(3,3);(4,4);(5,3);(6,2);(7,1);(8,0)");
            Assert.IsTrue(obj.Calcular(3) == 2,"Y="+ obj.Calcular(3));
        }        
    }
}
