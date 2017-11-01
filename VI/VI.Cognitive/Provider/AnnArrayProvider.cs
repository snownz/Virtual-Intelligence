using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.ParallelComputing;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Provider
{
    public class AnnArrayProvider : IAnnArrayProvider
    {
        public AnnArrayProvider()
        {

        }

        public void _M_sum_line_V(Index2 size, MemoryBuffer<float> v, MemoryBuffer2D<float> m)
        {
            var s = size.Y / 2;
            var r = size.Y % 2;

            while (s > 2)
            {
                var _size = new Index2(size.X, s);

                _M_sum_lines(_size, r, m);

                r = s % 2;
                s /= 2;
            }

            _M_2_lines_V(size.X, v, m);
        }

        public void _M_sum_column_V(Index2 size, MemoryBuffer<float> v, MemoryBuffer2D<float> m)
        {
            var s = size.X / 2;
            var r = size.X % 2;

            while (s > 2)
            {
                var _size = new Index2(s, size.Y);

                _M_sum_columns(_size, r, m);

                r = s % 2;
                s /= 2;
            }

            _M_2_columns_V(size.Y, v, m);
        }

        public void _M_sum_lines(Index2 size, int r, MemoryBuffer2D<float> mat)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, mat.View, r, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _M_sum_columns(Index2 size, int r, MemoryBuffer2D<float> mat)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, mat.View, r, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _M_2_lines_V(Index size, MemoryBuffer<float> v, MemoryBuffer2D<float> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, v.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _M_2_columns_V(Index size, MemoryBuffer<float> v, MemoryBuffer2D<float> m)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, v.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _V_zeros(Index size, MemoryBuffer<float> v)
        {
            ProcessingDevice.ArrayDevice.Executor["_V_zeros"].Launch(size, v.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _V_X_V(Index size, MemoryBuffer<float> output, MemoryBuffer<float> v0, MemoryBuffer<float> v1)
        {
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _V_X_V_M(Index2 size, MemoryBuffer2D<float> output, MemoryBuffer<float> v0, MemoryBuffer<float> v1)
        {
            ProcessingDevice.ArrayDevice.Executor["_V_X_V_M"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _C_X_M(Index2 size, MemoryBuffer2D<float> m, float c)
        {
            ProcessingDevice.ArrayDevice.Executor["_C_X_M"].Launch(size, m.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _M_sum_M(Index2 size, MemoryBuffer2D<float> m0, MemoryBuffer2D<float> m1)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_M"].Launch(size, m0.View, m1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        public void _M_sum_M(Index2 size, MemoryBuffer2D<float> m0, MemoryBuffer2D<float> m1, MemoryBuffer2D<float> m2)
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_M"].Launch(size, m0.View, m1.View, m2.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void _V_sum_V(Index size, MemoryBuffer<float> v0, MemoryBuffer<float> v1)
        {
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        public void _V_sum_V(Index size, MemoryBuffer<float> v0, MemoryBuffer<float> v1, MemoryBuffer<float> v2)
        {
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, v0.View, v1.View, v2.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public MemoryBuffer2D<float> _V_X_M_column_M(Index2 size, MemoryBuffer2D<float> m, MemoryBuffer<float> v)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<float>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_M_column_M"].Launch(size, output.View, m.View, v.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public MemoryBuffer2D<float> _V_X_M_line_M(Index2 size, MemoryBuffer2D<float> m, MemoryBuffer<float> v)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<float>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_M_line_M"].Launch(size, output.View, m.View, v.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public MemoryBuffer<float> _C_X_V(Index size, float c, MemoryBuffer<float> v)
        {
            var output = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<float>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_X_V"].Launch(size, output.View, v.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
    }
}
