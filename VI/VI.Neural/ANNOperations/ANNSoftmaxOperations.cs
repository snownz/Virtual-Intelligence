using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNSoftmaxOperations : ANNDenseOperations
    {
        public override void FeedForward(Array<float> feed)
        {
            _target.SumVector = (feed.T * _target.KnowlodgeMatrix).SumColumn() + _target.BiasVector;
            var exp = (_target.SumVector).Exp();
            var sum = (exp).Sum();
            _target.OutputVector = exp / sum;
        }

        public override void BackWard(Array<float> values)
        {
            var DE = _errorFunction.Error(_target.OutputVector, values);
            _target.ErrorVector = DE;
            _target.ErrorWeightVector = (_target.ErrorVector * _target.KnowlodgeMatrix).SumLine();
        }
    }
}