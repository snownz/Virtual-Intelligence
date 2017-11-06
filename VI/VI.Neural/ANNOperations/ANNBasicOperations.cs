using VI.Neural.Layer;
using VI.NumSharp.Array;
using VI.NumSharp.Provider;

namespace VI.Neural.ANNOperations
{
    public sealed class AnnBasicOperations
    {
        private readonly IActivationFunctionProvider _activationProvider;
        private readonly ILossFunctionProvider _lossProvider;

        public AnnBasicOperations(IActivationFunctionProvider activation, ILossFunctionProvider loss)
        {
            _activationProvider = activation;
            _lossProvider = loss;
        }

        public void FeedForward(ILayer target, Array<float> feed)
        {
            target.SumVector = (feed.H * target.KnowlodgeMatrix).SumColumn() + target.BiasVector;
            _activationProvider.Activation(target.SumVector, target.OutputVector);
        }

        public void BackWardDesired(ILayer target, Array<float> desired)
        {
            var de_dOut = _lossProvider.Error(target.OutputVector, desired);
            var dOut_dSum = _activationProvider.Derivated(target.SumVector);
            target.ErrorVector = de_dOut * dOut_dSum;
        }

        public void BackWard(ILayer target, Array<float> de_dOut)
        {
            var dOut_dSum = _activationProvider.Derivated(target.SumVector);
            target.ErrorVector = de_dOut * dOut_dSum;
        }

        public void BackWardError(ILayer target)
        {
            target.ErrorWeightVector = (target.ErrorVector.W * target.KnowlodgeMatrix).SumLine();
        }

        public void ErrorGradient(ILayer target, Array<float> inputs)
        {
            target.GradientMatrix = (inputs.H * target.ErrorVector) * target.LearningRate;
        }

        public void UpdateWeight(ILayer target)
        {
            target.KnowlodgeMatrix += target.GradientMatrix;
        }
        public void UpdateWeight(ILayer target, Array2D<float> u)
        {
            target.KnowlodgeMatrix += (target.GradientMatrix + u);
        }

        public void UpdateBias(ILayer target)
        {
            var biasAjust = target.ErrorVector * target.CachedLearningRate;
            target.BiasVector += biasAjust;
        }
        public void UpdateBias(ILayer target, Array<float> u)
        {
            var biasAjust = target.ErrorVector * target.CachedLearningRate;
            target.BiasVector += (biasAjust + u);
        }
    }
}
