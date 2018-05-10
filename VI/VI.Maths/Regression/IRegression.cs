using System.Collections.Generic;

namespace VI.Maths.Regression
{
    interface IRegression
    {
        string RegressionName { get; }
        string RegressionInitials { get; set; }
        double R2 { get; set; }
        string RegressionConfig { get; set; }
        double Calculate(double x);
        void CreateRegression();
        void LoadConfig(string Config);
        void InterData(double x, double y);
        void InterData(List<Coords> Pontos);
        List<Coords> Data();
        string Formula();
    }
}
