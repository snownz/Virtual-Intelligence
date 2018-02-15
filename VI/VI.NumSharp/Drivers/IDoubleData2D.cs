using System.Collections.Generic;
using ILGPU;

namespace VI.NumSharp.Drivers
{
	public interface IDoubleData2D
	{
		ArrayView2D<double> View { get; }
		double this[int x, int y] { get; set; }
		IEnumerable<int> AxesX { get; }
		IEnumerable<int> AxesY { get; }
		int W { get; }
		int H { get; }
		double[,] AsArray2D();
		double[] AsArray();
		double[,] Clone();
	}
}