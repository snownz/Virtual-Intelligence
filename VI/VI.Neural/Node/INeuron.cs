using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public interface INeuron
	{
		int NodesSize { get; }
		ILayer Nodes { get; }
		int Connections { get; }
		FloatArray Output(FloatArray inputs);
		FloatArray Output(float[] inputs);
		void Synapsis(int node, int connection);
		void Synapsis(int node, int connection, float w);
		void LoadSynapse(float[,] data);
	}
}