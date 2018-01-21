using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Vision.Array
{
    public class ArrayColor2D<T>
         where T : struct
    {
        private readonly MemoryBuffer2D<T> _memoryBuffer;

        public MemoryBuffer2D<T> View => _memoryBuffer;

        public ArrayColor2D(int w, int h)
        {
            _memoryBuffer = ImageProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
        }
        public ArrayColor2D(T[,] data)
        {
            _memoryBuffer = ImageProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
        }
        public ArrayColor2D(MemoryBuffer2D<T> memoryBuffer)
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
    }
}
