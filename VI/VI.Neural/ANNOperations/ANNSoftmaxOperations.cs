using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNSoftmaxOperations : ANNDenseOperations
    {
        public override void FeedForward(Array<float> feed)
        {
            _target.SumVector = NumMath.SumColumn(feed.T * _target.KnowlodgeMatrix) + _target.BiasVector;
            var exp = NumMath.Exp(_target.SumVector);
            var sum = NumMath.Sum(exp);
            _target.OutputVector = exp / sum;
        }

        public override void BackWard(Array<float> values)
        {
            var DE = _errorFunction.Error(_target.OutputVector, values);
            _target.ErrorVector = DE;
            _target.ErrorWeightVector = NumMath.SumLine(_target.ErrorVector * _target.KnowlodgeMatrix);
        }
    }
}