using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VI.NumSharp.Arrays;

namespace VI.NumSharp
{
    public static class ArrayExtension
    {
        public static Array<T> Join<T>(this Array<T> arr0, Array<T> arr1) where T : class
        {
            var result = new Array<T>(arr0.Length + arr1.Length);
            var jump = 0;
            Parallel.For(0, arr0.Length, i => result[i + jump] = arr0[i]);
            jump += arr0.Length;
            Parallel.For(0, arr1.Length, i => result[i + jump] = arr1[i]);
            return result;
        }

        public static Array<T> Join<T>(this Array<T> arr0, T arr1) where T : class
        {
            var result = new Array<T>(arr0.Length + 1);
            var jump = 0;
            if (arr0.Length > 0)
            {
                Parallel.For(0, arr0.Length, i => result[i + jump] = arr0[i]);
            }
            result[arr0.Length] = arr1;
            return result;
        }

        public static Array<T> AsArray<T>(this List<T> arr) where T : class
        {
            var result = new Array<T>(arr.Count);

            for (int i = 0; i < arr.Count; i++)
            {
                result[i] = arr[i];
            }

            return result;
        }

        public static Array<FloatArray> Join(this FloatArray arr0, FloatArray arr1)
        {
            var result = new Array<FloatArray>(2);
            result[0] = arr0;
            result[1] = arr1;
            return result;
        }

        public static FloatArray Union(this FloatArray arr0, FloatArray arr1)
        {
            var result = new FloatArray(arr0.Length + arr1.Length);
            var jump = 0;
            Parallel.For(0, arr0.Length, i => result[i + jump] = arr0[i]);
            jump += arr0.Length;
            Parallel.For(0, arr1.Length, i => result[i + jump] = arr1[i]);
            return result;
        }

        public static Array<FloatArray> Join(this IList<Array<FloatArray>> arr)
        {
            var result = arr[0];
            Parallel.For(0, arr.Count, i => result = result.Join(arr[i]));
            return result;
        }

        public static Array<FloatArray> Sum(this Array<FloatArray> arr, Array<FloatArray> arr1)
        {
            if (arr == null) return arr1;
            if (arr1 == null) return arr;

            var result = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => result[i] = arr[i] + arr1[i]);
            return result;
        }

        public static FloatArray Sum(this Array<FloatArray> arr)
        {
            if (arr.Length < 2) return arr[0];

            var result = new FloatArray(arr[0].Length);

            var s = arr.Length / 2d;
            var r = arr.Length % 2d;

            while (s > 1)
            {
                var size = (int)s;
                var rest = (int)Math.Ceiling(r);

                Parallel.For(0, size, i => arr[i] = arr[i] + arr[rest + size + i]);

                r = s % 2;
                s /= 2;
            }

            return arr[0] + arr[1];
        }

        public static Array<FloatArray> Divide(this Array<FloatArray> arr, int value)
        {
            var result = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => result[i] = arr[i] / (float)value);
            return result;
        }

        public static Array<FloatArray2D> Divide(this Array<FloatArray2D> arr, int value)
        {
            var result = new Array<FloatArray2D>(arr.Length);
            Parallel.For(0, arr.Length, i => result[i] = arr[i] / value);
            return result;
        }
        
        public static Array<FloatArray> Fill(this Array<FloatArray> arr, int size)
        {
            Parallel.For(0, arr.Length, i => arr[i] = new FloatArray(size));
            return arr;
        }

        public static Array<FloatArray2D> Sum(this Array<FloatArray2D> arr, Array<FloatArray2D> arr1)
        {
            if(arr == null) return arr1;
            if(arr1 == null) return arr;

            var result = new Array<FloatArray2D>(arr.Length);
            Parallel.For(0, arr.Length, i => result[i] = arr[i] + arr1[i]);
            return result;
        }

        public static Array<Array<FloatArray2D>> Sum(this Array<Array<FloatArray2D>> arr, Array<Array<FloatArray2D>> arr1)
        {
            if(arr == null) return arr1;
            if(arr1 == null) return arr;

            var result = new Array<Array<FloatArray2D>>(arr.Length);
            Parallel.For(0, arr.Length, i => result[i] = arr[i].Sum(arr1[i]));
            return result;
        }

        public static Array<FloatArray> Split(this FloatArray f, int size)
        {
            if (f.Length % 2 != 0)
                throw new InvalidOperationException();

            var p = f.Length / size;

            var a = new Array<FloatArray>(size);

            for (int i = 0; i < size; i++)
            {
                a[i] = new FloatArray(p);
                for (int j = 0; j < p; j++)
                {
                    a[i][j] = f[i * p + j];
                }
            }

            return a;
        }

        public static Array<FloatArray> Unuion(this FloatArray i, FloatArray a)
        {
            var l = new Array<FloatArray>(2);
            l[0] = i;
            l[1] = a;
            return l;
        }

        public static Array<FloatArray> Multiply(this Array<FloatArray> arr, FloatArray arr1)
        {
            var result = new Array<FloatArray>(arr.Length);

            Parallel.For(0, arr.Length, i =>
            {
                result[i] = arr[i] * arr1;
            });
            return result;
        }
    }
}