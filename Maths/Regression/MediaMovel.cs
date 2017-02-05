using System;
using System.Collections.Generic;
using System.Linq;

namespace Maths.Regression
{
    public class MediaMovel : Regressao
    {
        public MediaMovel() : base("MediaMovel","MEDIAMOVEL")
        {
        }

        public int _Periodo = 2;
        public int Periodo
        {
            get { return _Periodo; }
            set
            {
                if (value < 2)
                { throw new Exception("Periodo deve ser maior ou igual a 2"); }
                _Periodo = value;
            }
        }
        public List<Coordenadas> CoordRegressao = new List<Coordenadas>();

        public override Double Calcular(Double x)
        {
            var pontos = CoordRegressao.Where(c => c.X <= x);
            if (pontos.Count() < Periodo)
                throw new Exception("Dados insuficientes");
            return pontos.Take(Periodo).Average(c => c.Y);
        }

        public void CalcularParemetros(List<Coordenadas> ListPonto)
        {
            throw new NotImplementedException();
        }

        public override void CriarRegressao()
        {
            CoordRegressao = objPontos.OrderByDescending(c => c.X).ToList();
            ConfigRegressao = objPontos.ToText() + ";PERIODO=" + Periodo;

            #region Calculo de R2
            var TesteR2 = CoordRegressao.Take(CoordRegressao.Count - Periodo + 1);
            var YMed = TesteR2.Average(c => c.Y);
            var SQtot = TesteR2.Sum(c=>(c.Y - YMed)* (c.Y - YMed));
            var SQexp = TesteR2.Sum(c => (Calcular(c.X) - YMed) * (Calcular(c.X) - YMed));
            var SQres = TesteR2.Sum(c => (Calcular(c.X) - c.Y) * (Calcular(c.X) - c.Y));
            R2 = 1 - SQres / SQtot;
            #endregion


        }

        public override void CarregarConfig(string Config)
        {
            var ConfigGrau = Config.Split(';').ToList().Find(x => x.Contains("PERIODO=")) ?? "PERIODO=2";
            Periodo = Convert.ToInt32(ConfigGrau.Replace("PERIODO=", ""));
            base.CarregarConfig(Config);
        }

        public override string Formula()
        {
            return objPontos.ToText();
        }
    }
}