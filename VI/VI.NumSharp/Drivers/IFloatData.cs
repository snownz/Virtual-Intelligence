using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IFloatData
	{
		object View { get; }
		float this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		float[] AsArray();
		int Length { get; }
		float[] Clone();
	}
}