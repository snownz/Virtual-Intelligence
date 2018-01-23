using System;
using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Provider;

namespace VI.NumSharp.Arrays
{
    public class Array2D<T>
        where T : struct
    {
        private static IArrayExecutor _executor = ProcessingDevice.ArrayExecutorResolver();

        private readonly MemoryBuffer2D<T> _memoryBuffer;

        public ArrayView2D<T> View => _memoryBuffer.View;
        public MemoryBuffer2D<T> Memory => _memoryBuffer;

        public Index2 Size => new Index2(_memoryBuffer.Width, _memoryBuffer.Height);
        
        public Array2D(int w, int h)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
        }
        public Array2D(Index2 size)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
        }
        public Array2D(T[,] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
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
            return _executor.M_mult_M(m0, m1);
        }
        public static Array2D<T> operator /(Array2D<T> m0, Array2D<T> m1)
        {
            return _executor.M_div_M(m0, m1);
        }
        public static Array2D<T> operator +(Array2D<T> m0, Array2D<T> m1)
        {
            return _executor.M_add_M(m0, m1);
        }
        public static Array2D<T> operator -(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(Array2D<T> m0, T c)
        {
            return _executor.M_mult_C(m0, c);
        }
        public static Array2D<T> operator *(T c, Array2D<T> m0)
        {
            return m0 * c;
        }
        
        public static Array2D<T> operator /(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2D<T> m0, T c)
        {
            return _executor.M_add_C(m0, c);
        }
        public static Array2D<T> operator -(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<byte> operator ==(Array2D<T> m0, T c)
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<byte> operator !=(Array2D<T> m0, T c)
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<byte> operator >(Array2D<T> m0, T c)
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<byte> operator <(Array2D<T> m0, T c)
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<byte> operator >=(Array2D<T> m0, T c)
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<byte> operator <=(Array2D<T> m0, T c)
        {
            return _executor.M_less_equal_C(m0, c);
        }
        
        public override string ToString()
        {
            var str = "[";
            for (int j = 0; j < _memoryBuffer.Height; j++) 
            {
                str += "[";
                for (int i = 0; i < _memoryBuffer.Width; i++)
                {
                    str += $"{_memoryBuffer[new Index2(i, j)].ToString().Replace(",", ".")}, ";
                }
                str = str.Remove(str.Length - 2);
                str += "],";
            }
            str = str.Remove(str.Length - 1);
            str += "]";
            return str;
        }

        public Array<T> AsLinear()
        {
            return new Array<T>(_memoryBuffer.AsLinearView());
        }
    }
}
