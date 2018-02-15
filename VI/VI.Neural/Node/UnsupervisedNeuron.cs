using VI.Neural.ANNOperations;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public class UnsupervisedNeuron : NeuronBase, INeuron, IUnsupervisedLearning
	{
		protected readonly IUnsupervisedOperations _operations;

		public UnsupervisedNeuron(int nodeSize,
			int connectionSize,
			float learningRate,
			float momentum,
			IUnsupervisedOperations operations) : base(nodeSize, connectionSize, learningRate, momentum)
		{
			_operations = operations;
			_operations.SetLayer(_layer);
		}

		public FloatArray Output(FloatArray inputs)
		{
			_operations.FeedForward(inputs);
			return _layer.OutputVector;
		}

		public FloatArray Output(float[] inputs)
		{
			using (var i = new FloatArray(inputs))
			{
				return Output(i);
			}
		}

		public void Learn(float[] inputs)
		{
			using (var i = new FloatArray(inputs))
			{
				Learn(i);
			}
		}

		public void Learn(FloatArray inputs)
		{
			_operations.ErrorGradient(inputs);
			_operations.UpdateParams();
		}
	}
}