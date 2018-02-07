using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Arrays
{
    public class Array3D<T>
        where T : struct
    {
        private readonly MemoryBuffer3D<T> _memoryBuffer;

        public MemoryBuffer3D<T> View => _memoryBuffer;

        public Array3D(int w, int h, int z)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h, z);
        }
        public Array3D(T[,,] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
        }
        public Array3D(MemoryBuffer3D<T> memoryBuffer)
        {
            _memoryBuffer = memoryBuffer;
        }

        public T this[int x, int y, int z]
        {
            get => _memoryBuffer[new Index3(x, y, z)];
            set => _memoryBuffer[new Index3(x, y, z)] = value;
        }

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        public Array<T> AsLinear()
        {
            return new Array<T>(_memoryBuffer.AsLinearView());
        }
    }
}
