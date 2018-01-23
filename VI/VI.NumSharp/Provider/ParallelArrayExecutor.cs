using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using VI.NumSharp.Arrays;

namespace VI.NumSharp.Provider
{
    public class ParallelArrayExecutor : IArrayExecutor
    {
        public Array<T> V_sub_V<T>(Array<T> v0, Array<T> v1) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sub_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> V_add_V<T>(Array<T> v0, Array<T> v1) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sum_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> V_div_V<T>(Array<T> v0, Array<T> v1) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_div_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> V_mult_V<T>(Array<T> v0, Array<T> v1) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array<T> V_mult_C<T>(Array<T> v0, T c) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_X_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> C_mult_V<T>(T c, Array<T> v0) where T : struct
        {
            return V_mult_C(v0, c);
        }

        public Array<T> V_div_C<T>(Array<T> v0, T c) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_div_C"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> C_div_V<T>(T c, Array<T> v0) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_div_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array<T> V_add_C<T>(Array<T> v0, T c) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sum_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> C_add_V<T>(T c, Array<T> v0) where T : struct
        {
            return V_add_C(v0, c);
        }

        public Array<T> V_sub_C<T>(Array<T> v0, T c) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_sub_C"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> C_sub_V<T>(T c, Array<T> v0) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_C_sub_V"].Launch(size, c, output.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<T> V_mult_M<T>(Array<T> v0, Array2D<T> m0) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_line_M"]
                .Launch(size, output.View, m0.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array2D<T> M_mult_V<T>(Array2D<T> m0, Array<T> v0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> V_div_M<T>(Array<T> v0, Array2D<T> m01) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> M_div_V<T>(Array2D<T> m0, Array<T> v01) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> V_add_M<T>(Array<T> v0, Array2D<T> m0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> M_add_V<T>(Array2D<T> m0, Array<T> v0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> V_sub_M<T>(Array<T> v0, Array2D<T> m0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> M_sub_V<T>(Array2D<T> m0, Array<T> v0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> M_mult_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct
        {
            var size = m0.Size;
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_X_M"]
                .Launch(size, output.View, m0.View, m1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<T> M_div_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_div_M"]
                .Launch(size, output.View, m0.View, m1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<T> M_add_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_sum_M"]
                .Launch(size, m0.View, m1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return m0;
        }

        public Array2D<T> M_mult_C<T>(Array2D<T> m0, T c) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_C_X_M"]
                .Launch(size, output.View, m0.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<T> M_add_C<T>(Array2D<T> m0, T c) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_sum_C"]
                .Launch(size, output.View, m0.View, c);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<byte> M_less_equal_C<T>(Array2D<T> m0, T c) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<byte>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_C_Less_Equal"].Launch(size, output.View, c, m0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array<T> V_equal_C<T>(Array<T> v0, T c) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array<T> V_dif_C<T>(Array<T> v0, T c) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array<T> V_greater_C<T>(Array<T> v0, T c) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array<T> V_less_C<T>(Array<T> v0, T c) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array<T> V_greater_equal_C<T>(Array<T> v0, T c) where T : struct
        {
            var size = v0.View.Length;
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_V_C_More_Equal"].Launch(size, output.View, c, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array<T> V_less_equal_C<T>(Array<T> v0, T c) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> As2DView<T>(Array<T> arr, int w, int h) where T : struct
        {
            var size = new Index2(w, h);
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_1D_to_2D"].Launch(size, output.View, arr.View, w);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array3D<T> As3DView<T>(Array<T> arr, int w, int h, int z) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> Transpose_V_mult_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct
        {
            var size = new Index2(v0.View.Length, v1.View.Length);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_V_M"]
                .Launch(size, output.View, v0.View, v1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array2D<T> V_multi_Transpose_V<T>(Array<T> v0, ArrayT<T> v1) where T : struct
        {
            var size = new Index2(v1.View.Length, v0.View.Length);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_V_M"]
                .Launch(size, output.View, v1.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }

        public Array2D<T> Transpose_V_div_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> Transpose_V_add_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> Transpose_V_sub_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array2D<T> Transpose_V_multi_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct
        {
            var size = new Index2(m0.View.Width, m0.View.Height);
            var output = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_X_M_column_M"]
                .Launch(size, output.View, m0.View, v0.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        public Array2D<T> Transpose_V_div_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> Transpose_V_add_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }
        public Array2D<T> Transpose_V_sub_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct
        {
            throw new NotImplementedException("TalT to the owner of the repository to implement this method (Issue)");
        }

        public Array<T> Pow<T>(Array<T> arr, int p) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Pow"]
                .Launch(size, mem.View, arr.View, p);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Sqrt<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Sqrt"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Exp<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Exp"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Tan<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Tan"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Tanh<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Tanh"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Log<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Log"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Sin<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Sin"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }
        public Array<T> Cos<T>(Array<T> arr) where T : struct
        {
            var size = new Index(arr.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Cos"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public Array2D<T> Pow<T>(Array2D<T> arr, int p)
           where T : struct
        {
            var size = new Index2(arr.View.Length);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Pow"]
                .Launch(size, arr.View, p);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return arr;
        }

        public Array2D<T> ApplyMask<T>(Array2D<T> arr, Array2D<byte> mask)
            where T : struct
        {
            var size = new Index2(arr.View.Length);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_mask"]
                .Launch(size, arr.View, mask.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return arr;
        }

        public Array2D<T> Sqrt<T>(Array2D<T> arr)
            where T : struct
        {
            var size = new Index2(arr.View.Width, arr.View.Height);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_Sqrt"]
                .Launch(size, mem.View, arr.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public Array<T> SumColumn<T>(Array2D<T> arr)
            where T : struct
        {
            var s = arr.View.Height / 2;
            var r = arr.View.Height % 2;
            while (s > 1)
            {
                var _size = new Index2(arr.View.Width, s);

                _sumLines(_size, r, arr.Memory);

                r = s % 2;
                s /= 2;
            }
            return _joinLines(arr.View.Width, arr.Memory);
        }
        public Array<T> SumLine<T>(Array2D<T> arr)
            where T : struct
        {
            if (arr.View.Width <= 1)
                return _columns(arr.View.Height, arr.Memory);

            var s = arr.View.Width / 2;
            var r = arr.View.Width % 2;
            while (s > 1)
            {
                var _size = new Index2(s, arr.View.Height);

                _sumColumns(_size, r, arr.Memory);

                r = s % 2;
                s /= 2;
            }
            return _joinColumns(arr.View.Height, arr.Memory);
        }
        
        public Array<float> Euclidian(int size, int position)
        {
            var mem = Allocate<float>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_euclidian_distance"]
                .Launch(size, mem.View, position);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public Array2D<float> Euclidian(int w, int h, int x, int y)
        {
            var size = new Index2(w, h);
            var position = new Index2(x, y);
            var mem = Allocate<float>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_euclidian_distance"]
                .Launch(size, mem.View, position);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public Array<T> Max<T>(Array<T> arr0, Array<T> arr1)
           where T : struct
        {
            var size = new Index(arr0.View.Length);
            var mem = Allocate<T>(size);
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_Max"]
                .Launch(size, mem.View, arr0.View, arr1.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public void Normalize<T>(T p0, T p1, Array2D<T> m)
            where T : struct
        {
            ProcessingDevice
                .ArrayDevice
                .Executor["_M_normalize"]
                .Launch(m.Size, m.View, p0, p1);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public void Normalize<T>(T p0, T p1, Array<T> v)
            where T : struct
        {
            ProcessingDevice
                .ArrayDevice
                .Executor["_V_normalize"]
                .Launch(v.Length, v.View, p0, p1);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }

        public Array<T> Allocate<T>(Index size)
           where T : struct
        {
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            return new Array<T>(mem);
        }

        public Array2D<T> Allocate<T>(Index2 size)
            where T : struct
        {
            var mem = ProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            return new Array2D<T>(mem);
        }

        private void _sumLines<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T : struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_lines"].Launch(size, r, m.View, size.Y);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private void _sumColumns<T>(Index2 size, int r, MemoryBuffer2D<T> m)
            where T : struct
        {
            ProcessingDevice.ArrayDevice.Executor["_M_sum_columns"].Launch(size, r, m.View, size.X);
            ProcessingDevice.ArrayDevice.Executor.Wait();
        }
        private Array<T> _joinLines<T>(Index size, MemoryBuffer2D<T> m)
            where T : struct
        {
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_lines_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private Array<T> _joinColumns<T>(Index size, MemoryBuffer2D<T> m)
            where T : struct
        {
            var output = Allocate<T>(size);
            ProcessingDevice.ArrayDevice.Executor["_M_2_columns_V"].Launch(size, output.View, m.View);
            ProcessingDevice.ArrayDevice.Executor.Wait();
            return output;
        }
        private Array<T> _columns<T>(Index size, MemoryBuffer2D<T> m)
           where T : struct
        {
            var a = m.GetAsArray();
            return new Array<T>(a);
        }
    }
}
