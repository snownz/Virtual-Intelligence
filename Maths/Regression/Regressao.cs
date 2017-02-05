using Maths.Regression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maths.Regression
{
    public abstract class Regressao : IRegressao
    {
        protected Regressao(string nomeRegressao, string siglaRegressao) {
            NomeRegressao = nomeRegressao;
            SiglaRegressao = siglaRegressao;
            R2 = 0;
            ConfigRegressao = "";
        }

        public List<Coordenadas> objPontos = new List<Coordenadas>();

        public void InserirDados(Double x, Double y)
        {
            var objCordanadas = new Coordenadas();
            objCordanadas.X = x;
            objCordanadas.Y = y;
            objPontos.Add(objCordanadas);
        }

        public void InserirDados(List<Coordenadas> Pontos)
        {
            objPontos = Pontos;
        }

        public List<Coordenadas> ListaDosPontos()
        {
            return objPontos;
        }

        public virtual void CarregarConfig(string Config)
        {
            objPontos = Config.Split(';').ToList()
                .Where(x => x.StartsWith("(") && x.EndsWith(")"))
                .Select(x => new Coordenadas(x)).ToList();
            CriarRegressao();
        }

        public string NomeRegressao { get; set; }
        public string SiglaRegressao { get; set; }
        public double R2 { get; set; }
        public string ConfigRegressao { get; set; }
        public abstract double Calcular(double x);
        public abstract void CriarRegressao();
        public abstract string Formula();        
    }
}