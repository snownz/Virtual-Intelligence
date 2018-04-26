using System;
using System.Collections.Generic;
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
    public class FloatArray : IArray, IDisposable
    {
        private IFloatData _view;
        private readonly IFloatData cache;

        public IFloatData View
        {
            get { return _view; }
            set { _view = value; }
        }

        public IFloatData Cache => cache;

        public FloatArray()
        {
        }

        public FloatArray(int size)
        {
            _view = ProcessingDevice.FloatData.New(size);
            cache = ProcessingDevice.FloatData.New(size);
        }

        public FloatArray(float[] data)
        {
            _view = ProcessingDevice.FloatData.New(data);
            cache = ProcessingDevice.FloatData.New(Length);
        }

        public FloatArray(IFloatData data)
        {
            _view = data;
            cache = ProcessingDevice.FloatData.New(Length);
        }

        public float this[int x]
        {
            get
            {
                if (x < 0) x = Length - x;
                return _view[x];
            }
            set
            {
                if (x < 0) x = Length - x;

                _view[x] = value;
            }
        }

        public int Length => _view.Length;
        public FloatArrayT T => new FloatArrayT(_view);

        public static FloatArray operator *(FloatArray v0, FloatArray v1)
        {
            if(v0 == null) return v1;
            if(v1 == null) return v0;
            return new FloatArray(ProcessingDevice.FloatExecutor.V_mult_V(ProcessingDevice.FloatData.New(v0.Length), v0._view, v1._view));
        }

        public static FloatArray operator /(FloatArray v0, FloatArray v1)
        {
            if(v0 == null) return v1;
            if(v1 == null) return v0;
            return new FloatArray(ProcessingDevice.FloatExecutor.V_div_V(ProcessingDevice.FloatData.New(v0.Length), v0._view, v1._view));
        }

        public static FloatArray operator +(FloatArray v0, FloatArray v1)
        {
            if(v0 == null) return v1;
            if(v1 == null) return v0;
            return new FloatArray(ProcessingDevice.FloatExecutor.V_add_V(ProcessingDevice.FloatData.New(v0.Length), v0._view, v1._view));
        }

        public static FloatArray operator -(FloatArray v0, FloatArray v1)
        {
            if(v0 == null) return v1;
            if(v1 == null) return v0;
            return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_V(ProcessingDevice.FloatData.New(v0.Length), v0._view, v1._view));
        }

        public static FloatArray operator *(FloatArray v0, float c)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_mult_C(ProcessingDevice.FloatData.New(v0.Length), v0._view, c));
        }

        public static FloatArray operator *(float c, FloatArray v0)
        {
            return v0 * c;
        }

        public static FloatArray operator /(FloatArray v0, float c)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_div_C(ProcessingDevice.FloatData.New(v0.Length), v0._view, c));
        }

        public static FloatArray operator /(float c, FloatArray v0)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_div_C(ProcessingDevice.FloatData.New(v0.Length), c, v0._view));
        }

        public static FloatArray operator +(FloatArray v0, float c)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_add_C(ProcessingDevice.FloatData.New(v0.Length), v0._view, c));
        }

        public static FloatArray operator +(float c, FloatArray v0)
        {
            return v0 + c;
        }

        public static FloatArray operator -(FloatArray v0, float c)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_C(ProcessingDevice.FloatData.New(v0.Length), v0._view, c));
        }

        public static FloatArray operator -(float c, FloatArray v0)
        {
            return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_C(ProcessingDevice.FloatData.New(v0.Length), c, v0._view));
        }

        //TODO Code on executor
        public static FloatArray operator >=(FloatArray v0, float c)
        {
            var output = new float[v0.Length];
            for (int x = 0; x < v0.Length; x++)
            {
                output[x] = v0[x] >= c ? 1 : 0;
            }
            return new FloatArray(output);
        }

        //TODO Code on executor
        public static FloatArray operator <=(FloatArray v0, float c)
        {
            var output = new float[v0.Length];
            for (int x = 0; x < v0.Length; x++)
            {
                output[x] = v0[x] <= c ? 1 : 0;
            }
            return new FloatArray(output);
        }
        
        //TODO Code on executor
        public static FloatArray operator >(FloatArray v0, float c)
        {
            var output = new float[v0.Length];
            for (int x = 0; x < v0.Length; x++)
            {
                output[x] = v0[x] > c ? 1 : 0;
            }
            return new FloatArray(output);
        }

        //TODO Code on executor
        public static FloatArray operator <(FloatArray v0, float c)
        {
             var output = new float[v0.Length];
            for (int x = 0; x < v0.Length; x++)
            {
                output[x] = v0[x] < c ? 1 : 0;
            }
            return new FloatArray(output);
        }

        public List<float> ToList()
        {
            var lt = new List<float>();
            for (int x = 0; x < Length; x++)
            {
                lt.Add(_view[x]);
            }
            return lt;
        }

        public float[] ToArray()
        {
            var lt = new float[Length];
            for (int x = 0; x < Length; x++)
            {
                lt[x] = _view[x];
            }
            return lt;
        }

        public FloatArray Clone()
        {
            return new FloatArray(_view.Clone());
        }

        public override string ToString()
        {
            var str = "[";
            for (var i = 0; i < _view.Length; i++) str += $"{_view[i].ToString().Replace(",", ".")}, ";
            str = str.Remove(str.Length - 2);
            str += "]";
            return str;
        }

        public void Dispose()
        {
        }
    }

    public class FloatArrayT
    {
        private readonly IFloatData _view;

        public IFloatData View => _view;

        public FloatArrayT(IFloatData data)
        {
            _view = data;
        }

        public float this[int x]
        {
            get => _view[x];
            set => _view[x] = value;
        }

        public int Length => _view.Length;

        public static FloatArray2D operator *(FloatArrayT vt, FloatArray v)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_V(vt._view, v.View));
        }

        public static FloatArray2D operator *(FloatArray v, FloatArrayT vt)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_V(vt.View, v.View));
        }

        public static FloatArray2D operator *(FloatArrayT vt, FloatArray2D m)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_M(m.Cache, vt._view, m.View));
        }
    }
}