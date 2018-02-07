using System;
using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
	public class LayerCreator : IDisposable
	{
		protected (int w, int h)      _2Dsize;
		protected IActivationFunction _activation;
		protected int                 _connections;
		protected float               _degradation;
		protected float               _dropout;
		protected IErrorFunction      _error;
		protected float               _learningRate;
		protected float               _maxDist;
		protected float               _momentum;

		protected IList<(int x, int y)> _nodesToSynapsys;
		protected IOptimizerFunction    _optimizer;

		protected int _size;

		protected ISupervisedOperations   _supervised;
		protected IUnsupervisedOperations _unsupervised;
		protected float                   _weight;

		public LayerCreator(float learningRate, float dropout, float momentum,
			ISupervisedOperations    supervised,
			IUnsupervisedOperations  unsupervised, IActivationFunction activation, IOptimizerFunction optimizer,
			IErrorFunction           error, IList<(int x, int y)>      nodesToSynapsys, float         weight, int size,
			int                      connections,
			(int w, int h)           size2D) : this(size, connections)
		{
			_learningRate    = learningRate;
			_dropout         = dropout;
			_momentum        = momentum;
			_supervised      = supervised;
			_unsupervised    = unsupervised;
			_activation      = activation;
			_optimizer       = optimizer;
			_error           = error;
			_nodesToSynapsys = nodesToSynapsys;
			_weight          = weight;
			_2Dsize          = size2D;
		}

		public LayerCreator(int size, int connections)
		{
			_size        = size;
			_connections = connections;
		}

		public LayerCreator((int w, int h) size, int connections)
		{
			_2Dsize      = size;
			_connections = connections;
		}

		public void Dispose()
		{
			_supervised   = null;
			_unsupervised = null;
			_activation   = null;
			_optimizer    = null;
			_error        = null;
		}

		public LayerCreatorSupervised Supervised()
		{
			return new LayerCreatorSupervised(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
				_activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections, _2Dsize);
		}

		public LayerCreatorUnsupervised Unsupervised()
		{
			return new LayerCreatorUnsupervised(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
				_activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections, _2Dsize);
		}

		public LayerCreator WithLearningRate(float value)
		{
			_learningRate = value;
			return this;
		}

		public LayerCreator AddDropout(float value)
		{
			_dropout = value;
			return this;
		}

		public LayerCreator AddSynapse(int position, int connection)
		{
			_nodesToSynapsys.Add((position, connection));
			return this;
		}

		public LayerCreator FullSynapse()
		{
			_nodesToSynapsys = null;
			return this;
		}

		public LayerCreator FullSynapse(float value)
		{
			_nodesToSynapsys = null;
			_weight          = value;
			return this;
		}
	}
}