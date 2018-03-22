using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.NumSharp.Arrays;

namespace VI.NumSharp
{
    public static class ArrayExtension
    {
        public static float[] Join(this float[] arr0, float[] arr1)
        {
            var result = new float[arr0.Length + arr1.Length];
            var jump = 0;
            for (int i = 0; i < arr0.Length; i++) result[i + jump] = arr0[i];
            jump += arr0.Length;
            for (int i = 0; i < arr1.Length; i++) result[i + jump] = arr1[i];
            return result;
        }
                
        public static float[] Join(this float[][] arr)
        {
            var result = new List<float>();
            for (int i = 0; i < arr.Length; i++)
            {
                result.AddRange(arr[i]);
            }
            return result.ToArray();
        }
          
        public static Array<T> Join<T>(this Array<T> arr0, Array<T> arr1)  where T : class
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
                Parallel.For(0, arr0.Length, i => result[i + jump] = arr0[i]);
            result[arr0.Length] = arr1;
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
            var result = new Array<FloatArray>(arr.Length);

            Parallel.For(0, arr.Length, i =>
            {
                result[i] = arr[i] + arr1[i];
            });
            return result;
        }

        public static Array<FloatArray> Divide(this Array<FloatArray> arr, int value)
        {
            var result = new Array<FloatArray>(arr.Length);

            Parallel.For(0, arr.Length, i =>
            {
                result[i] = arr[i] / (float)value;
            });
            return result;
        }

        public static Array<FloatArray2D> Divide(this Array<FloatArray2D> arr, int value)
        {
            var result = new Array<FloatArray2D>(arr.Length);

            Parallel.For(0, arr.Length, i =>
            {
                result[i] = arr[i] / value;
            });
            return result;
        }

        public static Array<FloatArray2D> Sum(this Array<FloatArray2D> arr, Array<FloatArray2D> arr1)
        {
            var result = new Array<FloatArray2D>(arr.Length);

            Parallel.For(0, arr.Length, i =>
            {
                result[i] = arr[i] + arr1[i];
            });
            return result;
        }        
        
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
