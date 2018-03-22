using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public interface INeuron
	{
        FloatArray Output { get; }
        FloatArray2D Weights { get; }
        FloatArray2D WGradients { get; }
        FloatArray BGradients { get; }

        int NodesSize { get; }
		ILayer Nodes { get; }
		int Connections { get; }
        void FullSynapsis(int node, int connection, float std);
        void LoadSynapse(float[,] data);
        FloatArray FeedForward(FloatArray x);
        FloatArray ComputeErrorNBackWard(FloatArray target);
        FloatArray BackWard(FloatArray dw);
        void ComputeGradient(FloatArray outputVector);
        void UpdateParams(FloatArray2D dw, FloatArray db);
    }
}