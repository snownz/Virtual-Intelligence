using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Drivers.Data.GPU
{
    public static class ILGPUMethods
    {
        public static MemoryBuffer<T> Allocate<T>(Index size)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }

        public static MemoryBuffer<T> Allocate<T>(T[] data)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.SetBuffer(data);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }

        public static MemoryBuffer<T> Clone<T>(MemoryBuffer<T> data)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.CloneBuffer(data);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }

        public static MemoryBuffer2D<T> Allocate<T>(Index2 size)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }

        public static MemoryBuffer2D<T> Allocate<T>(T[,] data)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.SetBuffer(data);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }

        public static MemoryBuffer2D<T> Clone<T>(MemoryBuffer2D<T> data)
            where T : struct
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.FloatArrayDevice.Executor.CloneBuffer(data);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return mem;
        }
    }
}