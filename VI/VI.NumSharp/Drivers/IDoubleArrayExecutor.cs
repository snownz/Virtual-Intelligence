namespace VI.NumSharp.Drivers
{
	public interface IDoubleArrayExecutor
	{
		IDoubleData V_mult_V(IDoubleData cache, IDoubleData v0, IDoubleData v1);
		IDoubleData V_div_V(IDoubleData cache, IDoubleData v0, IDoubleData v1);
		IDoubleData V_sub_V(IDoubleData cache, IDoubleData v0, IDoubleData v1);
		IDoubleData V_add_V(IDoubleData cache, IDoubleData v0, IDoubleData v1);
		IDoubleData V_add_C(IDoubleData cache, IDoubleData v, double c);
		IDoubleData V_mult_C(IDoubleData cache, IDoubleData v, double c);
		IDoubleData V_sub_C(IDoubleData cache, IDoubleData v, double c);
		IDoubleData V_sub_C(IDoubleData cache, double c, IDoubleData v);
		IDoubleData V_div_C(IDoubleData cache, IDoubleData v, double c);
		IDoubleData V_div_C(IDoubleData cache, double c, IDoubleData v);

		IDoubleData2D VT_mult_M(IDoubleData2D cache, IDoubleData vt, IDoubleData2D m);
		IDoubleData2D M_mult_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D M_div_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D M_sub_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D M_add_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D M_mult_VT(IDoubleData2D cache, IDoubleData2D m, IDoubleData v);
		IDoubleData2D M_mult_V(IDoubleData2D cache, IDoubleData2D m, IDoubleData v);
		IDoubleData2D V_mult_M(IDoubleData2D cache, IDoubleData v, IDoubleData2D m);
		IDoubleData2D M_mult_C(IDoubleData2D cache, IDoubleData2D m, double c);
		IDoubleData2D M_add_C(IDoubleData2D cache, IDoubleData2D m, double c);
		IDoubleData2D M_div_C(IDoubleData2D cache, IDoubleData2D m, double c);
		IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, double c);
		IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, int c);

		IDoubleData Tanh(IDoubleData cache, IDoubleData arr);
		IDoubleData Sin(IDoubleData cache, IDoubleData arr);
		IDoubleData Cos(IDoubleData cache, IDoubleData arr);
		IDoubleData Pow(IDoubleData cache, IDoubleData arr, double exp);
		IDoubleData Exp(IDoubleData cache, IDoubleData arr);
		IDoubleData Log(IDoubleData cache, IDoubleData arr);
		IDoubleData Sqrt(IDoubleData cache, IDoubleData arr);
		IDoubleData2D Sqrt(IDoubleData2D cache, IDoubleData2D arr);

		IDoubleData2D M_mult_MT(IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D MT_mult_M(IDoubleData2D m0, IDoubleData2D m1);
		IDoubleData2D VT_mult_V(IDoubleData vt, IDoubleData v);
		IDoubleData2D V_mult_VT(IDoubleData v, IDoubleData vt);
		IDoubleData ApplyMask(IDoubleData arr, IByteData mask);
		IDoubleData2D ApplyMask(IDoubleData2D arr, IByteData2D mask);
		IDoubleData SumLine(IDoubleData2D arr);
		IDoubleData SumColumn(IDoubleData2D arr);
	}
}