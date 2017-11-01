using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.ParallelComputing;
using VI.ParallelComputing.ANN;

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

        public Array(Index size)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
        }
        public Array()
        {

        }
        public Array(T[] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
        }
        public Array(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
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
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(v0._memoryBuffer.Length);
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output, v0._memoryBuffer.View, v1.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return new Array<T>(output);
        }
        public static Array<T> operator /(Array<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array<T> operator +(Array<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array<T> operator -(Array<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }

        public static Array<T> operator *(Array<T> v0, T c)
        {
            throw new NotImplementedException();
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
    }
}
