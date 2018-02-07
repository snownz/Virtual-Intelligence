using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IByteData
	{
		byte this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		byte[]          AsArray();
		int              Length { get; }
		byte[]          Clone();
	}
}