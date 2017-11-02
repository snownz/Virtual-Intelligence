using VI.Cognitive.Layer;
using VI.NumSharp.Array;
using VI.NumSharp.Provider;

namespace VI.Cognitive.ANNOperations
{
    public sealed class ANNBasicOperations
    {
        private readonly IActivationFunctionProvider _activationProvider;
        private readonly ILossFunctionProvider _lossProvider;

        public ANNBasicOperations(IActivationFunctionProvider activation
                                  , ILossFunctionProvider loss)
        {
            _activationProvider = activation;
            _lossProvider = loss;
        }

        public void FeedForward(ActivationLayer2 target, Array<float> feed)
        {
            target.SumVector = (feed.H * target.KnowlodgeMatrix).SumColumn() + target.BiasVector;
            _activationProvider.Activation(target.SumVector, target.OutputVector);
        }

        public void BackWardDesired(ActivationLayer2 target, Array<float> desired)
        {
            var de_dOut = _lossProvider.Error(target.OutputVector, desired);
            var dOut_dSum = _activationProvider.Derivated(target.SumVector);
            target.ErrorVector = de_dOut * dOut_dSum;
        }

        public void BackWard(ActivationLayer2 target, Array<float> de_dOut)
        {
            var dOut_dSum = _activationProvider.Derivated(target.SumVector);
            target.ErrorVector = de_dOut * dOut_dSum;
        }

        public void BackWardError(ActivationLayer2 target, Array<float> error)
        {
            var cachedError = error.W * target.KnowlodgeMatrix;
            target.ErrorWeightVector = cachedError.SumLine();
        }

        public void ErrorGradient(ActivationLayer2 target, Array<float> error, Array<float> inputs)
        {
            target.GradientMatrix = (inputs.H * error) * target.LearningRate;
        }

        public void UpdateWeight(ActivationLayer2 target)
        {
            target.KnowlodgeMatrix += target.GradientMatrix;
        }
        public void UpdateWeight(ActivationLayer2 target, Array2D<float> u)
        {
            target.KnowlodgeMatrix += (target.GradientMatrix + u);
        }

        public void UpdateBias(ActivationLayer2 target)
        {
            var biasAjust = target.ErrorVector * target.CachedLearningRate;
            target.BiasVector += biasAjust;
        }
        public void UpdateBias(ActivationLayer2 target, Array<float> u)
        {
            var biasAjust = target.ErrorVector * target.CachedLearningRate;
            target.BiasVector += (biasAjust + u);
        }
    }
}
