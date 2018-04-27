using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SkiaSharp;

namespace Roslyn.ConsoleTools
{
    public class Cw
    {
        protected static void Print<T>(T msg)
        {
            Console.WriteLine(msg);
        }

        protected String Join<T>(string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }              
    }
}