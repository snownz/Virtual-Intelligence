using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.extension;
using VI.Neural.Factory;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class DenseModel
    {
        private Array<INeuron> W;

        public DenseModel(Array<INeuron> layers)
        {
            W = layers;
        }

        public DenseModel()
        {
            W = new Array<INeuron>(0);
        }

        public void AddLayer(INeuron layer)
        {
            W = W.Join(layer);
        }

        public FloatArray FeedForward(FloatArray inputs)
        {
            var x = inputs;

            for (int i = 0; i < W.Length; i++)
            {
                x = W[i].FeedForward(x);
            }
            
            return x;
        }

        public FloatArray ComputeErrorNBackward(FloatArray target)
        {
            var dw = W[W.Length - 1].ComputeErrorNBackWard(target);

            for (int i = W.Length - 2; i >= 0; i--)
            {
                dw = W[i].BackWard(dw);
            }

            return dw;
        }

        public FloatArray Backward(FloatArray error)
        {
            var dw = new FloatArray(0);

            for (int i = W.Length - 1; i >= 0; i--)
            {
                dw = W[i].BackWard(dw);
            }

            return dw;
        }

        public (Array<FloatArray2D> dw, Array<FloatArray> db) ComputeGradient(FloatArray input)
        {
            W[0].ComputeGradient(input);

            Parallel.For(1, W.Length, i =>
            {
                W[i].ComputeGradient(W[i - 1].Nodes.OutputVector);
            });

            return (W.GetWeightsGradient(), W.GetBiasGradient());
        }

        public void UpdateParams(Array<FloatArray2D> dw, Array<FloatArray> db)
        {
            Parallel.For(0, W.Length, i =>
            {
                W[i].UpdateParams(dw[i], db[i]);
            });
        }
    }    
}
