using System;
using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Arrays
{
    public class ArrayW<T>
        where T : struct
    {
        private MemoryBuffer<T> _memoryBuffer;
        public ArrayW(MemoryBuffer<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }

        public static Array2D<T> operator *(ArrayW<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(ArrayW<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayW<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayW<T> v0, Array<T> v1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(ArrayW<T> v0, Array2D<T> m0)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Array2D<T>.Allocate(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_line_M"]
                .Launch(size, output.View.View, m0.View.View, v0._memoryBuffer.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public static Array2D<T> operator /(ArrayW<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(ArrayW<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(ArrayW<T> v0, Array2D<T> m0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
    }
}
