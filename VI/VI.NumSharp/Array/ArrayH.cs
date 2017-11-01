using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILGPU.Runtime;
using ILGPU;
using VI.ParallelComputing;

namespace VI.NumSharp.Array
{
    public class ArrayH<T>
        where T : struct
    {
        private MemoryBuffer<T> _memoryBuffer;

        public MemoryBuffer<T> View => _memoryBuffer;

        public ArrayH(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }

        public static Array2D<T> operator *(ArrayH<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(ArrayH<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(ArrayH<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(ArrayH<T> v0, Array<T> v1)
        {
            throw new NotImplementedException();
        }

        public static Array2D<T> operator *(ArrayH<T> v0, Array2D<T> m0)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_column_M"]
                .Launch(size, output.View, m0.View.View, v0.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return new Array2D<T>(output);
        }
        public static Array2D<T> operator /(ArrayH<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(ArrayH<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(ArrayH<T> v0, Array2D<T> m0)
        {
            throw new NotImplementedException();
        }
    }
}
