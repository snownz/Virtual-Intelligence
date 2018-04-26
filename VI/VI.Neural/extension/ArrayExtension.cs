using System.Threading.Tasks;
using VI.Neural.Node;
using VI.NumSharp.Arrays;

namespace VI.Neural.extension
{
    public static class ArrayExtension
    {
        public static void SetOutputs(this Array<INeuron> neurons, Array<FloatArray> outputs)
        {
            Parallel.For(0, neurons.Length, i => neurons[i].Output = outputs[i]);
        }

        public static void SetOutputs(this Array<IMultipleNeuron> neurons, Array<FloatArray> outputs)
        {
            Parallel.For(0, neurons.Length, i => neurons[i].Output = outputs[i]);
        }

        public static Array<FloatArray> GetBias(this Array<INeuron> arr)
        {
            var r = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].Nodes.BiasVector);
            return r;
        }

        public static Array<FloatArray> GetOutput(this Array<INeuron> arr)
        {
            var r = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].Nodes.OutputVector);
            return r;
        }

        public static Array<FloatArray> GetBiasGradient(this Array<INeuron> arr)
        {
            var r = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].Nodes.ErrorVector);
            return r;
        }

        public static Array<FloatArray> GetBiasGradient(this Array<IMultipleNeuron> arr)
        {
            var r = new Array<FloatArray>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].BGradients);
            return r;
        }

        public static Array<FloatArray2D> GetWeights(this Array<INeuron> arr)
        {
            var r = new Array<FloatArray2D>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].Nodes.KnowlodgeMatrix);
            return r;
        }

        public static Array<FloatArray2D> GetWeightsGradient(this Array<INeuron> arr)
        {
            var r = new Array<FloatArray2D>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].Nodes.GradientMatrix);
            return r;
        }

        public static Array<Array<FloatArray2D>> GetWeightsGradient(this Array<IMultipleNeuron> arr)
        {
            var r = new Array<Array<FloatArray2D>>(arr.Length);
            Parallel.For(0, arr.Length, i => r[i] = arr[i].WGradients);
            return r;
        }

        public static int[] Len(this Array<INeuron> n)
        {
            var r = new int[n.Length];
            Parallel.For(0, n.Length, i => r[i] = n[i].Connections);
            return r;
        }
    }
}