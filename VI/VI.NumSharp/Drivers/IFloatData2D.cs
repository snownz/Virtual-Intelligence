﻿using ILGPU;
using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IFloatData2D
	{
        ArrayView2D<float> View { get; }
		float this[int x, int y] { get; set; }
		IEnumerable<int> AxesX { get; }
		IEnumerable<int> AxesY { get; }
		float[,] AsArray2D();
		float[] AsArray();
		int     W { get; }
		int     H { get; }
		float[,] Clone();
	}
}