using VI.NumSharp.Arrays;

namespace VI.Neural.Drivers.Executors
{
    public interface IOptimizationExecutor
    {
        void AdaGrad(FloatArray2D param, FloatArray2D grad, FloatArray2D m, float learningRate);
        void AdaGrad(FloatArray param, FloatArray grad, FloatArray m, float learningRate);

        void RMSProp(FloatArray2D param, FloatArray2D grad, FloatArray2D m, float learningRate);
        void RMSProp(FloatArray param, FloatArray grad, FloatArray m, float learningRate);

        void Adam(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate);
        void Adam(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate);

        void Nadam(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate);
        void Nadam(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate);

        void Adadelta(FloatArray2D param, FloatArray2D grad, FloatArray2D m, FloatArray2D v, float learningRate);
        void Adadelta(FloatArray param, FloatArray grad, FloatArray m, FloatArray v, float learningRate);
    } 
}