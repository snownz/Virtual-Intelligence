using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
    public class ByteDataCPU : IByteDataProcess
    {
        public IByteData New(int size)
        {
            return new CPU_ByteData(size);
        }

        public IByteData New(byte[] data)
        {
            return new CPU_ByteData(data);
        }

        public IByteData2D New(int w, int h)
        {
            return new CPU_ByteData2D(w, h);
        }

        public IByteData2D New(byte[,] data)
        {
            return new CPU_ByteData2D(data);
        }
    }
}