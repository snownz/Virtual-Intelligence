using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public interface INeuron
    {
        FloatArray Output { get; set; }
        FloatArray2D Weights { get; set; }
        FloatArray2D WGradients { get; }
        FloatArray BGradients { get; }
        FloatArray Bias { get; set; }

        int NodesSize { get; }
        ILayer Nodes { get; }
        int Connections { get; }

        void FullSynapsis(int node, int connection, float std);

        void LoadSynapse(float[,] data);

        FloatArray FeedForward(FloatArray x);

        FloatArray ComputeErrorNBackWard(FloatArray target);

        FloatArray ComputeErrorNBackWard(FloatArray target, FloatArray compl);

        FloatArray BackWard(FloatArray dw);

        void ComputeGradient(FloatArray input);

        void UpdateParams(FloatArray2D dw, FloatArray db);
    }
}