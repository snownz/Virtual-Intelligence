using System;
using ILGPU.Runtime;

namespace VI.NumSharp.Arrays
{
    public class Array2DW<T>
         where T : struct
    {
        private MemoryBuffer2D<T> _memoryBuffer;

        public Array2DW(MemoryBuffer2D<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }

        public static Array2D<T> operator *(Array2DW<T> v0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(Array2DW<T> v0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2DW<T> v0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(Array2DW<T> v0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(Array2DW<T> m0, Array<T> v0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(Array2DW<T> m0, Array<T> v0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2DW<T> m0, Array<T> v0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(Array2DW<T> m0, Array<T> v0)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
    }
}
