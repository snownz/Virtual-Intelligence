using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNConv2DOperations : ISupervisedOperations
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

        public void UpdateParams()
        {
            throw new System.NotImplementedException();
        }
        
        public void SetLayer(ILayer layer)
        {
            throw new System.NotImplementedException();
        }

        public void SetActivation(IActivationFunction act)
        {
            throw new System.NotImplementedException();
        }

        public void SetError(IErrorFunction err)
        {
            throw new System.NotImplementedException();
        }

        public void SetOptimizer(IOptimizerFunction opt)
        {
            throw new System.NotImplementedException();
        }

        public void ComputeGradient(Array<float> inputs)
        {
            throw new System.NotImplementedException();
        }
    }
}