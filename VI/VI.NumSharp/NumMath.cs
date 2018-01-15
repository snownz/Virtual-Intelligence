using System;
using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Arrays;

namespace VI.NumSharp
{
    public static class NumMath
    {
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

        public static Array<T> Max<T>(Array<T> arr0, Array<T> arr1)
            where T : struct
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
            where T : struct
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array<T> Allocate<T>(Index size)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return new Array<T>(mem);
        }

        public static Array2D<T> Allocate<T>(Index2 size)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X} x {size.Y}\n-----");
            return new Array2D<T>(mem);
        }       
    }
}