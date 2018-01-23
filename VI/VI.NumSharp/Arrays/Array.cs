using System;
using System.Collections.Generic;
using ILGPU.Runtime;
using ILGPU;
using VI.NumSharp.Provider;

namespace VI.NumSharp.Arrays
{
    public class Array<K> : IDisposable
        where K : struct
    {
        private static IArrayExecutor _executor = ProcessingDevice.ArrayExecutorResolver();

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
            _t = new ArrayT<K>(_memoryBuffer, _executor);
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
            return _executor.V_mult_V(v0, v1);           
        }
        public static Array<K> operator /(Array<K> v0, Array<K> v1)
        {
            return _executor.V_div_V(v0, v1);
        }
        public static Array<K> operator +(Array<K> v0, Array<K> v1)
        {
            return _executor.V_add_V(v0, v1);
        }
        public static Array<K> operator -(Array<K> v0, Array<K> v1)
        {
            return _executor.V_sub_V(v0, v1);
        }

        public static Array<K> operator *(Array<K> v0, K c)
        {
            return _executor.V_mult_C(v0, c);
        }
        public static Array<K> operator *(K c, Array<K> v0)
        {
            return _executor.C_mult_V(c, v0);
        }

        public static Array<K> operator /(Array<K> v0, K c)
        {
            return _executor.V_div_C(v0, c);
        }
        public static Array<K> operator /(K c, Array<K> v0)
        {
            return _executor.C_div_V(c, v0);
        }

        public static Array<K> operator +(Array<K> v0, K c)
        {
            return _executor.V_add_C(v0, c);
        }
        public static Array<K> operator +(K c, Array<K> v0)
        {
            return _executor.C_add_V(c, v0);
        }

        public static Array<K> operator -(Array<K> v0, K c)
        {
            return _executor.V_sub_C(v0, c);
        }
        public static Array<K> operator -(K c, Array<K> v0)
        {
            return _executor.C_sub_V(c, v0);
        }
        
        public static Array2D<K> operator *(Array<K> v0, Array2D<K> m0)
        {
            return _executor.V_mult_M(v0, m0);
        }
        public static Array2D<K> operator *(Array2D<K> m0, Array<K> v0)
        {
            return _executor.M_mult_V(m0, v0);
        }

        public static Array2D<K> operator /(Array<K> v0, Array2D<K> m0)
        {
            return _executor.V_div_M(v0, m0);
        }
        public static Array2D<K> operator /(Array2D<K> m0, Array<K> v0)
        {
            return _executor.M_div_V(m0, v0);
        }

        public static Array2D<K> operator +(Array<K> v0, Array2D<K> m0)
        {
            return _executor.V_add_M(v0, m0);
        }
        public static Array2D<K> operator +(Array2D<K> m0, Array<K> v0)
        {
            return _executor.M_add_V(m0, v0);
        }

        public static Array2D<K> operator -(Array<K> v0, Array2D<K> m0)
        {
            return _executor.V_sub_M(v0, m0);
        }
        public static Array2D<K> operator -(Array2D<K> m0, Array<K> v0)
        {
            return _executor.M_sub_V(m0, v0);
        }

        //TODO Return a single bit
        public static Array<K> operator ==(Array<K> v0, K c)
        {
            return _executor.V_equal_C(v0, c);
        }
        public static Array<K> operator !=(Array<K> v0, K c)
        {
            return _executor.V_dif_C(v0, c);
        }
        //TODO Return a single bit
        public static Array<K> operator >(Array<K> v0, K c)
        {
            return _executor.V_greater_C(v0, c);
        }
        public static Array<K> operator <(Array<K> v0, K c)
        {
            return _executor.V_less_C(v0, c);
        }
        //TODO Return a single bit
        public static Array<K> operator >=(Array<K> v0, K c)
        {
            return _executor.V_greater_equal_C(v0, c);
        }
        public static Array<K> operator <=(Array<K> v0, K c)
        {
            return _executor.V_less_equal_C(v0, c);
        }

        public Array2D<K> As2DView(int w, int h)
        {
            return _executor.As2DView(this, w, h);
        }        
        public Array3D<K> As3DView(int w, int h, int z)
        {
            return _executor.As3DView(this, w, h, z);
        }

        public List<K> ToList()
        {
            var lt = new List<K>();
            for (int i = 0; i < _memoryBuffer.Length; i++)
            {
                lt.Add(_memoryBuffer[i]);
            }
            return lt;
        }
        
        public K[] ToArray()
        {
            var lt = new K[_memoryBuffer.Length];
            for (int i = 0; i < _memoryBuffer.Length; i++)
            {
                lt[i] =_memoryBuffer[i];
            }
            return lt;
        }

        public Array<K> Clone()
        {
            return new Array<K>(ProcessingDevice.ArrayDevice.Executor.CloneBuffer(_memoryBuffer));
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
        private static IArrayExecutor _executor;

        private MemoryBuffer<T> _memoryBuffer;
        public ArrayView<T> View => _memoryBuffer.View;

        public ArrayT(MemoryBuffer<T> memoryBuffer, IArrayExecutor executor)
        {
            _memoryBuffer = memoryBuffer;
            _executor = executor;
        }

        public static Array2D<T> operator *(ArrayT<T> v0, Array<T> v1)
        {
            return _executor.Transpose_V_mult_V(v0, v1);          
        }
        public static Array2D<T> operator *(Array<T> v0, ArrayT<T> v1)
        {
            return _executor.V_multi_Transpose_V(v0, v1);            
        }

        public static Array2D<T> operator /(ArrayT<T> v0, Array<T> v1)
        {
            return _executor.Transpose_V_div_V(v0, v1);            
        }
        public static Array2D<T> operator +(ArrayT<T> v0, Array<T> v1)
        {
            return _executor.Transpose_V_add_V(v0, v1);            
        }
        public static Array2D<T> operator -(ArrayT<T> v0, Array<T> v1)
        {
            return _executor.Transpose_V_sub_V(v0, v1);
        }

        public static Array2D<T> operator *(ArrayT<T> v0, Array2D<T> m0)
        {
            return _executor.Transpose_V_multi_M(v0, m0);           
        }
        public static Array2D<T> operator /(ArrayT<T> v0, Array2D<T> m0)
        {
            return _executor.Transpose_V_div_M(v0, m0);            
        }
        public static Array2D<T> operator +(ArrayT<T> v0, Array2D<T> m0)
        {
            return _executor.Transpose_V_add_M(v0, m0);            
        }
        public static Array2D<T> operator -(ArrayT<T> v0, Array2D<T> m0)
        {
            return _executor.Transpose_V_sub_M(v0, m0);            
        }
    }
}
