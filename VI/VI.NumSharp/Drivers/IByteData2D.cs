using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IByteData2D
	{
		object View { get; }
		byte this[int x, int y] { get; set; }
		IEnumerable<int> AxesX { get; }
		IEnumerable<int> AxesY { get; }
		byte[,]         AsArray();
		int              W { get; }
		int              H { get; }
		byte[,]         Clone();
	}
}