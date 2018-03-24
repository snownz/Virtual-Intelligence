using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ANNOperations;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class SupervisedMultipleNeuron : IMultipleNeuron
    {
        protected readonly ISupervisedMultipleOperations _operations;
        protected readonly IMultipleLayer _layer;

        public FloatArray Output { get => _layer.OutputVector; set => _layer.OutputVector = value; }
        public Array<FloatArray2D> Weights => _layer.KnowlodgeMatrix;
        public Array<FloatArray2D> WGradients => _layer.GradientMatrix;
        public FloatArray BGradients => _layer.ErrorVector;

        public int NodesSize => _layer.Size;
        public int[] Connections => _layer.ConectionsSize;

        public IMultipleLayer Nodes => _layer;

        public FloatArray Bias { get => _layer.BiasVector; set => _layer.BiasVector = value; }

        public SupervisedMultipleNeuron(
           int nodeSize,
           int[] connectionSize,
           float learningRate,
           float momentum,
          ISupervisedMultipleOperations operations)
        {
            _layer = new MultipleActivationLayer(nodeSize, connectionSize)
            {
                LearningRate = learningRate,
                Momentum = momentum
            };
            _operations = operations;
            _operations.SetLayer(_layer);
            InitializeArrays(nodeSize, connectionSize);
        }

        private void InitializeArrays(int nodeSize, int[] connectionSize)
        {
            _layer.GradientMatrix = NumMath.Array(nodeSize, connectionSize);
            _layer.OutputVector = new FloatArray(nodeSize);
            _layer.BiasVector = new FloatArray(nodeSize);
            _layer.SumVector = new Array<FloatArray>(connectionSize.Length);
        }

        public FloatArray FeedForward(Array<FloatArray> x)
        {
            _operations.Summarization(x);
            _operations.Activate();
            return Output;
        }

        public Array<FloatArray> ComputeErrorNBackWard(FloatArray target)
        {
            return _operations.ComputeErrorNBackWard(target);
        }

        public Array<FloatArray> BackWard(FloatArray dw)
        {
            return _operations.BackWard(dw);
        }
               
        public void ComputeGradient(Array<FloatArray> input)
        {
            _operations.ComputeGradient(input);
        }

        public void UpdateParams(Array<FloatArray2D> dw, FloatArray db)
        {
            _operations.UpdateParams(dw, db);
        }

        public void FullSynapsis(float std)
        {
            _layer.KnowlodgeMatrix = NumMath.Random(NodesSize, Connections, std);
        }

        public void LoadSynapse(FloatArray2D[] data)
        {
            _layer.KnowlodgeMatrix = new Array<FloatArray2D>(data);
        }

        public override string ToString()
        {
            return _layer.KnowlodgeMatrix.ToString();
        }
    }
}
