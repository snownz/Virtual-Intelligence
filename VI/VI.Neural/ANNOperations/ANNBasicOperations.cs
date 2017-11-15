using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.LossFunction;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public sealed class AnnBasicOperations : ISupervisedOperations
    {
        private readonly IActivationFunction _activationFunction;
        private readonly IErrorFunction _errorFunction;
        private readonly IOptimizerFunction _optimizerFunction;
        private ILayer _target;

        public AnnBasicOperations(IActivationFunction activationFunction,
            IErrorFunction errorFunction, IOptimizerFunction optimizerFunction)
        {
            _activationFunction = activationFunction;
            _errorFunction = errorFunction;
            _optimizerFunction = optimizerFunction;            
        }

        public void FeedForward(Array<float> feed)
        {
            _target.SumVector = NumMath.SumColumn(feed.H * _target.KnowlodgeMatrix) + _target.BiasVector;
            _target.OutputVector = _activationFunction.Activate(_target.SumVector);
        }

        public void BackWard(Array<float> values)
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

        public void UpdateWeight()
        {
            _optimizerFunction.UpdateWeight(_target);
        }

        public void UpdateBias()
        {
            _optimizerFunction.UpdateBias(_target);
        }

        public void SetLayer(ILayer layer)
        {
            _target = layer;
            _optimizerFunction.CalculateParams(_target);
        }
    }
}
