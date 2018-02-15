using System.Collections.Generic;
using ILGPU;

namespace VI.NumSharp.Drivers
{
	public interface IDoubleData
	{
		ArrayView<double> View { get; }
		double this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		int Length { get; }
		double[] AsArray();
		double[] Clone();
	}
}