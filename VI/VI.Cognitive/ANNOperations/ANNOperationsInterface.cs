using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.Cognitive.Layer;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.ANNOperations
{
    public class ANNOperationsInterface
    {
        private readonly IAnnParallelInterface _operations;

        public IAnnParallelInterface Device => _operations;

        public ANNOperationsInterface(IAnnParallelInterface operations)
        {
            _operations = operations;
        }

        public void FeedForward(MemoryBuffer<float> feed, ActivationLayer forWard)
        {
            _operations
                .Executor
                .Execute("_zeros_"
                         , forWard.Size
                         , forWard.SumVector.View);
            _operations
                .Executor
                .Execute("_sum_weights_"
                         , forWard.MSize
                         , forWard.SumVector.View
                         , feed.View
                         , forWard.KnowlodgeMatrix.View);
            _operations
                .Executor
                .Execute("_sum_1D_"
                         , forWard.Size
                         , forWard.SumVector.View
                         , forWard.SumVector.View
                         , forWard.BiasVector.View);
            _operations
                .Executor
                .Execute("_activation_"
                         , forWard.Size
                         , forWard.OutputVector.View
                         , forWard.SumVector.View);
        }
        public void BackWardOutputGradient(ActivationLayer target, MemoryBuffer<float> desired)
        {
            using (var de_dOut = _operations.Executor.CreateBuffer<float>(target.Size))
            {
                using (var dOut_dSum = _operations.Executor.CreateBuffer<float>(target.Size))
                {
                    _operations
                       .Executor
                       .Execute("_sub_1D_"
                                , target.Size
                                , de_dOut.View
                                , desired.View
                                , target.OutputVector.View);
                    _operations
                      .Executor
                      .Execute("_derivative_"
                               , target.Size
                               , dOut_dSum.View
                               , target.SumVector.View);
                    _operations
                      .Executor
                      .Execute("_multi_1D_1D_"
                               , target.Size
                               , target.ErrorVector.View
                               , de_dOut.View
                               , dOut_dSum.View);
                }
            }
            //backpropagate error
        }

        public void BackWardHiddenGradient(ActivationLayer target, MemoryBuffer<float> error)
        {
            var de_dOut = error;
            using (var dOut_dSum = _operations.Executor.CreateBuffer<float>(target.Size))
            {
                _operations
                  .Executor
                  .Execute("_derivative_"
                           , target.Size
                           , dOut_dSum.View
                           , target.SumVector.View);
                _operations
                  .Executor
                  .Execute("_multi_1D_1D_"
                           , target.Size
                           , target.ErrorVector.View
                           , de_dOut.View
                           , dOut_dSum.View);
            }
            //backpropagate error
        }

        public void ComputeErrorLayer(ActivationLayer target)
        {
            _operations
                .Executor
                .Execute("_process_error_to_back_propagate"
                         , target.MSize
                         , target.ErrorWeightVector.View
                         , target.ErrorVector.View
                         , target.KnowlodgeMatrix.View);
        }

        public void ComputeCachedVariables(ActivationLayer target)
        {
            target.CachedMomentum = target.LearningRate * target.Momentum;
            target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);
        }

        public void UpdateWeight(ActivationLayer target, MemoryBuffer<float> inputs)
        {
            _operations
                .Executor
                .Execute("_update_weights_"
                         , target.MSize
                         , target.KnowlodgeMatrix.View
                         , target.ErrorVector.View
                         , inputs.View
                         , target.CachedLearningRate
                         , target.CachedMomentum
                        );
        }

        public void UpdateBias(ActivationLayer target)
        {
            _operations
                .Executor
                .Execute("_update_bias_"
                         , target.Size
                         , target.BiasVector.View
                         , target.ErrorVector.View
                         , target.CachedLearningRate
                         , target.CachedMomentum
                        );
        }
    }
}
