using System;
using System.Collections.Generic;

namespace Maths.Regression
{
    public class Linear : Regressao
    {
        public Linear() : base("Linear", "LINEAR")
        { }
        private Double SomatoriaElevadaX { get; set; }
        private Double SomatoriaElevadaY { get; set; }
        private Double MediaNormalX { get; set; }
        private Double MediaNormalY { get; set; }
        private Double MultiplicacoXY { get; set; }
        private Double CalculoFormula { get; set; }
        private Double B1 { get; set; }
        private Double B0 { get; set; }

        public override Double Calcular(Double x)
        {
            return B1*x + B0;
        }

        private void CalcularParemetros(List<Coordenadas> ListPonto)
        {
            SomatoriaElevadaX = 0;
            SomatoriaElevadaY = 0;
            MediaNormalX = 0;
            MediaNormalY = 0;
            MultiplicacoXY = 0;

            for (var i = 0; i < objPontos.Count; i++)
            {
                SomatoriaElevadaX += objPontos[i].X*objPontos[i].X;
                SomatoriaElevadaY += objPontos[i].Y*objPontos[i].Y;
                MediaNormalX += objPontos[i].X;
                MediaNormalY += objPontos[i].Y;
                MultiplicacoXY += (objPontos[i].X*objPontos[i].Y);
            }
            MediaNormalX = MediaNormalX/objPontos.Count;
            MediaNormalY = MediaNormalY/objPontos.Count;

            CalculoFormula = 0;

            for (var i = 0; i < objPontos.Count; i++)
                CalculoFormula += (objPontos[i].Y - MediaNormalY)*(objPontos[i].Y - MediaNormalY);
        }

        public override void CriarRegressao()
        {
            CalcularParemetros(objPontos);
            var Syy = SomatoriaElevadaY - objPontos.Count*(MediaNormalY*MediaNormalY);
            var Sxx = SomatoriaElevadaX - objPontos.Count*(MediaNormalX*MediaNormalX);
            var Sxy = MultiplicacoXY - (objPontos.Count*(MediaNormalX*MediaNormalY));
            B1 = Sxy/Sxx;
            B0 = MediaNormalY - B1*MediaNormalX;
            var SQR = Syy - (B1*Sxy);
            var SQT = CalculoFormula;
            R2 = 1 - SQR/SQT;

            ConfigRegressao = objPontos.ToText();
        }        

        public override String Formula()
        {
            return Math.Round(B1, 3) + "x+" + Math.Round(B0, 3);
        }
    }
}