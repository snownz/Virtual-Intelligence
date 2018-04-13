using VI.Neural.ANNOperations;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class SupervisedNeuron : INeuron
    {
        protected readonly ISupervisedOperations _operations;
        protected readonly ILayer _layer;

        public int NodesSize => _layer.Size;
        public int Connections => _layer.ConectionsSize;
        public ILayer Nodes => _layer;

        public FloatArray Output { get => _layer.OutputVector; set => _layer.OutputVector = value; }
        public FloatArray2D WGradients => _layer.GradientMatrix;
        public FloatArray BGradients => _layer.ErrorVector;

        public FloatArray2D Weights { get => _layer.KnowlodgeMatrix; set => _layer.KnowlodgeMatrix = value; }
        public FloatArray Bias { get => _layer.BiasVector; set => _layer.BiasVector = value; }

        public SupervisedNeuron(
            int nodeSize,
            int connectionSize,
            float learningRate,
            float momentum,
           ISupervisedOperations operations)
        {
            _layer = new ActivationLayer(nodeSize, connectionSize)
            {
                LearningRate = learningRate,
                Momentum = momentum
            };
            _operations = operations;
            _operations.SetLayer(_layer);
            InitializeArrays(nodeSize, connectionSize);
        }

        private void InitializeArrays(int nodeSize, int connectionSize)
        {
            _layer.GradientMatrix = new FloatArray2D(nodeSize, connectionSize);
            _layer.OutputVector = new FloatArray(nodeSize);
            _layer.BiasVector = new FloatArray(nodeSize);
        }

        public FloatArray FeedForward(FloatArray x)
        {
            _operations.Summarization(x);
            _operations.Activate();
            return Output;
        }

        public FloatArray ComputeErrorNBackWard(FloatArray target)
        {
            return _operations.ComputeErrorNBackWard(target);
        }

         public FloatArray ComputeErrorNBackWard(FloatArray target, FloatArray compl)
        {
            return _operations.ComputeErrorNBackWard(target, compl);
        }

        public FloatArray BackWard(FloatArray dw)
        {
            return _operations.BackWard(dw);
        }

        public void ComputeGradient(FloatArray input)
        {
            _operations.ComputeGradient(input);
        }

        public void UpdateParams(FloatArray2D dw, FloatArray db)
        {
            _operations.UpdateParams(dw, db);
        }

        public void FullSynapsis(int node, int connection, float std)
        {
            _layer.KnowlodgeMatrix = NumMath.Random(node, connection, std);
        }

        public void LoadSynapse(float[,] data)
        {
            _layer.KnowlodgeMatrix = new FloatArray2D(data);
        }

        public override string ToString()
        {
            return _layer.KnowlodgeMatrix.ToString();
        }
    }
}