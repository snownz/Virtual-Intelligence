using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public class SimpleDropOutLayer : SimpleLayer
	{
		public FloatArray DropOutProbability { get; set; }
	}
}