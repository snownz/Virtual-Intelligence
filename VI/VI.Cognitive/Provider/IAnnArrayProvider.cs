using ILGPU;
using ILGPU.Runtime;

namespace VI.Cognitive.Provider
{
    public interface IAnnArrayProvider
    {
        void _C_X_M(Index2 size, MemoryBuffer2D<float> m, float c);
        void _M_2_lines_V(Index size, MemoryBuffer<float> v, MemoryBuffer2D<float> m);
        void _M_sum_column_V(Index2 size, MemoryBuffer<float> v, MemoryBuffer2D<float> m);
        void _M_sum_lines(Index2 size, int r, MemoryBuffer2D<float> mat);
        void _M_sum_line_V(Index2 size, MemoryBuffer<float> v, MemoryBuffer2D<float> m);
        void _M_sum_M(Index2 size, MemoryBuffer2D<float> m0, MemoryBuffer2D<float> m1);
        void _M_sum_M(Index2 size, MemoryBuffer2D<float> m0, MemoryBuffer2D<float> m1, MemoryBuffer2D<float> m2);
        void _V_sum_V(Index size, MemoryBuffer<float> v0, MemoryBuffer<float> v1);
        void _V_sum_V(Index size, MemoryBuffer<float> v0, MemoryBuffer<float> v1, MemoryBuffer<float> v2);
        void _V_X_V(Index size, MemoryBuffer<float> output, MemoryBuffer<float> v0, MemoryBuffer<float> v1);
        void _V_X_V_M(Index2 size, MemoryBuffer2D<float> output, MemoryBuffer<float> v0, MemoryBuffer<float> v1);
        void _V_zeros(Index size, MemoryBuffer<float> v);
        MemoryBuffer2D<float> _V_X_M_column_M(Index2 size, MemoryBuffer2D<float> m, MemoryBuffer<float> v);
        MemoryBuffer2D<float> _V_X_M_line_M(Index2 size, MemoryBuffer2D<float> m, MemoryBuffer<float> v);
        MemoryBuffer<float> _C_X_V(Index size, float c, MemoryBuffer<float> v);
    }
}