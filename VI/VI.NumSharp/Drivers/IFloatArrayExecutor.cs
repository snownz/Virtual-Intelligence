namespace VI.NumSharp.Drivers
{
    public interface IFloatArrayExecutor
    {
        IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1);

        IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1);

        IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1);

        IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1);

        IFloatData V_add_C(IFloatData cache, IFloatData v, float c);

        IFloatData V_mult_C(IFloatData cache, IFloatData v, float c);

        IFloatData V_sub_C(IFloatData cache, IFloatData v, float c);

        IFloatData V_sub_C(IFloatData cache, float c, IFloatData v);

        IFloatData V_div_C(IFloatData cache, IFloatData v, float c);

        IFloatData V_div_C(IFloatData cache, float c, IFloatData v);

        IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m);

        IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1);

        IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1);

        IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1);

        IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1);

        IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v);

        IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c);

        IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c);

        IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c);

        IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c);

        IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c);

        IFloatData Tanh(IFloatData cache, IFloatData arr);

        IFloatData Sin(IFloatData cache, IFloatData arr);

        IFloatData Cos(IFloatData cache, IFloatData arr);

        IFloatData Pow(IFloatData cache, IFloatData arr, float exp);

        IFloatData Exp(IFloatData cache, IFloatData arr);

        IFloatData Log(IFloatData cache, IFloatData arr);

        IFloatData Sqrt(IFloatData cache, IFloatData arr);

        IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr);

        IFloatData2D M_mult_MT(IFloatData2D m0, IFloatData2D m1);

        IFloatData2D VT_mult_V(IFloatData vt, IFloatData v);

        IFloatData SumLine(IFloatData2D arr);

        IFloatData SumColumn(IFloatData2D arr);
    }
}