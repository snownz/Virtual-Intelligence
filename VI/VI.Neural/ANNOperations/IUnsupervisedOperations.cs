using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface IUnsupervisedOperations
    {
        void FeedForward(Array<float> feed);
        void ErrorGradient(Array<float> values);
        void UpdateParams();
        void SetLayer(ILayer layer);
    }
}