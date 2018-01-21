using System;
using ILGPU.Runtime;
using ILGPU;

namespace VI.NumSharp.Arrays
{
    public class Array<K> : IDisposable
        where K : struct
    {
        private readonly MemoryBuffer<K> _memoryBuffer;
        private ArrayT<K> _t;
        private ArrayView<K> _view;

        public ArrayView<K> View => _view;
        public ArrayT<K> T => _t;

        public Array(int size)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<K>(size);
            _view = _memoryBuffer.View;
            _construct();
        }
        public Array(K[] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
            _view = _memoryBuffer.View;
            _construct();
        }
        public Array(MemoryBuffer<K> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
            _view = memoryBuffer.View;
            _construct();
        }
        public Array(ArrayView<K> memoryBuffer)
        {
            _view = memoryBuffer;
            _construct();
        }
        private void _construct()
        {
            _t = new ArrayT<K>(_memoryBuffer);
        }

        public K this[int x]
        {
            get => _memoryBuffer[x];
            set => _memoryBuffer[x] = value;
        }

        public int Length => _view.Length;

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        public static Array<K> operator *(Array<K> v0, Array<K> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View, v0._memoryBuffer.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator /(Array<K> v0, Array<K> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<K> operator +(Array<K> v0, Array<K> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator -(Array<K> v0, Array<K> v1)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sub_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public static Array<K> operator *(Array<K> v0, K c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_X_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator *(K c, Array<K> v0)
        {
            return v0 * c;
        }

        public static Array<K> operator /(Array<K> v0, K c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_div_C"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator /(K c, Array<K> v0)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_div_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public static Array<K> operator +(Array<K> v0, K c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sum_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator +(K c, Array<K> v0)
        {
            return v0 + c;
        }

        public static Array<K> operator -(Array<K> v0, K c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sub_C"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator -(K c, Array<K> v0)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sub_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        
        public static Array2D<K> operator *(Array<K> v0, Array2D<K> m0)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_line_M"]
                .Launch(size, output.View.View, m0.View.View, v0._memoryBuffer.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<K> operator *(Array2D<K> m0, Array<K> v0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<K> operator /(Array<K> v0, Array2D<K> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<K> operator /(Array2D<K> m0, Array<K> v0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<K> operator +(Array<K> v0, Array2D<K> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<K> operator +(Array2D<K> m0, Array<K> v0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<K> operator -(Array<K> v0, Array2D<K> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<K> operator -(Array2D<K> m0, Array<K> v0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        //TODO Return a single bit
        public static Array<K> operator ==(Array<K> v0, K c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<K> operator !=(Array<K> v0, K c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        //TODO Return a single bit
        public static Array<K> operator >(Array<K> v0, K c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array<K> operator <(Array<K> v0, K c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        //TODO Return a single bit
        public static Array<K> operator >=(Array<K> v0, K c)
        {
            var size = v0._memoryBuffer.Length;
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_C_More_Equal"].Launch(size, output.View, c, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array<K> operator <=(Array<K> v0, K c)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<K> As2DView(int w, int h)
        {
            var size = new Index2(w, h);
            var output = NumMath.Allocate<K>(size);
            ProcessingDevice.ArrayDevice.Executor["_1D_to_2D"].Launch(size, output.View, View, w);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array2D<K> As3DView(int w, int h, int z)
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
            str = str.Remove(str.Length - 2);
            str += "]";
            return str;
        }
    }

    public class ArrayT<T>
        where T : struct
    {
        private MemoryBuffer<T> _memoryBuffer;
        public ArrayView<T> View => _memoryBuffer.View;

        public ArrayT(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }

        public static Array2D<T> operator *(ArrayT<T> v0, Array<T> v1)
        {
            var size = new Index2(v0.View.Length, v1.View.Length);
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_V_M"]
                .Launch(size, output.View.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<T> operator *(Array<T> v0, ArrayT<T> v1)
        {
            var size = new Index2(v1.View.Length, v0.View.Length);
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_V_M"]
                .Launch(size, output.View.View, v1.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public static Array2D<T> operator /(ArrayT<T> v0, Array<T> v1)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayT<T> v0, Array<T> v1)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayT<T> v0, Array<T> v1)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(ArrayT<T> v0, Array2D<T> m0)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_column_M"]
                .Launch(size, output.View.View, m0.View.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<T> operator /(ArrayT<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayT<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayT<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
    }
}
