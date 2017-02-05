using System;
using System.Collections.Generic;
using System.Linq;

namespace Maths.Regression
{
    public class Coordenadas
    {
        public Coordenadas() { }
        public Coordenadas(String texto)
        {
            texto = texto.Replace("(", "").Replace(")", "");
            var xy = texto.Split(',');
            if (xy.Length != 2)
                throw new Exception("Numero de coordenadas inválida, deve ser 2");
            X = Convert.ToDouble(xy[0]);
            Y = Convert.ToDouble(xy[1]);
        }
        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }

        
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Dist { get; set; }
    }

    public static class CoordenadasExtensions
    {
        public static string ToText(this List<Coordenadas> coordenadas)
        {
            return string.Join(";", coordenadas.Select(x => x.ToString()).ToList().ToArray());
        }
    }
}