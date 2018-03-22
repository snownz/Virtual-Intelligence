using VI.Neural.ActivationFunction;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
	public class ANNActivatorOperations : ISupervisedOperations
	{
        protected IActivationFunction _activationFunction;
        protected IOptimizerFunction  _optimizerFunction;
		protected ILayer              _target;

		public virtual void FeedForward(FloatArray feed)
		{
			_target.SumVector    = (feed.T * _target.KnowlodgeMatrix).SumLine() + _target.BiasVector;
			_target.OutputVector = _activationFunction.Activate(_target.SumVector);
		}

        public virtual FloatArray BackWard(FloatArray backprop)
        {
            var DO = _activationFunction.Derivate(_target.SumVector);
            _target.ErrorVector =  DO * backprop;
            return (_target.ErrorVector * _target.KnowlodgeMatrix).SumColumn();
        }

        public virtual FloatArray ComputeErrorNBackWard(FloatArray values)
        {
            var DE = _optimizerFunction.Error(_target.OutputVector, values);
            var DO = _activationFunction.Derivate(_target.SumVector);
            _target.ErrorVector = DO * DE ;
            return (_target.ErrorVector * _target.KnowlodgeMatrix).SumColumn();
        }

        public void ComputeGradient(FloatArray inputs)
		{
			_target.GradientMatrix = inputs.T * _target.ErrorVector;
        }

		public void UpdateParams(FloatArray2D dW, FloatArray dB)
		{
			_optimizerFunction.UpdateWeight(_target, dW);
			_optimizerFunction.UpdateBias(_target, dB);
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

		public void SetOptimizer(IOptimizerFunction opt)
		{
			_optimizerFunction = opt;
		}
    }
}