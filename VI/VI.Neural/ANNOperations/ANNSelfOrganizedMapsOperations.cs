using System;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public sealed class ANNSelfOrganizedMapsOperations : IUnsupervisedOperations, I2DStructure
    {
        private ILayer _target;

        private (int w, int h) _size;
        private (int x, int y) _minimalLocal;

        private float _dist;
        private float _maxDist;
        private float _desc;

        private float _maxValue;
        private float _minValue;
        
        public void FeedForward(Array<float> feed)
        {
            _target.OutputVector = (feed.T - _target.KnowlodgeMatrix)
                .Pow(2)
                .SumColumn();
        }

        public void ErrorGradient(Array<float> values)
        {
            _target.GradientMatrix = (values.T - _target.KnowlodgeMatrix);
        }

        public void UpdateParams()
        {
            var dist = NumMath.Euclidian(_size.w, _size.h, _minimalLocal.x, _minimalLocal.y);
            var mask = dist <= _maxDist;

            var tx = (dist / _maxDist)
                .ApplyMask(mask)
                .AsLinear();
            
            _target.KnowlodgeMatrix += ((_target.LearningRate * tx).T * _target.GradientMatrix);
        }

        public Array2D<float> GetActivation()
        {
            return _target.OutputVector.As2DView(_size.w, _size.h);
        }

        public void Set2DSize(int w, int h)
        {
            _dist = (float)Math.Sqrt(Math.Pow(0 - _size.w, 2) + Math.Pow(0 - _size.h, 2));
            _size = (w, h);
        }

        public void SetNeighborhoodDistance(float dist, float desc)
        {
            _desc = desc;
            _maxDist = dist;
        }

        public void SetLayer(ILayer layer)
        {
            _target = layer;
        }        
    }
}