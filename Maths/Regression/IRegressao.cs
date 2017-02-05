using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths.Regression
{
    interface IRegressao
    {
        string NomeRegressao { get; }
        string SiglaRegressao { get; set; }
        double R2 { get; set; }
        string ConfigRegressao { get; set; }
        double Calcular(double x);
        void CriarRegressao();
        void CarregarConfig(string Config);
        void InserirDados(double x, double y);
        void InserirDados(List<Coordenadas> Pontos);
        List<Coordenadas> ListaDosPontos();
        string Formula();
    }
}
