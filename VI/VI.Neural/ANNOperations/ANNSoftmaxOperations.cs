using System;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNSoftmaxOperations : ANNDenseOperations
    {
        private readonly IErrorFunction _errorFunction;
        private ILayer _target;

        public override void FeedForward(Array<float> feed)
        {
            _target.SumVector = NumMath.SumColumn(feed.H * _target.KnowlodgeMatrix) + _target.BiasVector;
            var exp = NumMath.Exp(_target.SumVector);
            _target.OutputVector = exp / NumMath.Sum(exp);
        }

        public override void BackWard(Array<float> values)
        {
            var DE = _errorFunction.Error(_target.OutputVector, values);
            _target.ErrorVector = -1 * DE;
            _target.ErrorWeightVector = NumMath.SumLine(_target.ErrorVector.W * _target.KnowlodgeMatrix);
        }
    }
}