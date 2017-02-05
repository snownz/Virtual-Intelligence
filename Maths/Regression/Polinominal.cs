using System;
using System.Collections.Generic;
using System.Linq;

namespace Maths.Regression
{
    public class Polinominal : Regressao
    {
        public Double[] ValoresCalculo { get; private set; }

        public Polinominal() : base("Polinominal", "POLINOMIAL")
        {
        }
        public Int32 NumeroGrau { get; set; }
        private Int32 NumeroVariaveis { get; set; }
        private Int32 NumeroParametros { get; set; }
        private String Equacao { get; set; }
        public Double S2 { get; private set; }

        public override Double Calcular(Double x)
        {
            var Calculo = new Double();
            var Grau = NumeroGrau;
            for (var i = 1; i <= Grau; i++)
            {
                Calculo += ValoresCalculo[i]*Math.Pow(x, i - 1);
            }
            return Calculo;
        }

        private void CalcularParemetros(List<Coordenadas> ListPonto)
        {
            Double N = ListPonto.Count;
            var V = 1;
            var P = NumeroGrau;
            var X = new Double[ListPonto.Count + 1, ListPonto.Count + 1];
            var Sxx = new Double[ListPonto.Count + 1, ListPonto.Count + 1];
            var Sxy = new Double[ListPonto.Count + 1];
            var Y = new Double[ListPonto.Count + 1];

            for (var i = 1; i <= ListPonto.Count; i++)
            {
                X[i, 1] = ListPonto[i - 1].X;
                Y[i] = ListPonto[i - 1].Y;
            }
            if ((V > 1) && (V + 1 != P))
            {
                return;
            }
            for (var i = 1; i <= N; i++)
            {
                for (var j = V + 1; j >= 2; j--)
                {
                    X[i, j] = X[i, j - 1];
                }
                X[i, 1] = 1;
            }
            if ((V == 1) && (P > 2))
                for (var j = 2; j <= P - 1; j++)
                {
                    for (var i = 1; i <= N; i++)
                    {
                        X[i, j + 1] = Math.Pow(X[i, 2], j);
                    }
                }
            Double Soma = 0;
            for (var i = 1; i <= P; i++)
            {
                for (var j = 1; j <= P; j++)
                {
                    Soma = 0;
                    for (var k = 1; k <= N; k++)
                    {
                        Soma += X[k, i]*X[k, j];
                    }
                    Sxx[i, j] = Soma;
                }
                Soma = 0;
                for (var k = 1; k <= N; k++)
                {
                    Soma += X[k, i]*Y[k];
                }
                Sxy[i] = Soma;
            }
            var L = Cholesky(P, Sxx);
            var T = SubstuicoesSucessivas(P, L, Sxy);
            var U = new Double[ListPonto.Count + 1, ListPonto.Count + 1];
            for (var i = 1; i <= P; i++)
            {
                for (var j = 1; j <= i; j++)
                {
                    U[j, i] = L[i, j];
                }
            }
            var B = SubstituicoesRetroativas(P, U, T);
            Double D = 0;
            var d = new Double[ListPonto.Count + 1];
            Double Sy2 = 0;
            var u = new Double[ListPonto.Count + 1];
            for (var i = 1; i <= N; i++)
            {
                Soma = 0;
                for (var j = 1; j <= P; j++)
                {
                    Soma += B[j]*X[i, j];
                }

                u[i] = Soma;
                d[i] = Y[i] - u[i];
                D += (d[i]*d[i]);
                Sy2 += (Y[i]*Y[i]);
            }
            ValoresCalculo = new Double[B.Count()];
            ValoresCalculo = B;
            R2 = 1 - (D/(Sy2 - (Math.Pow(Sxy[1], 2)/N)));
            S2 = D/(N - P);
        }

        private Double[] SubstituicoesRetroativas(Int32 N, Double[,] U, Double[] D)
        {
            var X = new Double[objPontos.Count + 1];
            X[N] = D[N]/U[N, N];
            for (var i = N - 1; i >= 1; i--)
            {
                Double Soma = 0;
                for (var j = i + 1; j <= N; j++)
                {
                    Soma += U[i, j]*X[j];
                }
                X[i] = (D[i] - Soma)/U[i, i];
            }


            return X;
        }

        private Double[,] Cholesky(Double N, Double[,] A)
        {
            Double Det = 1;
            Double R = 0;
            var Erro = true;
            var L = new Double[objPontos.Count + 1, objPontos.Count + 1];
            for (var j = 1; j <= N; j++)
            {
                Double Soma = 0;
                for (var k = 1; k <= j - 1; k++)
                {
                    Soma += L[j, k]*L[j, k];
                }
                var T = A[j, j] - Soma;
                Det = Det*T;
                Erro = T <= 0;
                if (Erro)
                {
                    return new Double[1, 1];
                    //A matriz nao definida positiva;
                }
                L[j, j] = Math.Sqrt(T);
                R = 1/L[j, j];
                for (var i = j + 1; i <= N; i++)
                {
                    Soma = 0;

                    for (var k = 1; k <= j - 1; k++)
                    {
                        Soma += L[i, k]*L[j, k];
                    }
                    L[i, j] = (A[i, j] - Soma)*R;
                }
            }
            return L;
        }

        private Double[] SubstuicoesSucessivas(Double N, Double[,] L, Double[] C)
        {
            var X = new Double[objPontos.Count + 1];
            X[1] = C[1]/L[1, 1];
            for (var i = 2; i <= N; i++)
            {
                Double Soma = 0;
                for (var j = 1; j <= i - 1; j++)
                {
                    Soma += L[i, j]*X[j];
                }
                X[i] = (C[i] - Soma)/L[i, i];
            }
            return X;
        }

        public override void CriarRegressao()
        {
            try
            {
                CalcularParemetros(objPontos);
            }
            catch 
            { }

            ConfigRegressao = objPontos.ToText() + ";GRAU=" +NumeroGrau;
        }

        public override void CarregarConfig(string Config)
        {            
            var ConfigGrau = Config.Split(';').ToList().Find(x => x.Contains("GRAU=")) ?? "GRAU=2";
            NumeroGrau = Convert.ToInt32(ConfigGrau.Replace("GRAU=", ""));
            base.CarregarConfig(Config);
        }

        public override string Formula()
        {
            var FormulaFinal = String.Empty;
            var Grau = NumeroGrau;
            for (var i = 1; i <= NumeroGrau + 1; i++)
            {
                if (Grau == 0)
                {
                    FormulaFinal += Math.Round(ValoresCalculo[i]);
                }
                else
                    FormulaFinal += Math.Round(ValoresCalculo[i], 3) + "x^" + (Grau) + "+";
                Grau -= 1;
            }

            return FormulaFinal;
        }
    }
}