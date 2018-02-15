using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IByteData2D
	{
		object View { get; }
		byte this[int x, int y] { get; set; }
		IEnumerable<int> AxesX { get; }
		IEnumerable<int> AxesY { get; }
		int W { get; }
		int H { get; }
		byte[,] AsArray();
		byte[,] Clone();
	}
}