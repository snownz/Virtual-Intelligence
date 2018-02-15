using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
	public class LayerBuilder : LayerCreator
	{
		private byte[,] connectionMask;
		private float[,] Knowlodge;
		private bool withOutBias;

		public LayerBuilder(float learningRate, float dropout, float momentum,
			ISupervisedOperations supervised,
			IUnsupervisedOperations unsupervised, IActivationFunction activation, IOptimizerFunction optimizer,
			IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight, int size,
			int connections,
			(int w, int h) size2D) :
			base(learningRate, dropout, momentum, supervised, unsupervised, activation, optimizer, error,
				nodesToSynapsys, weight, size, connections, size2D)
		{
		}

		public LayerBuilder LoadSynapse(float[,] data)
		{
			Knowlodge = data;
			return this;
		}

		public LayerBuilder WithoutBias()
		{
			withOutBias = true;
			return this;
		}

		public LayerBuilder WithConnectionMask(byte[,] mask)
		{
			withOutBias = true;
			return this;
		}

		public INeuron Build()
		{
			INeuron neuron = null;

			if (_supervised != null)
			{
				_supervised.SetActivation(_activation);
				_supervised.SetError(_error);
				_supervised.SetOptimizer(_optimizer);

				if (withOutBias)
				{
					var bMask = new byte[_size];
					neuron    = new SupervisedNeuron(_size, _connections, _learningRate, _momentum, _supervised, bMask,
						connectionMask);
				}
				else
				{
					neuron = new SupervisedNeuron(_size, _connections, _learningRate, _momentum, connectionMask, _supervised);
				}
			}
			else if (_unsupervised != null)
			{
				neuron = new UnsupervisedNeuron(_size, _connections, _learningRate, _momentum, _unsupervised);
			}

			if (Knowlodge == null)
				if (_weight  > 0)
					SynapseFull(neuron, _weight);
				else
					SynapseFull(neuron);
			else
				neuron.LoadSynapse(Knowlodge);

			return neuron;
		}

		private void SynapseFull(INeuron n)
		{
			for (var i = 0; i < n.NodesSize; i++)
			for (var j = 0; j < n.Connections; j++)
				Synapse(n, i, j);
		}

		private void Synapse(INeuron n, int node, int connection)
		{
			n.Synapsis(node, connection);
		}

		private void SynapseFull(INeuron n, float w)
		{
			for (var i = 0; i < n.NodesSize; i++)
			for (var j = 0; j < n.Connections; j++)
				Synapse(n, i, j, w);
		}

		private void Synapse(INeuron n, int node, int connection, float w)
		{
			n.Synapsis(node, connection, w);
		}
	}
}