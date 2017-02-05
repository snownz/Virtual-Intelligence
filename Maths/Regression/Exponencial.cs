using System;
using System.Collections.Generic;

namespace Maths.Regression
{
    public class Exponencial : Regressao
    {
        private List<Coordenadas> ListaPontosModificado = new List<Coordenadas>();
        public Exponencial() : base("Exponencial", "EXPONENCIAL")
        { }

        private Double S2 { get; set; }
        private Double[] ValoresCalculo { get; set; }

        private void CalcularParemetros(List<Coordenadas> ListPonto)
        {
            for (var i = 0; i < ListPonto.Count; i++)
            {
                var objCoordenadas = new Coordenadas();
                objCoordenadas.X = ListPonto[i].X;
                objCoordenadas.Y = Math.Log(ListPonto[i].Y);
                ListaPontosModificado.Add(objCoordenadas);
            }
        }

        public override void CriarRegressao()
        {
            CalcularParemetros(objPontos);
            var objPolinomial = new Polinominal();
            objPolinomial.NumeroGrau = 2;
            for (var i = 0; i < ListaPontosModificado.Count; i++)
            {
                objPolinomial.InserirDados(ListaPontosModificado[i].X, ListaPontosModificado[i].Y);
            }
            objPolinomial.CriarRegressao();
            R2 = objPolinomial.R2;
            S2 = objPolinomial.S2;
            ValoresCalculo = objPolinomial.ValoresCalculo;

            ConfigRegressao = objPontos.ToText();
        }        

        public override string Formula()
        {
            return "F(x)=" + Math.Exp(ValoresCalculo[1]) + "*e^" + ValoresCalculo[2] + "*x";
        }

        public override Double Calcular(Double x)
        {
            return (Math.Exp(ValoresCalculo[1])*(Math.Exp(ValoresCalculo[2]*x)));
        }
    }
}