using System.Threading.Tasks;
using VI.Neural.Node;
using VI.NumSharp.Arrays;

namespace VI.Neural.extension
{
    public static class ArrayExtension
    {
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
    }
}
