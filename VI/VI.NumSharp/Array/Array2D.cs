using ILGPU;
using ILGPU.Runtime;
using System;
using VI.ParallelComputing;

namespace VI.NumSharp.Array
{
    public class Array2D<T>
        where T : struct
    {
        private readonly MemoryBuffer2D<T> _memoryBuffer;
        private Array2DW<T> _w;
        private Array2DH<T> _h;

        public MemoryBuffer2D<T> View => _memoryBuffer;
        public Array2DW<T> W => _w;
        public Array2DH<T> H => _h;

        public Array2D(int w, int h)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(w, h);
            _construct();
        }
        public Array2D(T[,] data)
        {
            _memoryBuffer = ProcessingDevice.ArrayDevice.Executor.SetBuffer(data);
            _construct();
        }
        public Array2D(MemoryBuffer2D<T> memoryBuffer)
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

        public (T, Index2) FindMin()
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public (T, Index2) FindMax()
        {
            throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public Array<T> SumColumn()
        {
            var s = _memoryBuffer.Height / 2;
            var r = _memoryBuffer.Height % 2;
            while (s > 1)
            {
                var _size = new Index2(_memoryBuffer.Width, s);

                _sumLines(_size, r, _memoryBuffer);

                r = s % 2;
                s /= 2;
            }
            return new Array<T>(_joinLines(_memoryBuffer.Width, _memoryBuffer));
        }
        public Array<T> SumLine()
        {
            var s = _memoryBuffer.Width / 2;
            var r = _memoryBuffer.Width % 2;
            while (s > 1)
            {
                var _size = new Index2(s, _memoryBuffer.Height);

                _sumColumns(_size, r, _memoryBuffer);

                r = s % 2;
                s /= 2;
            }
            return new Array<T>(_joinColumns(_memoryBuffer.Height, _memoryBuffer));
        }
        public Array2D<T> Sqrt()
        {
            var size = new Index2(View.Width, View.Height);
            var mem = Allocate(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Sqrt"]
                .Launch(size, mem.View.View, View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        
        public static Array2D<T> operator *(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator /(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2D<T> m0, Array2D<T> m1)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_sum_M"]
                .Launch(size, m0.View.View, m1.View.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return m0;
        }
        public static Array2D<T> operator -(Array2D<T> m0, Array2D<T> m1)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> operator *(Array2D<T> m0, T c)
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            ProcessingDevice
                .ArrayDevice
                .Executor["_C_X_M"]
                .Launch(size, m0.View.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return m0;
        }
        public static Array2D<T> operator *(T c, Array2D<T> m0)
        {
            return m0 * c;
        }
        
        public static Array2D<T> operator /(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator +(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }
        public static Array2D<T> operator -(Array2D<T> m0, T c)
        {
              throw new NotImplementedException("Talk to the owner of the repository to implement this method (Issue)");
        }

        public static Array2D<T> Allocate(Index2 size)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X} x {size.Y}\n-----");
            return new Array2D<T>(mem);
        }
        
        private static void _sumLines(Index2 size, int r, MemoryBuffer2D<T> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, r, m.View, size.Y);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private static void _sumColumns(Index2 size, int r, MemoryBuffer2D<T> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, r, m.View, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        private static MemoryBuffer<T> _joinLines(Index size, MemoryBuffer2D<T> m)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private static MemoryBuffer<T> _joinColumns(Index size, MemoryBuffer2D<T> m)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        private void _construct()
        {
            _w = new Array2DW<T>(_memoryBuffer);
            _h = new Array2DH<T>(_memoryBuffer);
        }
    }
}
