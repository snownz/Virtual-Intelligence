using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface IUnsupervisedOperations
    {
        void FeedForward(Array<float> feed);
        void BackWard(Array<float> values);
        void UpdateWeight();
    }
}