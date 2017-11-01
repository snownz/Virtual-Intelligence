using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Array;

namespace VI.Cognitive.Layer
{
    public class ActivationLayer
    {
        private MemoryBuffer2D<float> _knowlodgeMatrix;
        private MemoryBuffer2D<float> _gradientMatrix;
        private MemoryBuffer<float> _biasVector;
        private MemoryBuffer<float> _outputVector;
        private MemoryBuffer<float> _transformVector;
        private MemoryBuffer<float> _errorVector;
        private MemoryBuffer<float> _errorWeightVector;

        private float _learningRate;
        private float _cachedLearningRate;
        private float _momentum;
        private float _cachedMomentum;

        private int _size;
        private int _conectionsSize;

        public MemoryBuffer2D<float> KnowlodgeMatrix
        {
            get
            {
                return _knowlodgeMatrix;
            }

            set
            {
                _knowlodgeMatrix = value;
            }
        }
        public MemoryBuffer2D<float> GradientMatrix
        {
            get
            {
                return _gradientMatrix;
            }

            set
            {
                _gradientMatrix = value;
            }
        }
        public MemoryBuffer<float> BiasVector
        {
            get
            {
                return _biasVector;
            }

            set
            {
                _biasVector = value;
            }
        }
        public MemoryBuffer<float> OutputVector
        {
            get
            {
                return _outputVector;
            }

            set
            {
                _outputVector = value;
            }
        }
        public MemoryBuffer<float> SumVector
        {
            get
            {
                return _transformVector;
            }

            set
            {
                _transformVector = value;
            }
        }
        public MemoryBuffer<float> ErrorVector
        {
            get
            {
                return _errorVector;
            }

            set
            {
                _errorVector = value;
            }
        }
        public MemoryBuffer<float> ErrorWeightVector
        {
            get
            {
                return _errorWeightVector;
            }

            set
            {
                _errorWeightVector = value;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }
        public int ConectionsSize
        {
            get
            {
                return _conectionsSize;
            }

            set
            {
                _conectionsSize = value;
            }
        }
        public float LearningRate
        {
            get
            {
                return _learningRate;
            }

            set
            {
                _learningRate = value;
            }
        }
        public float CachedLearningRate
        {
            get
            {
                return _cachedLearningRate;
            }

            set
            {
                _cachedLearningRate = value;
            }
        }
        public float Momentum
        {
            get
            {
                return _momentum;
            }

            set
            {
                _momentum = value;
            }
        }
        public float CachedMomentum
        {
            get
            {
                return _cachedMomentum;
            }

            set
            {
                _cachedMomentum = value;
            }
        }

        public Index2 MSize => new Index2(_size, _conectionsSize);
        public ActivationLayer(int size, int conectionsSize)
        {
            Size = size;
            ConectionsSize = conectionsSize;
        }
    }

    public class ActivationLayer2
    {
        private Array2D<float> _knowlodgeMatrix;
        private Array2D<float> _gradientMatrix;
        private Array<float> _biasVector;
        private Array<float> _outputVector;
        private Array<float> _transformVector;
        private Array<float> _errorVector;
        private Array<float> _errorWeightVector;

        private float _learningRate;
        private float _cachedLearningRate;
        private float _momentum;
        private float _cachedMomentum;

        private int _size;
        private int _conectionsSize;

        public Array2D<float> KnowlodgeMatrix
        {
            get
            {
                return _knowlodgeMatrix;
            }

            set
            {
                _knowlodgeMatrix = value;
            }
        }
        public Array2D<float> GradientMatrix
        {
            get
            {
                return _gradientMatrix;
            }

            set
            {
                _gradientMatrix = value;
            }
        }
        public Array<float> BiasVector
        {
            get
            {
                return _biasVector;
            }

            set
            {
                _biasVector = value;
            }
        }
        public Array<float> OutputVector
        {
            get
            {
                return _outputVector;
            }

            set
            {
                _outputVector = value;
            }
        }
        public Array<float> SumVector
        {
            get
            {
                return _transformVector;
            }

            set
            {
                _transformVector = value;
            }
        }
        public Array<float> ErrorVector
        {
            get
            {
                return _errorVector;
            }

            set
            {
                _errorVector = value;
            }
        }
        public Array<float> ErrorWeightVector
        {
            get
            {
                return _errorWeightVector;
            }

            set
            {
                _errorWeightVector = value;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }
        public int ConectionsSize
        {
            get
            {
                return _conectionsSize;
            }

            set
            {
                _conectionsSize = value;
            }
        }
        public float LearningRate
        {
            get
            {
                return _learningRate;
            }

            set
            {
                _learningRate = value;
            }
        }
        public float CachedLearningRate
        {
            get
            {
                return _cachedLearningRate;
            }

            set
            {
                _cachedLearningRate = value;
            }
        }
        public float Momentum
        {
            get
            {
                return _momentum;
            }

            set
            {
                _momentum = value;
            }
        }
        public float CachedMomentum
        {
            get
            {
                return _cachedMomentum;
            }

            set
            {
                _cachedMomentum = value;
            }
        }

        public Index2 MSize => new Index2(_size, _conectionsSize);
        public ActivationLayer2(int size, int conectionsSize)
        {
            Size = size;
            ConectionsSize = conectionsSize;
        }
    }
}
