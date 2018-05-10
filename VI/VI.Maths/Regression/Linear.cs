using System;
using System.Collections.Generic;

namespace VI.Maths.Regression
{
    public class Linear : Regressions
    {
        public Linear() : base("Linear", "LINEAR")
        { }
        private Double SomatoriaElevadaX { get; set; }
        private Double SomatoriaElevadaY { get; set; }
        private Double MediaNormalX { get; set; }
        private Double MediaNormalY { get; set; }
        private Double MultiplicacoXY { get; set; }
        private Double CalculoFormula { get; set; }
        public Double B1 { get; set; }
        private Double B0 { get; set; }

        public override Double Calculate(Double x)
        {
            return B1*x + B0;
        }

        private void CalculateParams(List<Coords> ListPonto)
        {
            SomatoriaElevadaX = 0;
            SomatoriaElevadaY = 0;
            MediaNormalX = 0;
            MediaNormalY = 0;
            MultiplicacoXY = 0;

            for (var i = 0; i < objPoints.Count; i++)
            {
                SomatoriaElevadaX += objPoints[i].X*objPoints[i].X;
                SomatoriaElevadaY += objPoints[i].Y*objPoints[i].Y;
                MediaNormalX += objPoints[i].X;
                MediaNormalY += objPoints[i].Y;
                MultiplicacoXY += (objPoints[i].X*objPoints[i].Y);
            }
            MediaNormalX = MediaNormalX/objPoints.Count;
            MediaNormalY = MediaNormalY/objPoints.Count;

            CalculoFormula = 0;

            for (var i = 0; i < objPoints.Count; i++)
                CalculoFormula += (objPoints[i].Y - MediaNormalY)*(objPoints[i].Y - MediaNormalY);
        }

        public override void CreateRegression()
        {
            CalculateParams(objPoints);
            var Syy = SomatoriaElevadaY - objPoints.Count*(MediaNormalY*MediaNormalY);
            var Sxx = SomatoriaElevadaX - objPoints.Count*(MediaNormalX*MediaNormalX);
            var Sxy = MultiplicacoXY - (objPoints.Count*(MediaNormalX*MediaNormalY));
            B1 = Sxy/Sxx;
            B0 = MediaNormalY - B1*MediaNormalX;
            var SQR = Syy - (B1*Sxy);
            var SQT = CalculoFormula;
            R2 = 1 - SQR/SQT;

            RegressionConfig = objPoints.ToText();
        }        

        public override String Formula()
        {
            return Math.Round(B1, 3) + "x+" + Math.Round(B0, 3);
        }
    }
}