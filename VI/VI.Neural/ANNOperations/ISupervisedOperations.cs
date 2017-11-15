using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface ISupervisedOperations
    {
        void FeedForward(Array<float> feed);
        void BackWard(Array<float> values);
        void ErrorGradient(Array<float> inputs);
        void UpdateWeight();
        void UpdateBias();
        void SetLayer(ILayer layer);
    }
}