using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface ISupervisedOperations
    {
        void FeedForward(Array<float> feed);
        void BackWard(Array<float> values);
        void ErrorGradient(Array<float> inputs);
        void ComputeGradient(Array<float> inputs);
        void UpdateParams();
        void SetLayer(ILayer layer);
        void SetActivation(IActivationFunction act);
        void SetError(IErrorFunction err);
        void SetOptimizer(IOptimizerFunction opt);
    }
}