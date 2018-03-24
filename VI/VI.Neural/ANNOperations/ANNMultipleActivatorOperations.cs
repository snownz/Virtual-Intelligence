using System.Threading.Tasks;
using VI.Neural.ActivationFunction;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
	public class ANNMultipleActivatorOperations : ISupervisedMultipleOperations
    {
        protected IActivationFunction _activationFunction;
        protected IOptimizerMultipleLayerFunction _optimizerFunction;
		protected IMultipleLayer _target;

        public void Summarization(Array<FloatArray> feed)
        {
            Parallel.For(0, _target.ConectionsSize.Length, i => _target.SumVector[i] = (feed[i].T * _target.KnowlodgeMatrix[i]).SumLine());
            _target.Sum = _target.SumVector.Sum() + _target.BiasVector;
        }

        public virtual void Activate()
        {
            _target.OutputVector = _activationFunction.Activate(_target.Sum);
        }
        
        public virtual Array<FloatArray> BackWard(FloatArray backprop)
        {
            var DO = _activationFunction.Derivate(_target.Sum, _target.OutputVector);
            _target.ErrorVector =  DO * backprop;
            var back = new Array<FloatArray>(_target.ConectionsSize.Length);
            Parallel.For(0, _target.ConectionsSize.Length, i => back[i] = (_target.ErrorVector * _target.KnowlodgeMatrix[i]).SumColumn());
            return back;
        }

        public virtual Array<FloatArray> ComputeErrorNBackWard(FloatArray values)
        {
            var DE = _optimizerFunction.Error(_target.OutputVector, values);
            var DO = _activationFunction.Derivate(_target.Sum, _target.OutputVector);
            _target.ErrorVector = DO * DE;
            var back = new Array<FloatArray>(_target.ConectionsSize.Length);
            Parallel.For(0, _target.ConectionsSize.Length, i => back[i] = (_target.ErrorVector * _target.KnowlodgeMatrix[i]).SumColumn());
            return back;
        }

        public virtual void ComputeGradient(Array<FloatArray> inputs)
		{
            Parallel.For(0, _target.ConectionsSize.Length, i =>  _target.GradientMatrix[i] = inputs[i].T * _target.ErrorVector);        
        }

		public virtual void UpdateParams(Array<FloatArray2D> dW, FloatArray dB)
		{
			_optimizerFunction.UpdateWeight(_target, dW);
		}

		public virtual void SetLayer(IMultipleLayer layer)
		{
			_target = layer;
			_optimizerFunction.CalculateParams(_target);
		}

		public void SetActivation(IActivationFunction act)
		{
			_activationFunction = act;
		}        

		public virtual void SetOptimizer(IOptimizerMultipleLayerFunction opt)
		{
			_optimizerFunction = opt;
		}       
    }
}