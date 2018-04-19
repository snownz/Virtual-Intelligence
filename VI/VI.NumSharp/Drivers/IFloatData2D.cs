using System.Collections.Generic;
using ILGPU;

namespace VI.NumSharp.Drivers
{
	public interface IFloatData2D
	{
		ArrayView2D<float> View { get; }
		float this[int x, int y] { get; set; }
		IEnumerable<int> AxesX { get; }
		IEnumerable<int> AxesY { get; }
		int W { get; }
		int H { get; }
		float[,] AsArray2D();
		float[] AsArray();
		float[,] Clone();
	}
}