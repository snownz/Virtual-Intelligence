using ILGPU;
using ILGPU.Runtime;
using System;
using VI.ParallelComputing;

namespace VI.NumSharp.Array
{
    public class Array<T> : IDisposable
        where T : struct
    {
        private MemoryBuffer<T> _memoryBuffer;
        private ArrayW<T> _w;
        private ArrayH<T> _h;

        public MemoryBuffer<T> View => _memoryBuffer;
        public ArrayW<T> W => _w;
        public ArrayH<T> H => _h;

        public Array(int size) 
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            _construct();
        }       
        public Array(T[] data) 
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
            _construct();
        }
        public Array(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
            _construct();
        }
        private void _construct()
        {
            _w = new ArrayW<T>(_memoryBuffer);
            _h = new ArrayH<T>(_memoryBuffer);
        }

        public T this[int x]
        {
            get { return _memoryBuffer[x]; }
            set { _memoryBuffer[x] = value; }
        }
        
        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        public static Array<T> operator *(Array<T> v0, Array<T> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = Allocate(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View.View, v0._memoryBuffer.View, v1.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator /(Array<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array<T> operator +(Array<T> v0, Array<T> v1)
        {
            var size = v0._memoryBuffer.Length;
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, v0._memoryBuffer.View, v1.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return v0;
        }
        public static Array<T> operator -(Array<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }

        public static Array<T> operator *(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = Allocate(size);
            ProcessingDevice.ArrayDevice.Executor["_C_X_V"].Launch(size, c, output.View.View, v0.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator /(Array<T> v0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array<T> operator +(Array<T> v0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array<T> operator -(Array<T> v0, T c)
        {
            throw new NotImplementedException();
        }

        public static Array2D<T> operator *(Array<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        
        public static Array2D<T> operator *(Array<T> v0, Array2DH<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array<T> v0, Array2DH<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array<T> v0, Array2DH<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array<T> v0, Array2DH<T> v1)
        {
            throw new NotImplementedException();
        }
        
        public static Array2D<T> operator *(Array<T> v0, Array2DW<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array<T> v0, Array2DW<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array<T> v0, Array2DW<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array<T> v0, Array2DW<T> v1)
        {
            throw new NotImplementedException();
        }

        public static Array<T> Allocate(Index size)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X}\n-----");
            return new Array<T>(mem);
        }
    }
}
