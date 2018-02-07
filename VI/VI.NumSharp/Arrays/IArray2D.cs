using System.Collections.Generic;

namespace VI.NumSharp.Arrays
{
	public interface IArray2D
	{
		int              W     { get; }
		int              H     { get; }
		FloatArray2DT    T     { get; }
	}
}