using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;

namespace VI.ParallelComputing
{
    public class ParalleExecutorlInterface : IDisposable
    {
        private Accelerator _accelerator;
        private Dictionary<string, Kernel> _kernels;

        public Kernel this[string kn] { get { return _kernels[kn]; } }

        public ParalleExecutorlInterface(Accelerator accelerator, Dictionary<string, Kernel> kernels)
        {
            _accelerator = accelerator;
            _kernels = kernels;
        }

        public MemoryBuffer<T> CreateBuffer<T>(int size)
            where T : struct
        {
            var buffer = _accelerator.Allocate<T>(size);
            return buffer;
        }
        public MemoryBuffer2D<T> CreateBuffer<T>(int w, int h)
           where T : struct
        {
            var buffer = _accelerator.Allocate<T>(w, h);
            return buffer;
        }
        public MemoryBuffer2D<T> CreateBuffer<T>(Index2 index)
          where T : struct
        {
            var buffer = _accelerator.Allocate<T>(index.X, index.Y);
            return buffer;
        }

        public MemoryBuffer<T> SetBuffer<T>(T[] obj)
            where T : struct
        {
            var buffer = _accelerator.Allocate<T>(obj.Length);
            buffer.CopyFrom(obj, 0, 0, obj.Length);
            return buffer;
        }
        public MemoryBuffer2D<T> SetBuffer<T>(T[,] obj)
           where T : struct
        {
            var w = obj.GetLength(0);
            var h = obj.GetLength(1);

            var buffer = _accelerator.Allocate<T>(w, h);
            buffer.CopyFrom(obj, new Index2(0, 0), new Index2(0, 0), new Index2(w, h));
            return buffer;
        }

        public void Wait()
        {
            _accelerator.Synchronize();
        }

        public void Dispose()
        {
            foreach (var kn in _kernels)
            {
                kn.Value.Dispose();
            }
            _kernels.Clear();
            _accelerator.Dispose();
        }
    }
}
