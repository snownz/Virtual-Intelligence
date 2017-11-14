using VI.Neural.Layer;
using VI.Neural.Provider;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public sealed class AnnBasicOperations
    {
        private readonly IActivationFunctionProvider _activationProvider;
        private readonly ILossFunctionProvider _lossProvider;
        private readonly IBackwardProvider _backwardProvider;
        private readonly IOptimizerParamsProvider _optimizerParamsProvider;

        private readonly ILayer _target;
        
        public AnnBasicOperations(IActivationFunctionProvider activation, ILossFunctionProvider loss)
        {
            _activationProvider = activation;
            _lossProvider = loss;
            _optimizerParamsProvider.CalculateParams();
        }

        public void FeedForward(Array<float> feed)
        {
            _target.SumVector = (feed.H * _target.KnowlodgeMatrix).SumColumn() + _target.BiasVector;
            _activationProvider.Activation(_target.SumVector, _target.OutputVector);
        }

        public void BackWard(Array<float> values)
        {
            var DE = _backwardProvider.Error(_target.OutputVector, values);
            var DO = _activationProvider.Derivated(_target.SumVector);
            _target.ErrorVector = DE * DO;
            _target.ErrorWeightVector = (_target.ErrorVector.W * _target.KnowlodgeMatrix).SumLine();
        }
       
        public void ErrorGradient(Array<float> inputs)
        {
            _target.GradientMatrix = (inputs.H * _target.ErrorVector);
        }

        public float Loss(Array<float> desired)
        {
            return _lossProvider.Loss(desired);
        }
        
        public void UpdateWeight()
        {
            _optimizerParamsProvider.UpdateWeight(_target);
            //target.KnowlodgeMatrix += target.GradientMatrix;
        }
        
        public void UpdateBias()
        {
            _optimizerParamsProvider.UpdateBias(_target);
            //var biasAjust = target.ErrorVector * target.CachedLearningRate;
            //target.BiasVector += biasAjust;
        }
    }
}
using VI.Neural.Layer;
using VI.Neural.Provider;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public sealed class AnnBasicOperations
    {
        private readonly IActivationFunctionProvider _activationProvider;
        private readonly ILossFunctionProvider _lossProvider;
        private readonly IBackwardProvider _backwardProvider;
        private readonly IOptimizerParamsProvider _optimizerParamsProvider;

        private readonly ILayer _target;
        
        public AnnBasicOperations(IActivationFunctionProvider activation, ILossFunctionProvider loss)
        {
            _activationProvider = activation;
            _lossProvider = loss;
            _optimizerParamsProvider.CalculateParams();
        }

        public void FeedForward(Array<float> feed)
        {
            _target.SumVector = (feed.H * _target.KnowlodgeMatrix).SumColumn() + _target.BiasVector;
            _activationProvider.Activation(_target.SumVector, _target.OutputVector);
        }

        public void BackWard(Array<float> values)
        {
            var DE = _backwardProvider.Error(_target.OutputVector, values);
            var DO = _activationProvider.Derivated(_target.SumVector);
            _target.ErrorVector = DE * DO;
            _target.ErrorWeightVector = (_target.ErrorVector.W * _target.KnowlodgeMatrix).SumLine();
        }
       
        public void ErrorGradient(Array<float> inputs)
        {
            _target.GradientMatrix = (inputs.H * _target.ErrorVector);
        }

        public float Loss(Array<float> desired)
        {
            return _lossProvider.Loss(desired);
        }
        
        public void UpdateWeight()
        {
            _optimizerParamsProvider.UpdateWeight(_target);
            //target.KnowlodgeMatrix += target.GradientMatrix;
        }
        
        public void UpdateBias()
        {
            _optimizerParamsProvider.UpdateBias(_target);
            //var biasAjust = target.ErrorVector * target.CachedLearningRate;
            //target.BiasVector += biasAjust;
        }
    }
}
