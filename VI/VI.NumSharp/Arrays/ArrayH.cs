using System;
using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Arrays
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
            var size = new Index2(v1.View.Length, v0.View.Length);
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_V_M"]
                .Launch(size, output.View.View, v1.View.View, v0.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<T> operator /(ArrayH<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayH<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayH<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(ArrayH<T> v0, Array2D<T> m0)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = NumMath.Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_column_M"]
                .Launch(size, output.View.View, m0.View.View, v0.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<T> operator /(ArrayH<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayH<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayH<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
    }
}
