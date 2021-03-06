﻿using System;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public class ANNSoftmaxOperations : ANNActivatorOperations
    {
        public override void Activate()
        {
            var exp = _target.SumVector.Exp();
            var sum = exp.Sum();
            _target.OutputVector = exp / sum;
        }

        public override FloatArray BackWard(FloatArray values)
        {
            throw new Exception("This layer haven't a support for this feature! Just [ComputeErrorNBackWard]");
        }

        //TODO search other way
        public override FloatArray ComputeErrorNBackWard(FloatArray target)
        {
            var dh = _target.OutputVector.Clone();
            var pos = target.Pos(1);
            dh[pos] -= 1;
            _target.ErrorVector = dh;
            return (_target.ErrorVector * _target.KnowlodgeMatrix).SumColumn();
        }
    }
}