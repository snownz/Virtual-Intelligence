using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public interface IMultipleNeuron
    {
        FloatArray BGradients { get; }
        FloatArray Bias { get; set; }
        int[] Connections { get; }
        IMultipleLayer Nodes { get; }
        int NodesSize { get; }
        FloatArray Output { get; set; }
        Array<FloatArray2D> Weights { get; }
        Array<FloatArray2D> WGradients { get; }

        Array<FloatArray> BackWard(FloatArray dw);

        Array<FloatArray> ComputeErrorNBackWard(FloatArray target);

        void ComputeGradient(Array<FloatArray> input);

        FloatArray FeedForward(Array<FloatArray> x);

        void FullSynapsis(float std);

        void LoadSynapse(FloatArray2D[] data);

        string ToString();

        void UpdateParams(Array<FloatArray2D> dw, FloatArray db);
    }
}