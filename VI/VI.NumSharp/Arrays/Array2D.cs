using System;
using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Arrays
{
    public class Array2D<T>
        where T : struct
    {
        private readonly MemoryBuffer2D<T> _memoryBuffer;
        private Array2DW<T> _w;
        private Array2DH<T> _h;

        public MemoryBuffer2D<T> View => _memoryBuffer;
        public Array2DW<T> W => _w;
        public Array2DH<T> H => _h;

        public Array2D(int w, int h)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
            _construct();
        }
        public Array2D(T[,] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
            _construct();
        }
        public Array2D(MemoryBuffer2D<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }
        
        public T this[int x, int y]
        {
            get => _memoryBuffer[new Index2(x, y)];
            set => _memoryBuffer[new Index2(x, y)] = value;
        }

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }
        
        public static Array2D<T> operator *(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2D<T> m0, Array2D<T> m1)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_sum_M"]
                .Launch(size, m0.View.View, m1.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return m0;
        }
        public static Array2D<T> operator -(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(Array2D<T> m0, T c)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_C_X_M"]
                .Launch(size, output.View, m0.View.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return new Array2D<T>(output);
        }
        public static Array2D<T> operator *(T c, Array2D<T> m0)
        {
            return m0 * c;
        }
        
        public static Array2D<T> operator /(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        private void _construct()
        {
            _w = new Array2DW<T>(_memoryBuffer);
            _h = new Array2DH<T>(_memoryBuffer);
        }

        public override string ToString()
        {
            var str = "[";
            for (int i = 0; i < _memoryBuffer.Width; i++)
            {
                str += "[";
                for (int j = 0; j < _memoryBuffer.Height; j++)
                {
                    str += $"{_memoryBuffer[new Index2(i, j)].ToString().Replace(",", ".")}, ";
                }
                str += "]";
            }
            str += "]";
            return str;
        }
    }
}
