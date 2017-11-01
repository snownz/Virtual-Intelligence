using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.Cognitive.Layer;
using VI.Cognitive.Provider;
using VI.NumSharp.Array;

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

    #region testar
    //public sealed class ANNBasicOperations : IANNBasicOperations
    //{
    //    private readonly IAnnArrayProvider _operationsProvider;
    //    private readonly IActivationFunctionProvider _activationProvider;
    //    private readonly ILossFunctionProvider _lossProvider;

    //    public ANNBasicOperations(IAnnArrayProvider operations
    //                              , IActivationFunctionProvider activation
    //                              , ILossFunctionProvider loss)
    //    {
    //        _operationsProvider = operations;
    //        _activationProvider = activation;
    //        _lossProvider = loss;
    //    }

    //    public void FeedForward(MemoryBuffer<float> feed, ActivationLayer forWard)
    //    {
    //        _operationsProvider._V_zeros(forWard.Size, forWard.SumVector);
    //        using (var m = _operationsProvider._V_X_M_column_M(forWard.MSize, forWard.KnowlodgeMatrix, feed))
    //        {
    //            _operationsProvider._M_sum_line_V(forWard.MSize, forWard.SumVector, m);
    //        }
    //        _operationsProvider._V_sum_V(forWard.Size, forWard.SumVector, forWard.BiasVector);
    //        _activationProvider.Activation(forWard.SumVector, forWard.OutputVector);
    //    }      

    //    public void BackWardDesired(ActivationLayer target, MemoryBuffer<float> desired)
    //    {
    //        using (var de_dOut = _lossProvider.Error(target.OutputVector, desired))
    //        {
    //            using (var dOut_dSum = _activationProvider.Derivated(target.SumVector))
    //            {
    //                _operationsProvider._V_X_V(target.Size, target.ErrorVector, de_dOut, dOut_dSum);
    //            }
    //        }
    //    }

    //    public void BackWard(ActivationLayer target, MemoryBuffer<float> de_dOut)
    //    {
    //        using (var dOut_dSum = _activationProvider.Derivated(target.SumVector))
    //        {
    //            _operationsProvider._V_X_V(target.Size, target.ErrorVector, de_dOut, dOut_dSum);
    //        }
    //    }

    //    public void BackWardError(ActivationLayer target, MemoryBuffer<float> error)
    //    {
    //        using (var cachedError = _operationsProvider._V_X_M_line_M(target.MSize, target.KnowlodgeMatrix, error))
    //        {
    //            _operationsProvider._M_sum_column_V(target.MSize, target.ErrorWeightVector, cachedError);
    //        }
    //    }

    //    public void ErrorGradient(ActivationLayer target, MemoryBuffer<float> error, MemoryBuffer<float> inputs)
    //    {
    //        _operationsProvider._V_X_V_M(target.MSize, target.GradientMatrix, error, inputs);
    //        _operationsProvider._C_X_M(target.MSize, target.GradientMatrix, target.CachedLearningRate);
    //    }

    //    public void UpdateWeight(ActivationLayer target)
    //    {
    //        _operationsProvider._M_sum_M(target.MSize, target.KnowlodgeMatrix, target.GradientMatrix);
    //    }
    //    public void UpdateWeight(ActivationLayer target, MemoryBuffer2D<float> u)
    //    {
    //        _operationsProvider._M_sum_M(target.MSize, target.KnowlodgeMatrix, target.GradientMatrix, u);
    //    }

    //    public void UpdateBias(ActivationLayer target)
    //    {
    //        using (var biasAjust = _operationsProvider._C_X_V(target.Size, target.CachedLearningRate, target.ErrorVector))
    //        {
    //            _operationsProvider._V_sum_V(target.Size, target.BiasVector, biasAjust);
    //        }
    //    }
    //    public void UpdateBias(ActivationLayer target, MemoryBuffer<float> u)
    //    {
    //        using (var biasAjust = _operationsProvider._C_X_V(target.Size, target.CachedLearningRate, target.ErrorVector))
    //        {
    //            _operationsProvider._V_sum_V(target.Size, target.BiasVector, biasAjust, u);
    //        }
    //    }
    //}
    #endregion
}
