using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maths.Regression;

namespace Brain.Tests
{
    [TestClass]
    public class RegressaoTest
    {
        public class RepressaoAbstractTest : Regressao
        {
            public RepressaoAbstractTest() : base("TESTE", "TESTE") { }
            public string MetodoCriarRegressao;
            
            public override double Calcular(double x)
            {
                throw new NotImplementedException();
            }

            public override void CriarRegressao()
            {
                MetodoCriarRegressao = "executado";
            }

            public override string Formula()
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void RepressaoTest1()
        {
            var obj = new RepressaoAbstractTest();
            obj.CarregarConfig("(1,1);(2,2);(3,3)");
            obj.InserirDados(4, 4);

            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(obj.objPontos[i].X == i + 1 && obj.objPontos[i].Y == i + 1,
                    "X=" + obj.objPontos[i].X + ";Y=" + obj.objPontos[i].Y);
            }

            Assert.IsTrue(obj.objPontos.Count == 4, "Count=" + obj.objPontos.Count);
            Assert.IsTrue(obj.MetodoCriarRegressao == "executado");
            Assert.IsTrue(obj.NomeRegressao == "TESTE", "nome=" + obj.NomeRegressao);
            Assert.IsTrue(obj.SiglaRegressao == "TESTE", "sigla=" + obj.SiglaRegressao);
        }

        
    }
}
