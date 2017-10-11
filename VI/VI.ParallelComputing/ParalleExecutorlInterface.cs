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

        public void Execute<T>(string kernelName, Index size, ArrayView<T> result)
            where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result);
            _accelerator.Synchronize();
        }
        public void Execute<T>(string kernelName, 
                               Index2 size, 
                               ArrayView<T> result,
                               ArrayView<T> v1,
                               ArrayView2D<T> m1)
            where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result, v1, m1);
            _accelerator.Synchronize();
        }
        public void Execute<T>(string kernelName,
                              Index2 size,
                              ArrayView2D<T> result,
                              ArrayView<T> v1,
                              ArrayView<T> v2,
                              float c1,
                              float c2)
           where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result, v1, v2, c1, c2);
            _accelerator.Synchronize();
        }
        public void Execute<T>(string kernelName,
                             Index size,
                             ArrayView<T> result,
                             ArrayView<T> v1,
                             float c1,
                             float c2)
          where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result, v1, c1, c2);
            _accelerator.Synchronize();
        }
        public void Execute<T>(string kernelName,
                             Index size,
                             ArrayView<T> result,
                             ArrayView<T> v1)
          where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result, v1);
            _accelerator.Synchronize();
        }
        public void Execute<T>(string kernelName,
                             Index size,
                             ArrayView<T> result,
                             ArrayView<T> v1,
                             ArrayView<T> v2)
          where T : struct
        {
            var kernel = _kernels[kernelName];
            kernel.Launch(size, result, v1, v2);
            _accelerator.Synchronize();
        }

        public T[] GetInformation<T>(MemoryBuffer<T> memory)
            where T : struct
        {
            var data = memory.GetAsArray();
            return data;
        }
        public T[,] GetInformation<T>(MemoryBuffer2D<T> memory, int w, int h)
            where T : struct
        {
            var targetData = new T[w, h];

            memory.CopyTo(
                      targetData,         // data target
                      new Index2(0, 0),   // source index in the scope of the data source
                      new Index2(),       // target index in the scope of the buffer
                      new Index2(w, h));  // the number of elements to copy

            return targetData;
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
