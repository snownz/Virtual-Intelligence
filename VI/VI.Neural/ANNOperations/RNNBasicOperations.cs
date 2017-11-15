
using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public sealed class RnnBasicOperations : ISupervisedOperations
    {
        public void FeedForward(Array<float> feed)
        {
            throw new System.NotImplementedException();
        }

        public void BackWard(Array<float> values)
        {
            throw new System.NotImplementedException();
        }

        public void ErrorGradient(Array<float> inputs)
        {
            throw new System.NotImplementedException();
        }

        public float Loss(Array<float> desired)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateWeight()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBias()
        {
            throw new System.NotImplementedException();
        }

        public void SetLayer(ILayer layer)
        {
            throw new System.NotImplementedException();
        }
    }
}
