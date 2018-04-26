using System;
using System.Linq;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Data.Array
{
    public static class ArrayMethods
    {
        public static float[] ByteToArray(byte b, int range)
        {
            var f = Convert.ToInt32(b);
            var br = new float[range];
            br[f] = 1;
            return br;
        }

        public static int GetBestPosition(FloatArray result, bool prob)
        {
            if (prob)
            {
                var p = NumMath.Choice(Enumerable.Range(0, result.Length).ToArray(), 1, result.ToArray()).First();
                return p;
            }

            var max = 0f;
            var pos = 0;
            for (var i = 0; i < result.Length; i++)
                if (max < result[i])
                {
                    max = result[i];
                    pos = i;
                }

            return pos;
        }

        private static int[] IntToArray(int v, int max)
        {
            var a = Enumerable.Repeat(0, max).ToArray();
            a[v] = 1;
            return a;
        }

        private static float[] FloatToArray(int v, int max)
        {
            var a = Enumerable.Repeat(0f, max).ToArray();
            a[v] = 1f;
            return a;
        }

        public static string PrintArray(float[] b, int range)
        {
            var str = "[";
            for (var i = 0; i < range - 1; i++) str += $"{Math.Round(b[i], 2)}, ";
            str += Math.Round(b[range - 1], 2) + "] = " + ArrayToInt(b, range);
            return str;
        }

        public static string PrintArray(FloatArray b, int range)
        {
            var str = "[";
            for (var i = 0; i < range - 1; i++) str += $"{b[i]}, ";
            str += b[range - 1] + "] = " + ArrayToInt(b, range);
            return str;
        }

        public static int ArrayToInt(FloatArray b, int range)
        {
            var r = 0;
            float max = 0;
            for (var i = 0; i < range; i++)
                if (b[i] > max)
                {
                    max = b[i];
                    r = i;
                }

            return r;
        }

        public static int ArrayToInt(float[] b, int range)
        {
            var r = 0;
            float max = 0;
            for (var i = 0; i < range; i++)
                if (b[i] > max)
                {
                    max = b[i];
                    r = i;
                }

            return r;
        }

        public static float[] ByteToArray(byte[][] b, int rangeX, int rangeY)
        {
            var f = new float[rangeX * rangeY];

            for (var x = 0; x < rangeX; x++)
                for (var y = 0; y < rangeY; y++)
                    f[x + y * rangeX] = b[x][y] / 255f;
            return f;
        }
    }
}