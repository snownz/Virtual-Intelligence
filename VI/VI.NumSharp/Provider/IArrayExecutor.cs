using ILGPU;
using VI.NumSharp.Arrays;

namespace VI.NumSharp.Provider
{
    public interface IArrayExecutor
    {
        Array<T> V_sub_V<T>(Array<T> v0, Array<T> v1) where T : struct;
        Array<T> V_add_V<T>(Array<T> v0, Array<T> v1) where T : struct;
        Array<T> V_div_V<T>(Array<T> v0, Array<T> v1) where T : struct;
        Array<T> V_mult_V<T>(Array<T> v0, Array<T> v1) where T : struct;

        Array<T> V_mult_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> C_mult_V<T>(T c, Array<T> v0) where T : struct;

        Array<T> V_div_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> C_div_V<T>(T c, Array<T> v0) where T : struct;

        Array<T> V_add_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> C_add_V<T>(T c, Array<T> v0) where T : struct;

        Array<T> V_sub_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> C_sub_V<T>(T c, Array<T> v0) where T : struct;

        Array2D<T> V_mult_M<T>(Array<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> M_mult_V<T>(Array2D<T> m0, Array<T> v0) where T : struct;

        Array2D<T> V_div_M<T>(Array<T> v0, Array2D<T> m01) where T : struct;
        Array2D<T> M_div_V<T>(Array2D<T> m0, Array<T> v01) where T : struct;

        Array2D<T> V_add_M<T>(Array<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> M_add_V<T>(Array2D<T> m0, Array<T> v0) where T : struct;

        Array2D<T> V_sub_M<T>(Array<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> M_sub_V<T>(Array2D<T> m0, Array<T> v0) where T : struct;

        Array2D<T> M_mult_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct;
        Array2D<T> M_div_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct;
        Array2D<T> M_add_M<T>(Array2D<T> m0, Array2D<T> m1) where T : struct;
        Array2D<T> M_mult_C<T>(Array2D<T> m0, T c) where T : struct;
        Array2D<T> M_add_C<T>(Array2D<T> m0, T c) where T : struct;
        Array2D<byte> M_less_equal_C<T>(Array2D<T> m0, T c) where T : struct;

        Array<T> V_equal_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> V_dif_C<T>(Array<T> v0, T c) where T : struct;

        Array<T> V_greater_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> V_less_C<T>(Array<T> v0, T c) where T : struct;

        Array<T> V_greater_equal_C<T>(Array<T> v0, T c) where T : struct;
        Array<T> V_less_equal_C<T>(Array<T> v0, T c) where T : struct;

        Array2D<T> As2DView<T>(Array<T> arr, int w, int h) where T : struct;
        Array3D<T> As3DView<T>(Array<T> arr, int w, int h, int z) where T : struct;

        Array2D<T> Transpose_V_mult_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct;
        Array2D<T> V_multi_Transpose_V<T>(Array<T> v0, ArrayT<T> v1) where T : struct;

        Array2D<T> Transpose_V_div_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct;
        Array2D<T> Transpose_V_add_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct;
        Array2D<T> Transpose_V_sub_V<T>(ArrayT<T> v0, Array<T> v1) where T : struct;

        Array2D<T> Transpose_V_multi_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> Transpose_V_div_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> Transpose_V_add_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct;
        Array2D<T> Transpose_V_sub_M<T>(ArrayT<T> v0, Array2D<T> m0) where T : struct;

        Array<T> Pow<T>(Array<T> arr, int p) where T : struct;
        Array<T> Sqrt<T>(Array<T> arr) where T : struct;
        Array<T> Exp<T>(Array<T> arr) where T : struct;
        Array<T> Tan<T>(Array<T> arr) where T : struct;
        Array<T> Tanh<T>(Array<T> arr) where T : struct;
        Array<T> Log<T>(Array<T> arr) where T : struct;
        Array<T> Sin<T>(Array<T> arr) where T : struct;
        Array<T> Cos<T>(Array<T> arr) where T : struct;

        Array2D<T> Pow<T>(Array2D<T> arr, int p) where T : struct;
        Array2D<T> Sqrt<T>(Array2D<T> arr) where T : struct;

        Array2D<T> ApplyMask<T>(Array2D<T> arr, Array2D<byte> mask) where T : struct;

        Array<T> SumColumn<T>(Array2D<T> arr) where T : struct;
        Array<T> SumLine<T>(Array2D<T> arr) where T : struct;

        Array<float> Euclidian(int size, int position);
        Array2D<float> Euclidian(int w, int h, int x, int y);

        Array<T> Max<T>(Array<T> arr0, Array<T> arr1) where T : struct;

        void Normalize<T>(T p0, T p1, Array2D<T> m) where T : struct;
        void Normalize<T>(T p0, T p1, Array<T> v) where T : struct;

        Array<T> Allocate<T>(Index size) where T : struct;
        Array2D<T> Allocate<T>(Index2 size) where T : struct;
    }
}
