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
        private Array<INeuron> w;

        public int[] Len => w.Len();
        
        public DenseModel(Array<INeuron> layers)
        {
            w = layers;
        }

        public DenseModel()
        {
            w = new Array<INeuron>(0);
        }

        /// <summary>
        /// Include a new Layer on the model
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(INeuron layer)
        {
            w = w.Join(layer);
        }

        /// <summary>
        /// Execute Feedforward N Get All layer Results
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>All layer Results</returns>
        public Array<FloatArray> FeedForward(FloatArray inputs)
        {
            var result = new Array<FloatArray>(w.Length);
            result[-1] = inputs;

            for (int i = 0; i < w.Length; i++)
            {
                result[i] = w[i].FeedForward(result[i - 1]);
            }
            
            return result;
        }

        /// <summary>
        /// Execute Feedforward N Get Output Layer Result
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>Output Layer Result</returns>
        public FloatArray Output(FloatArray inputs)
        {
            return FeedForward(inputs)[w.Length - 1];
        }

        /// <summary>
        /// Compute error for linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray ComputeErrorNBackward(FloatArray target)
        {
            var dw = w[w.Length - 1].ComputeErrorNBackWard(target);

            for (int i = w.Length - 2; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Compute error for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public FloatArray ComputeErrorNBackward(FloatArray target, Array<FloatArray> output)
        {
            w.SetOutputs(output);
            var dw = w[w.Length - 1].ComputeErrorNBackWard(target);

            for (int i = w.Length - 2; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Backward for linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray Backward(FloatArray error)
        {
            var dw = new FloatArray(0);

            for (int i = w.Length - 1; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Backward for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray Backward(FloatArray error, Array<FloatArray> output)
        {
            w.SetOutputs(output);
            var dw = new FloatArray(0);

            for (int i = w.Length - 1; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Compute All layer Gradients
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (Array<FloatArray2D> dw, Array<FloatArray> db) ComputeGradient(FloatArray input)
        {
            w[0].ComputeGradient(input);

            Parallel.For(1, w.Length, i =>
            {
                w[i].ComputeGradient(w[i - 1].Nodes.OutputVector);
            });

            return (w.GetWeightsGradient(), w.GetBiasGradient());
        }

        /// <summary>
        /// Update Params from all layers
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="db"></param>
        public void UpdateParams(Array<FloatArray2D> dw, Array<FloatArray> db)
        {
            Parallel.For(0, w.Length, i =>
            {
                w[i].UpdateParams(dw[i], db[i]);
            });
        }
    }    
}
