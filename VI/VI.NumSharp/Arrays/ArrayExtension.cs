using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.NumSharp.Arrays
{
    public static class ArrayExtension
    {
        public static Array2D<T> ApplyMask<T>(this Array2D<T> arr, Array2D<byte> mask)
           where T : struct
        {
            var size = new Index2(arr.View.Length);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_mask"]
                .Launch(size, arr.View, mask.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return arr;
        }

        public static Array<T> Pow<T>(this Array<T> arr, int p)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Pow"]
                .Launch(size, mem.View, arr.View, p);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array2D<T> Pow<T>(this Array2D<T> arr, int p)
            where T : struct
        {
            var size = new Index2(arr.View.Length);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Pow"]
                .Launch(size, arr.View, p);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return arr;
        }

        public static Array<T> Sqrt<T>(this Array<T> arr)
           where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static (T, Index) FindMin<T>(this Array<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static (T, Index) FindMax<T>(this Array<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array<T> Exp<T>(this Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Exp"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Tan<T>(this Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Tan"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Log<T>(this Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Log"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Sin<T>(this Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Sin"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Cos<T>(this Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Cos"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static float Sum(this Array<float> arr)
        {
            var sum = 0f;
            for (int i = 0; i < arr.View.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public static T Mult<T>(this Array<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static T Sub<T>(this Array<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> Exp<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> Sqrt<T>(this Array2D<T> arr)
            where T : struct
        {
            var size = new Index2(arr.View.Width, arr.View.Height);
            var mem = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Sqrt"]
                .Launch(size, mem.View.View, arr.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static (T, Index2) FindMin<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static (T, Index2) FindMax<T>(this Array2D<T> arr)
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array<T> SumColumn<T>(this Array2D<T> arr)
            where T : struct
        {
            var s = arr.View.Height / 2;
            var r = arr.View.Height % 2;
            while (s > 1)
            {
                var _size = new Index2(arr.View.Width, s);

                _sumLines(_size, r, arr.View);

                r = s % 2;
                s /= 2;
            }
            return new Array<T>(_joinLines(arr.View.Width, arr.View));
        }

        public static Array<T> SumLine<T>(this Array2D<T> arr)
            where T : struct
        {
            if (arr.View.Width <= 1)
                return _columns(arr.View.Height, arr.View);

            var s = arr.View.Width / 2;
            var r = arr.View.Width % 2;
            while (s > 1)
            {
                var _size = new Index2(s, arr.View.Height);

                _sumColumns(_size, r, arr.View);

                r = s % 2;
                s /= 2;
            }
            return new Array<T>(_joinColumns(arr.View.Height, arr.View));
        }
        
        private static void _sumLines<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T : struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, r, m.View, size.Y);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private static void _sumColumns<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T : struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, r, m.View, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private static MemoryBuffer<T> _joinLines<T>(Index size, MemoryBuffer2D<T> m)
            where T : struct
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private static MemoryBuffer<T> _joinColumns<T>(Index size, MemoryBuffer2D<T> m)
            where T : struct
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private static Array<T> _columns<T>(Index size, MemoryBuffer2D<T> m)
           where T : struct
        {
            var a = m.GetAsArray();
            return new Array<T>(a);
        }
    }
}
