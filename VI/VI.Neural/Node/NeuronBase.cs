using VI.Maths.Random;
using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public class NeuronBase
	{
		private static readonly ThreadSafeRandom _tr = new ThreadSafeRandom();

		protected readonly ILayer _layer;

		public NeuronBase(int nodeSize,
			int                  connectionSize,
			float                learningRate,
			float                momentum)
		{
			_layer = new ActivationLayer(nodeSize, connectionSize)
			{
				LearningRate = learningRate,
				Momentum     = momentum
			};
			InitializeArrays(nodeSize, connectionSize);
		}

		public int NodesSize   => _layer.Size;
		public int Connections => _layer.ConectionsSize;

		public ILayer Nodes => _layer;

		private void InitializeArrays(int nodeSize, int connectionSize)
		{
			_layer.KnowlodgeMatrix = new FloatArray2D(nodeSize, connectionSize);
			_layer.GradientMatrix  = new FloatArray2D(nodeSize, connectionSize);
			_layer.OutputVector    = new FloatArray(nodeSize);
		}

		public override string ToString()
		{
			return _layer.KnowlodgeMatrix.ToString();
		}

		//TODO Make it on GPU
		public void Synapsis(int node, int connection)
		{
			_layer.KnowlodgeMatrix[node, connection] = (float) _tr.NextDouble();
		}

		public void Synapsis(int node, int connection, float w)
		{
			_layer.KnowlodgeMatrix[node, connection] = (float) _tr.NextDouble() * w;
		}

		public void LoadSynapse(float[,] data)
		{
			_layer.KnowlodgeMatrix = new FloatArray2D(data);
		}
	}
}