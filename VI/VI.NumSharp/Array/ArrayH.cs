using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILGPU.Runtime;

namespace VI.NumSharp.Array
{
    public class ArrayH<T>
        where T : struct
    {
        private MemoryBuffer<T> _memoryBuffer;
        protected ArrayH(MemoryBuffer<T> memoryBuffer)
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

        public static Array2D<T> operator *(ArrayH<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(ArrayH<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(ArrayH<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(ArrayH<T> v0, Array2D<T> v1)
        {
            throw new NotImplementedException();
        }
    }
}
