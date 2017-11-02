using ILGPU;
using ILGPU.Runtime;
using System;
using VI.ParallelComputing;

namespace VI.NumSharp.Array
{
    public class Array2D<T> 
        where T: struct
    {
        private MemoryBuffer2D<T> _memoryBuffer;
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
        private void _construct()
        {
            _w = new Array2DW<T>(_memoryBuffer);
            _h = new Array2DH<T>(_memoryBuffer);
        }        
        public T this[int x, int y]
        {
            get { return _memoryBuffer[new Index2(x, y)]; }
            set { _memoryBuffer[new Index2(x, y)] = value; }
        }

        public void Dispose()
        {
            _memoryBuffer.Dispose();
        }

        private Array<T> SumColumnLoop()
        {
            var arr = Array<T>.Allocate(View.Width);
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            ProcessingDevice.ArrayDevice.Executor["_M_sum_loop"].Launch(View.Width, arr.View.View, View.View, View.Height);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            //watch.Stop();
            //Console.WriteLine($"Array Sum Time: {watch.ElapsedMilliseconds}ms Size {_memoryBuffer.Width} x {_memoryBuffer.Height}");
            return arr;
        }
        private Array<T> SumColumnPrallel()
        {
            var s = _memoryBuffer.Height / 2;
            var r = _memoryBuffer.Height % 2;
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            while (s > 1)
            {
                var _size = new Index2(_memoryBuffer.Width, s);

                _sumLines(_size, r, _memoryBuffer);

                r = s % 2;
                s /= 2;
            }
            //watch.Stop();
            //Console.WriteLine($"Array Sum Time: {watch.ElapsedMilliseconds}ms Size {_memoryBuffer.Width} x {_memoryBuffer.Height}");
            return new Array<T>(_joinLines(_memoryBuffer.Width, _memoryBuffer));
        }

        public Array<T> SumColumn()
        {
            if(ProcessingDevice.Device == Device.CPU)
            {
                return SumColumnLoop();
            }
            else
            {
                return SumColumnPrallel();
            }
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
        
        private void _sumLines(Index2 size, int r, MemoryBuffer2D<T> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, r, m.View, size.Y);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private void _sumColumns(Index2 size, int r, MemoryBuffer2D<T> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, r, m.View, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        
        private MemoryBuffer<T> _joinLines(Index size, MemoryBuffer2D<T> m)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private MemoryBuffer<T> _joinColumns(Index size, MemoryBuffer2D<T> m)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public static Array2D<T> operator *(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator /(Array2D<T> m0, Array2D<T> m1)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        public static Array2D<T> operator /(Array2D<T> m0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator +(Array2D<T> m0, T c)
        {
            throw new NotImplementedException();
        }
        public static Array2D<T> operator -(Array2D<T> m0, T c)
        {
            throw new NotImplementedException();
        }

        public static Array2D<T> Allocate(Index2 size)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            //watch.Stop();
            //Console.WriteLine($"\n-----\nAllocation Time: {watch.ElapsedMilliseconds}ms\nSize {size.X} x {size.Y}\n-----");
            return new Array2D<T>(mem);
        }
    }
}
