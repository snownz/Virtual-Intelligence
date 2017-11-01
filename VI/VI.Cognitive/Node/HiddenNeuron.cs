using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.Cognitive.ANNOperations;
using VI.Cognitive.Layer;
using VI.Maths.Random;
using VI.NumSharp.Array;

namespace VI.Cognitive.Node
{
    public class HiddenNeuron : INeuron
    {
        private ActivationLayer _layer;
        private static ThreadSafeRandom _tr = new ThreadSafeRandom();
        private readonly ANNOperationsInterface _ann;

        public int Nodes => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public HiddenNeuron(int nodeSize, int connectionSize, float learningRate, float momentum,
            ANNOperationsInterface operations)
        {
            _layer = new ActivationLayer(nodeSize, connectionSize);
            _ann = operations;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            _layer.KnowlodgeMatrix = _ann.Device.Executor.CreateBuffer<float>(nodeSize, connectionSize);
            watch.Stop();
            Console.WriteLine($"Matrix Time: {watch.ElapsedMilliseconds}ms");

            watch = System.Diagnostics.Stopwatch.StartNew();
            _layer.BiasVector = _ann.Device.Executor.CreateBuffer<float>(nodeSize);
            watch.Stop();
            Console.WriteLine($"Vector Time: {watch.ElapsedMilliseconds}ms");

            _layer.OutputVector = _ann.Device.Executor.CreateBuffer<float>(nodeSize);
            _layer.SumVector = _ann.Device.Executor.CreateBuffer<float>(nodeSize);
            _layer.ErrorVector = _ann.Device.Executor.CreateBuffer<float>(nodeSize);
            _layer.ErrorWeightVector = _ann.Device.Executor.CreateBuffer<float>(connectionSize);

            _layer.LearningRate = learningRate;
            _layer.Momentum = momentum;

            for (int i = 0; i < nodeSize; i++)
            {
                _layer.BiasVector[i] = 1;
            }
        }

        public MemoryBuffer<float> Output(float[] inputs)
        {
            using (var i = _ann.Device.Executor.SetBuffer(inputs))
            {
                _ann.FeedForward(i, _layer);
                return _layer.OutputVector;
            }
        }
        public MemoryBuffer<float> Output(MemoryBuffer<float> inputs)
        {
            _ann.FeedForward(inputs, _layer);
            return _layer.OutputVector;
        }

        public MemoryBuffer<float> Learn(float[] inputs, float[] error)
        {
            using (var i = _ann.Device.Executor.SetBuffer(inputs))
            {
                using (var e = _ann.Device.Executor.SetBuffer(error))
                {
                    _ann.ComputeCachedVariables(_layer);
                    _ann.BackWardHiddenGradient(_layer, e);
                    _ann.ComputeErrorLayer(_layer);
                    _ann.UpdateWeight(_layer, i);
                    _ann.UpdateBias(_layer);
                    return _layer.ErrorWeightVector;
                }
            }
        }
        public MemoryBuffer<float> Learn(float[] inputs, MemoryBuffer<float> error)
        {
            using (var i = _ann.Device.Executor.SetBuffer(inputs))
            {
                _ann.ComputeCachedVariables(_layer);
                _ann.BackWardHiddenGradient(_layer, error);
                _ann.ComputeErrorLayer(_layer);
                _ann.UpdateWeight(_layer, i);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public MemoryBuffer<float> Learn(MemoryBuffer<float> inputs, float[] error)
        {
            using (var e = _ann.Device.Executor.SetBuffer(error))
            {
                _ann.ComputeCachedVariables(_layer);
                _ann.BackWardHiddenGradient(_layer, e);
                _ann.ComputeErrorLayer(_layer);
                _ann.UpdateWeight(_layer, inputs);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public MemoryBuffer<float> Learn(MemoryBuffer<float> inputs, MemoryBuffer<float> error)
        {
            _ann.ComputeCachedVariables(_layer);
            _ann.BackWardHiddenGradient(_layer, error);
            _ann.ComputeErrorLayer(_layer);
            _ann.UpdateWeight(_layer, inputs);
            _ann.UpdateBias(_layer);
            return _layer.ErrorWeightVector;
        }

        public void Synapsis(int node, int connection)
        {
            _layer.KnowlodgeMatrix[new Index2(node, connection)] = (float)_tr.NextDouble();
        }
    }
    
    public class HiddenNeuron2 : INeuron
    {
        private ActivationLayer2 _layer;
        private static ThreadSafeRandom _tr = new ThreadSafeRandom();
        private readonly ANNBasicOperations _ann;

        public int Nodes => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public HiddenNeuron2(int nodeSize, int connectionSize, float learningRate, ANNBasicOperations operations)
        {
            _layer = new ActivationLayer2(nodeSize, connectionSize);
            _ann = operations;

            _layer.KnowlodgeMatrix = new Array2D<float>(nodeSize, connectionSize);
         
            _layer.LearningRate = learningRate;
            _layer.CachedLearningRate = learningRate;

            
            _layer.OutputVector = new Array<float>(nodeSize);

            _layer.BiasVector = new Array<float>(nodeSize);
            for (var i = 0; i < nodeSize; i++)
            {
                _layer.BiasVector[i] = 1;
            }
        }
        
        public Array<float> Output(Array<float> inputs)
        {
            _ann.FeedForward(_layer, inputs);
            return _layer.OutputVector;
        }
        public Array<float> Output(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.FeedForward(_layer, i);
                return _layer.OutputVector;
            }                
        }

        public Array<float> Learn(float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.BackWard(_layer, error);
                _ann.BackWardError(_layer, _layer.ErrorVector);
                _ann.ErrorGradient(_layer, _layer.ErrorVector, i);
                _ann.UpdateWeight(_layer);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }

        public void Synapsis(int node, int connection)
        {
            _layer.KnowlodgeMatrix[node, connection] = (float)_tr.NextDouble();
        }
    }
}
