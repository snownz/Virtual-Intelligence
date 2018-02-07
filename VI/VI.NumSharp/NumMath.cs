using System;
using System.Collections.Generic;
using System.Linq;
using ILGPU;
using VI.Maths.Random;
using VI.NumSharp.Arrays;
using VI.NumSharp.Provider;

namespace VI.NumSharp
{
    public static class NumMath
    {
        private static ThreadSafeRandom th = new ThreadSafeRandom();
        private static IArrayExecutor _executor = ProcessingDevice.ArrayExecutorResolver();

        //TODO .... calc it on GPU
        public static Array2D<float> Random(int w, int h, float factor)
        {
            var arr = new Array2D<float>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    arr[x, y] = th.NextFloat() * factor;
                }
            }

            return arr;
        }

        //TODO .... calc it on GPU
        public static Array<float> Random(int w, float factor)
        {
            var arr = new Array<float>(w);

            for (int x = 0; x < w; x++)
            {
                arr[x] = th.NextFloat() * factor;
            }

            return arr;
        }

        //TODO .... calc it on GPU
        public static Array2D<float> Repeat(int w, int h, float value)
        {
            var arr = new Array2D<float>(w, h);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    arr[x, y] = value;
                }
            }

            return arr;
        }

        //TODO .... calc it on GPU
        public static Array<float> Repeat(int w, float value)
        {
            var arr = new Array<float>(w);

            for (int x = 0; x < w; x++)
            {
                arr[x] = value;
            }

            return arr;
        }

        //TODO .... calc it on GPU
        public static IEnumerable<T> Choice<T>(IList<T> sequence, int size, float[] distribution)
        {
            double sum = 0;
            var cumulative = distribution.Select(c => {
                var result = c + sum;
                sum += c;
                return result;
            }).ToList();
            for (int i = 0; i < size; i++)
            {
                var r = th.NextDouble();
                var idx = cumulative.BinarySearch(r);
                if (idx < 0)
                    idx = ~idx;
                if (idx > cumulative.Count - 1)
                    idx = cumulative.Count - 1;
                yield return sequence[idx];
            }
        }

        public static Array<float> Euclidian(int size, int position)
        {
            return _executor.Euclidian(size, position);
        }

        public static Array2D<float> Euclidian(int w, int h, int x, int y)
        {
            return _executor.Euclidian(w, h, x, y);
        }

        public static Array<T> Max<T>(Array<T> arr0, Array<T> arr1)
            where T : struct
        {
            return _executor.Max(arr0, arr1);
        }

        public static Array<T> Min<T>(Array<T> arr0, Array<T> arr1)
            where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static void Normalize(float p0, float p1, Array2D<float> m)
        {
            _executor.Normalize(p0, p1, m);
        }

        public static void Normalize(float p0, float p1, Array<float> v)
        {
            _executor.Normalize(p0, p1, v);
        }
    }
}