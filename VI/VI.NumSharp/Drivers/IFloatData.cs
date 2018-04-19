using System.Collections.Generic;
using ILGPU;

namespace VI.NumSharp.Drivers
{
	public interface IFloatData
	{
		ArrayView<float> View { get; }
		float this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		int Length { get; }
		float[] AsArray();
		float[] Clone();
	}
}