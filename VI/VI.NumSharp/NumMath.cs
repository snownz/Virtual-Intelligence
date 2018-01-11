using System;
using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Arrays;

namespace VI.NumSharp
{
    public static class NumMath
    {
        public static Array<T> Exp<T>(Array<T> arr)
            where T: struct 
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Exp"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Tan<T>(Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Tan"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Pow<T>(Array<T> arr, int v)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Pow"]
                .Launch(size, mem.View, arr.View, v);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Sin<T>(Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Sin"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Cos<T>(Array<T> arr)
            where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Cos"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static Array<T> Sqrt<T>(Array<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
      
        public static (T, Index) FindMin<T>(Array<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static (T, Index) FindMax<T>(Array<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array<T> Max<T>(Array<T> arr0, Array<T> arr1)
            where T: struct 
        {
            var size = new Index(arr0.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Max"]
                .Launch(size, mem.View, arr0.View, arr1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        
        public static Array<T> Min<T>(Array<T> arr0, Array<T> arr1)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static float Sum(Array<float> arr)
        {
            var sum = 0f;
            for (int i = 0; i < arr.View.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        
        public static T Mult<T>(Array<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static T Sub<T>(Array<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array2D<T> Exp<T>(Array2D<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array2D<T> Sqrt<T>(Array2D<T> arr)
            where T: struct 
        {
            var size = new Index2(arr.View.Width, arr.View.Height);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Sqrt"]
                .Launch(size, mem.View.View, arr.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
      
        public static (T, Index2) FindMin<T>(Array2D<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static (T, Index2) FindMax<T>(Array2D<T> arr)
            where T: struct 
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array<T> SumColumn<T>(Array2D<T> arr)
            where T: struct
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
        
        public static Array<T> SumLine<T>(Array2D<T> arr)
            where T: struct
        {
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
        
        public static Array<T> Allocate<T>(Index size)
            where T: struct 
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return new Array<T>(mem);
        }
        public static Array2D<T> Allocate<T>(Index2 size)
            where T: struct 
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X} x {size.Y}\n-----");
            return new Array2D<T>(mem);
        }
        
        
        private static void _sumLines<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T: struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, r, m.View, size.Y);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private static void _sumColumns<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T: struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, r, m.View, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        private static MemoryBuffer<T> _joinLines<T>(Index size, MemoryBuffer2D<T> m)
            where T: struct
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private static MemoryBuffer<T> _joinColumns<T>(Index size, MemoryBuffer2D<T> m)
            where T: struct
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
    }
}