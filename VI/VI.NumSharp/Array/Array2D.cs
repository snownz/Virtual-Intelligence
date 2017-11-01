using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.ParallelComputing;

namespace VI.NumSharp.Array
{
    public class Array2D<T> 
        where T: struct
    {
        private MemoryBuffer2D<T> _memoryBuffer;
        private Array2DW<T> _w;
        private Array2DH<T> _h;

        public Array2DW<T> W => _w;
        public Array2DH<T> H => _h;

        public Array2D(int w, int h)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
        }
        public Array2D(MemoryBuffer2D<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }
        public Array2D(T[,] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
        }
        public Array2D()
        {

        }
        
        public T this[int x, int y]
        {
            get { return _memoryBuffer[new Index2(x, y)]; }
            set { _memoryBuffer[new Index2(x, y)] = value; }
        }

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        public Array<T> SumColumn()
        {
            throw new NotImplementedException();
        }
        public Array<T> SumLine()
        {
            throw new NotImplementedException();
        }
        
        public static Array2D<T> operator *(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
        }

        public static Array2D<T> operator *(Array2D<T> v0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array2D<T> v0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array2D<T> v0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array2D<T> v0, T c)
        {
            throw new NotImplementedException();
        }
    }
}
