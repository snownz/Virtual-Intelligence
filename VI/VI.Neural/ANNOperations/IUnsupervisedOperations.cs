using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
	public interface IUnsupervisedOperations
	{
		void FeedForward(FloatArray feed);
		void ErrorGradient(FloatArray values);
		void UpdateParams();
		void SetLayer(ILayer layer);
	}
}