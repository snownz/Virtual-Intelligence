using System;
using System.Collections.Generic;
using System.Linq;

namespace VI.Maths.Regression
{
    public class Coords
    {
        public Coords() { }
        public Coords(string text)
        {
            text = text.Replace("(", "").Replace(")", "");
            var xy = text.Split(',');
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

    public static class CoordsExtensions
    {
        public static string ToText(this List<Coords> coords)
        {
            return string.Join(";", coords.Select(x => x.ToString()).ToList().ToArray());
        }
    }
}