using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public class SimpleWithMasks : SimpleLayer
	{
		public ByteArray BiasMask { get; set; }
		public ByteArray2D ConnectionMask { get; set; }
	}
}