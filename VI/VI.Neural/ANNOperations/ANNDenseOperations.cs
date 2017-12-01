using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNDenseOperations : ISupervisedOperations
    {
        protected ILayer _target;
        
        private IActivationFunction _activationFunction;
        protected IErrorFunction _errorFunction;
        private IOptimizerFunction _optimizerFunction;
     
        public virtual void FeedForward(Array<float> feed)
        {
            _target.SumVector = NumMath.SumColumn(feed.H * _target.KnowlodgeMatrix) + _target.BiasVector;
            _target.OutputVector = _activationFunction.Activate(_target.SumVector);
        }

        public virtual void BackWard(Array<float> values)
        {
            var DE = _errorFunction.Error(_target.OutputVector, values);
            var DO = _activationFunction.Derivate(_target.SumVector);
            _target.ErrorVector = DE * DO;
            _target.ErrorWeightVector = NumMath.SumLine(_target.ErrorVector.W * _target.KnowlodgeMatrix);
        }

        public void ErrorGradient(Array<float> inputs)
        {
            _target.GradientMatrix = (inputs.H * _target.ErrorVector);
        }

        public void UpdateParams()
        {
            _optimizerFunction.UpdateWeight(_target);
            _optimizerFunction.UpdateBias(_target);
        }
        
        public void SetLayer(ILayer layer)
        {
            _target = layer;
            _optimizerFunction.CalculateParams(_target);
        }

        public void SetActivation(IActivationFunction act)
        {
            _activationFunction = act;
        }

        public void SetError(IErrorFunction err)
        {
            _errorFunction = err;
        }

        public void SetOptimizer(IOptimizerFunction opt)
        {
            _optimizerFunction = opt;
        }
    }
}
