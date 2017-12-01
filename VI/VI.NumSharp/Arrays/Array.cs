using System;
using ILGPU.Runtime;
using ILGPU;

namespace VI.NumSharp.Arrays
{
    public class Array<T> : IDisposable
        where T : struct
    {
        private readonly MemoryBuffer<T> _memoryBuffer;
        private ArrayW<T> _w;
        private ArrayH<T> _h;
        private ArrayView<T> _view;

        public ArrayView<T> View => _view;
        public ArrayW<T> W => _w;
        public ArrayH<T> H => _h;

        public Array(int size)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            _view = _memoryBuffer.View;
            _construct();
        }
        public Array(T[] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
            _view = _memoryBuffer.View;
            _construct();
        }
        public Array(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
            _view = memoryBuffer.View;
            _construct();
        }
        public Array(ArrayView<T> memoryBuffer)
        {
            _view = memoryBuffer;
            _construct();
        }
        private void _construct()
        {
            _w = new ArrayW<T>(_memoryBuffer);
            _h = new ArrayH<T>(_memoryBuffer);
        }

        public T this[int x]
        {
            get => _memoryBuffer[x];
            set => _memoryBuffer[x] = value;
        }

        public string Get(int x)
        {
            return _memoryBuffer[x].ToString();
        }

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        public static Array<T> operator *(Array<T> v0, Array<T> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View, v0._memoryBuffer.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator /(Array<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<T> operator +(Array<T> v0, Array<T> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator -(Array<T> v0, Array<T> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sub_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public static Array<T> operator *(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_X_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator /(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_div_C"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator +(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sum_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator -(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sub_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator /(T c, Array<T> v0)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_div_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator +(T c, Array<T> v0)
        {
            return v0 + c;
        }
        public static Array<T> operator -(T c, Array<T> v0)
        {
            return v0 - c;
        }
        public static Array<T> operator *(T c, Array<T> v0)
        {
            return v0 * c;
        }

        public static Array2D<T> operator *(Array<T> v0, Array2D<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(Array<T> v0, Array2D<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array<T> v0, Array2D<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(Array<T> v0, Array2D<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array<T> operator ==(Array<T> v0, T c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<T> operator !=(Array<T> v0, T c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array<T> operator >(Array<T> v0, T c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<T> operator <(Array<T> v0, T c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        
        public static Array<T> operator >=(Array<T> v0, T c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_C_More_Equal"].Launch(size, output.View, c, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<T> operator <=(Array<T> v0, T c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public override string ToString()
        {
            var str = "[";
            for (int i = 0; i < _memoryBuffer.Length; i++)
            {
                str += $"{_memoryBuffer[i].ToString().Replace(",",".")}, ";
            }
            str += "]";
            return str;
        }
    }
}
