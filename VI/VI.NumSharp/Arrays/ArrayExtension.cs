using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using VI.NumSharp.Provider;

namespace VI.NumSharp.Arrays
{
    public static class ArrayExtension
    {
        private static IArrayExecutor _executor = ProcessingDevice.ArrayExecutorResolver();

        //TODO .... calc it on GPU
        public static (float value, int x) FindMin(this Array<float> arr)
        {
            var min = float.MaxValue;
            var pos = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (min > arr[i])
                {
                    min = arr[i];
                    pos = i;
                }
            }

            return (min, pos);
        }

        //TODO .... calc it on GPU
        public static (float value, int x) FindMax(this Array<float> arr)
        {
            var max = 0f;
            var pos = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                    pos = i;
                }
            }

            return (max, pos);
        }

        //TODO .... calc it on GPU
        public static float Sum(this Array<float> arr)
        {
            var sum = 0f;
            for (int i = 0; i < arr.View.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public static (T, (int x, int y)) FindMin<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static (T, (int x, int y)) FindMax<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static T Mult<T>(this Array<T> arr)
          where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static T Sub<T>(this Array<T> arr)
            where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> Exp<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static Array<T> Pow<T>(this Array<T> arr, int p)
            where T : struct
        {
            return _executor.Pow(arr, p);
        }
        
        public static Array<T> Sqrt<T>(this Array<T> arr)
           where T : struct
        {
            return _executor.Sqrt(arr);
        }
        
        public static Array<T> Exp<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Exp(arr);
        }

        public static Array<T> Tan<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Tan(arr);
        }
        
        public static Array<T> Tanh<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Tanh(arr);
        }

        public static Array<T> Log<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Log(arr);
        }

        public static Array<T> Sin<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Sin(arr);
        }

        public static Array<T> Cos<T>(this Array<T> arr)
            where T : struct
        {
            return _executor.Cos(arr);
        }

        public static Array2D<T> Pow<T>(this Array2D<T> arr, int p)
           where T : struct
        {
            return _executor.Pow(arr, p);
        }

        public static Array2D<T> ApplyMask<T>(this Array2D<T> arr, Array2D<byte> mask)
             where T : struct
        {
            return _executor.ApplyMask(arr, mask);
        }

        public static Array2D<T> Sqrt<T>(this Array2D<T> arr)
            where T : struct
        {
            return _executor.Sqrt(arr);
        }
        
        public static Array<T> SumColumn<T>(this Array2D<T> arr)
            where T : struct
        {
            return _executor.SumColumn(arr);
        }
        public static Array<T> SumLine<T>(this Array2D<T> arr)
            where T : struct
        {
            return _executor.SumLine(arr);
        }
    }
}
