using System.Collections.Generic;

namespace VI.NumSharp.Drivers
{
	public interface IByteData
	{
		object View { get; }
		byte this[int x] { get; set; }
		IEnumerable<int> AxesX { get; }
		int Length { get; }
		byte[] AsArray();
		byte[] Clone();
	}
}