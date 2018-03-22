using System;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNRecurrentOperations : ANNActivatorOperations
	{
        private FloatArray2D WT;

		public override void FeedForward(FloatArray feed)
		{
            (var input, var hidden) = feed.Take(_target.ConectionsSize);

			_target.SumVector    = (input.T * _target.KnowlodgeMatrix).SumLine() + (hidden.T * WT).SumLine() + _target.BiasVector;
			_target.OutputVector = _activationFunction.Activate(_target.SumVector);
		}

        public override FloatArray BackWard(FloatArray backprop)
        {
            throw new NotImplementedException();
        }

        public override FloatArray ComputeErrorNBackWard(FloatArray values)
        {
            throw new NotImplementedException();
        }
    }
}