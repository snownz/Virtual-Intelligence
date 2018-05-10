using System;
using System.Collections.Generic;
using System.Linq;

namespace VI.Maths.Regression
{
    public abstract class Regressions : IRegression
    {
        protected Regressions(string regressionName, string regressionInitials) {
            RegressionName = regressionName;
            RegressionInitials = regressionInitials;
            R2 = 0;
            RegressionConfig = "";
            objPoints = new List<Coords>();
        }

        protected List<Coords> objPoints;

        public void InterData(Double x, Double y)
        {
            var objCordanadas = new Coords();
            objCordanadas.X = x;
            objCordanadas.Y = y;
            objPoints.Add(objCordanadas);
        }

        public void InterData(List<Coords> Pontos)
        {
            objPoints = Pontos;
        }

        public List<Coords> Data() => objPoints;

        public virtual void LoadConfig(string Config)
        {
            objPoints = Config.Split(';').ToList()
                .Where(x => x.StartsWith("(") && x.EndsWith(")"))
                .Select(x => new Coords(x)).ToList();

            CreateRegression();
        }

        public string RegressionName { get; set; }
        public string RegressionInitials { get; set; }
        public double R2 { get; set; }
        public string RegressionConfig { get; set; }
        public abstract double Calculate(double x);
        public abstract void CreateRegression();
        public abstract string Formula();
    }
}