using VI.Neural.ANNOperations;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public class SupervisedNeuron : NeuronBase, INeuron, ISupervisedLearning
	{
		protected readonly ISupervisedOperations _operations;

		public SupervisedNeuron(int nodeSize,
			int connectionSize,
			float learningRate,
			float momentum,
			byte[,] connections,
			ISupervisedOperations operations) : base(nodeSize, connectionSize, learningRate, momentum)
		{
			_operations = operations;
			_operations.SetLayer(_layer);
			SetpArray(nodeSize);
			_layer.ConnectionMask = new ByteArray2D(connections);
		}

		public SupervisedNeuron(int nodeSize,
			int connectionSize,
			float learningRate,
			float momentum,
			ISupervisedOperations operations,
			byte[] biasMask,
			byte[,] connections
		) : this(nodeSize, connectionSize, learningRate, momentum, connections, operations)
		{
			_layer.BiasMask = new ByteArray(biasMask);
		}

		public FloatArray Output(float[] inputs)
		{
			using (var i = new FloatArray(inputs))
			{
				return Output(i);
			}
		}

		public FloatArray Output(FloatArray inputs)
		{
			_operations.FeedForward(inputs);
			return _layer.OutputVector;
		}

		public FloatArray ComputeGradient(float[] inputs, FloatArray error)
		{
			using (var i = new FloatArray(inputs))
			{
				return ComputeGradient(i, error);
			}
		}

		public FloatArray ComputeGradient(FloatArray inputs, float[] error)
		{
			using (var e = new FloatArray(error))
			{
				return ComputeGradient(inputs, e);
			}
		}

		public FloatArray ComputeGradient(FloatArray inputs, FloatArray error)
		{
			_operations.BackWard(error);
			_operations.ComputeGradient(inputs);
			return _layer.ErrorWeightVector;
		}

		public void UpdateParams()
		{
			_operations.UpdateParams();
			_layer.GradientMatrix = new FloatArray2D(NodesSize, Connections);
		}

		public FloatArray Learn(float[] inputs, FloatArray error)
		{
			using (var i = new FloatArray(inputs))
			{
				return Learn(i, error);
			}
		}

		public FloatArray Learn(FloatArray inputs, float[] error)
		{
			using (var e = new FloatArray(error))
			{
				return Learn(inputs, e);
			}
		}

		public FloatArray Learn(FloatArray inputs, FloatArray error)
		{
			_operations.BackWard(error);
			_operations.ErrorGradient(inputs);
			_operations.UpdateParams();
			return _layer.ErrorWeightVector;
		}

		private void SetpArray(int nodeSize)
		{
			_layer.BiasVector                                       = new FloatArray(nodeSize);
			for (var i = 0; i < nodeSize; i++) _layer.BiasVector[i] = 1;
		}
	}
}