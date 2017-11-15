using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
    public class ActivationLayer : ILayer
    {
        private Array2D<float> _knowlodgeMatrix;
        private Array2D<float> _gradientMatrix;
        private Array<float> _biasVector;
        private Array<float> _outputVector;
        private Array<float> _transformVector;
        private Array<float> _errorVector;
        private Array<float> _errorWeightVector;
        private Array<float> _dropOutProbability;

        private float _learningRate;
        private float _cachedLearningRate;
        private float _momentum;
        private float _cachedMomentum;

        private int _size;
        private int _conectionsSize;
                     
        public Array<float> DropOutProbability { get => _dropOutProbability; set => _dropOutProbability = value; }
        public Array2D<float> KnowlodgeMatrix { get => _knowlodgeMatrix; set => _knowlodgeMatrix = value; }
        public Array2D<float> GradientMatrix { get => _gradientMatrix; set => _gradientMatrix = value; }
        public Array<float> BiasVector { get => _biasVector; set => _biasVector = value; }
        public Array<float> OutputVector { get => _outputVector; set => _outputVector = value; }
        public Array<float> SumVector { get => _transformVector; set => _transformVector = value; }
        public Array<float> ErrorVector { get => _errorVector; set => _errorVector = value; }
        public Array<float> ErrorWeightVector { get => _errorWeightVector; set => _errorWeightVector = value; }

        public float LearningRate { get => _learningRate; set => _learningRate = value; }
        public int Size { get => _size; set => _size = value; }
        public int ConectionsSize { get => _conectionsSize; set => _conectionsSize = value; }
        public float CachedLearningRate { get => _cachedLearningRate; set => _cachedLearningRate = value; }
        public float Momentum { get => _momentum; set => _momentum = value; }
        public float CachedMomentum { get => _cachedMomentum; set => _cachedMomentum = value; }

        public ActivationLayer(int size, int conectionsSize)
        {
            Size = size;
            ConectionsSize = conectionsSize;
        }
    }
}
