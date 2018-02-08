using ILGPU;
using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IFloatData
	{
        ArrayView<float> View { get; }
		float this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		float[] AsArray();
		int Length { get; }
		float[] Clone();
	}
}