using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths
{
    public static class Sequences
    {
        public static long Fibonacci(int position)
        {
            int previous = 0;
            int current = 1;

            for (int i = 0; i < position; i++)
            {
                var aux = current;
                current += previous;
                previous = aux;
            }

            return current;
        }

        public static long Exponential(int value)
        {
            return (long)Math.Pow(value, value);
        }

        public static long Sequential(int value)
        {
            return value + 1;
        }
    }
}
